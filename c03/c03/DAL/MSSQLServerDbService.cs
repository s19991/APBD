using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using c03.DTOs.Requests;
using c03.DTOs.Responses;
using c03.Models;
using Microsoft.AspNetCore.Mvc;

namespace c03.DAL
{
    public class MSSQLServerDbService : IDbService
    {
        private readonly string _connectionString = "Data Source=db-mssql.pjwstk.edu.pl; " 
                                                    + "Initial Catalog=s19991; " 
                                                    + "User Id=apbds19991;"
                                                    + " Password=admin";

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
        
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response;
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction; 
                
                Console.WriteLine($"Looking for studies {request.Studies}");
                command.CommandText = "select IdStudy from studies where name=@name";
                command.Parameters.AddWithValue("name", request.Studies);
                var reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (!reader.HasRows)
                {
                    reader.Close();
                    transaction.Rollback();
                    throw new ArgumentException($"Unknown studies {request.Studies}");
                }

                Console.WriteLine($"Looking for enrollment {request.Studies}");
                reader.Read();
                int idStudy = (int) reader["IdStudy"];
                reader.Close();
                
                int idEnrollment = _getIdEnrollment(command, idStudy);
                if (idEnrollment == 0)
                {
                    Console.WriteLine($"Adding enrollment for {request.Studies}");
                    _insertIntoEnrollments(command, idStudy);
                    idEnrollment = _getIdEnrollment(command, idStudy);
                }

                Console.WriteLine($"Checking if student {request.IndexNumber} exists");
                command.CommandText = "select * from Student where IndexNumber=@indexnumber";
                command.Parameters.AddWithValue("indexnumber", request.IndexNumber);
                reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (reader.HasRows)
                {
                    reader.Close();
                    transaction.Rollback();
                    throw new ArgumentException($"Student {request.Studies} exists");
                }

                reader.Close();
                command.CommandText = "insert into Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) " 
                                      + "values (@indexnumber, @firstname, @lastname, @birthdate, @idenrollment)";
                command.Parameters.AddWithValue("indexnumber", request.IndexNumber);
                command.Parameters.AddWithValue("firstname", request.Firstname);
                command.Parameters.AddWithValue("lastname", request.LastName);
                command.Parameters.AddWithValue("birthdate", request.BirthDate);
                command.Parameters.AddWithValue("idenrollment", idEnrollment);
                reader = command.ExecuteReader();
                command.Parameters.Clear();
                response = new EnrollStudentResponse()
                {
                    LastName = request.LastName,
                    Semester = 1,
                    StartDate = DateTime.Now
                };
                reader.Close();
                transaction.Commit();
            }

            return response;
        }

        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            PromoteStudentResponse response = new PromoteStudentResponse();
            response.Enrollments = new List<object>();
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction; 
                
                Console.WriteLine($"Looking for studies {request.Studies} and semester {request.Semester}");
                command.CommandText = "select * from enrollment e " 
                                      + "inner join studies s on e.idstudy = s.idstudy " 
                                      + "where s.name = @name and e.semester = @semester";
                command.Parameters.AddWithValue("name", request.Studies);
                command.Parameters.AddWithValue("semester", request.Semester);
                var reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (!reader.HasRows)
                {
                    reader.Close();
                    transaction.Rollback();
                    throw new SqlNullValueException(
                        $"Couldn't find students on {request.Studies} {request.Semester}"
                        );
                }
                reader.Close();
    
                Console.WriteLine($"Executing stored procedure");
                command.CommandText = "exec promote_students @studies_name, @semester";
                command.Parameters.AddWithValue("studies_name", request.Studies);
                command.Parameters.AddWithValue("semester", request.Semester);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                
                Console.WriteLine($"Getting all new promoted records");
                command.CommandText = "select e.* from enrollment e " 
                                      + "inner join studies s on e.idstudy = s.idstudy " 
                                      + "where s.name = @studies_name and e.semester = @semester+1";
                command.Parameters.AddWithValue("studies_name", request.Studies);
                command.Parameters.AddWithValue("semester", request.Semester);
                reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (!reader.HasRows)
                {
                    reader.Close();
                    transaction.Rollback();
                    throw new SqlNullValueException("Couldn't get the new records");
                }

                while (reader.Read())
                {
                    response.Enrollments.Add(
                        new
                        {
                            IdEnrollment = int.Parse(reader["IdEnrollment"].ToString()),
                            Semester = int.Parse(reader["Semester"].ToString()),
                            IdStudy = int.Parse(reader["IdStudy"].ToString()),
                            StartDate = DateTime.Parse(reader["StartDate"].ToString())
                        }
                    );
                }
                reader.Close();
                transaction.Commit();
            }

            return response;
        }
        
        private void _insertIntoEnrollments(SqlCommand command, int idStudy)
        {
            command.CommandText = "insert into Enrollment(IDENROLLMENT, SEMESTER, IDSTUDY, STARTDATE) "
                                  + "values ((select count(*)+1 from Enrollment), 1, @id_study, getdate())";
            command.Parameters.AddWithValue("id_study", idStudy);
            var reader = command.ExecuteReader();
            command.Parameters.Clear();
            reader.Close();
        }
        
        private int _getIdEnrollment(SqlCommand command, int idStudy)
        {
            command.CommandText = "select IdEnrollment from Enrollment " 
                                  + "where semester = 1 and idstudy = @id_study and StartDate = "
                                  + "(select max(StartDate) from Enrollment " 
                                  + " where semester = 1 and IdStudy = @id_study)";
            command.Parameters.AddWithValue("id_study", idStudy);
            var reader = command.ExecuteReader();
            command.Parameters.Clear();
            reader.Read();
            int idEnrollment = reader.HasRows ? int.Parse(reader["IdEnrollment"].ToString()) : 0;
            reader.Close();
            return idEnrollment;
        }

        public Student GetStudent(string indexNumber)
        {
            Student student = null;
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                
                command.CommandText = "select * from student "
                                      + "where IndexNumber = @index_number";
                command.Parameters.AddWithValue("index_number", indexNumber);
                var reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return null;
                }

                while (reader.Read())
                {
                    student = new Student
                    {
                        IndexNumber = reader["IndexNumber"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                        IdEnrollment = int.Parse(reader["IdEnrollment"].ToString())
                    };
                }
            }

            return student;
        }

        public LoginResponse Login(string login, string password)
        {
            LoginResponse response;
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                
                command.CommandText = "select * from password "
                                      + "where StudentIndexNumber = @login " 
                                      + "and Password = @password";
                command.Parameters.AddWithValue("login", login);
                command.Parameters.AddWithValue("password", password);
                var reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (!reader.HasRows)
                {
                    reader.Close();
                    throw new Exception($"Could not login {login} user");
                }

                response = new LoginResponse
                {
                    message = $"{login} logged in successfuly"
                };
            }

            return response;
        }
    }
}