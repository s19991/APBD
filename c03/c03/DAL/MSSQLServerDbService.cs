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
                command.CommandText = "select IdStudies from studies where name=@name";
                command.Parameters.AddWithValue("name", request.Studies);
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    transaction.Rollback();
                    throw new ArgumentException($"Unknown studies {request.Studies}");
                }

                Console.WriteLine($"Looking for enrollment {request.Studies}");
                int idStudies = (int) reader["IdStudies"];
                int idEnrollment = _getIdEnrollment(command, idStudies);
                if (idEnrollment == 0)
                {
                    Console.WriteLine($"Adding enrollment for {request.Studies}");
                    _insertIntoEnrollments(command, idStudies);
                    idEnrollment = _getIdEnrollment(command, idStudies);
                }

                Console.WriteLine($"Checking if student {request.IndexNumber} exists");
                command.CommandText = "select * from Student where IndexNumber=@indexnumber";
                command.Parameters.AddWithValue("indexnumber", request.IndexNumber);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    transaction.Rollback();
                    throw new ArgumentException($"Student {request.Studies} exists");
                }

                command.CommandText = "insert into Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) " 
                                      + "values (@indexnumber, @firstname, @lastname, @birthdate, @idenrollment)";
                command.Parameters.AddWithValue("indexnumber", request.IndexNumber);
                command.Parameters.AddWithValue("firstname", request.Firstname);
                command.Parameters.AddWithValue("lastname", request.LastName);
                command.Parameters.AddWithValue("birthdate", request.BirthDate);
                command.Parameters.AddWithValue("idenrollment", idEnrollment);
                command.ExecuteReader();

                response = new EnrollStudentResponse()
                {
                    LastName = request.LastName,
                    Semester = 1,
                    StartDate = DateTime.Now
                };
                transaction.Commit();
            }

            return response;
        }

        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            PromoteStudentResponse response = new PromoteStudentResponse();

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
                if (!reader.HasRows)
                {
                    transaction.Rollback();
                    throw new SqlNullValueException(
                        $"Couldn't find students on {request.Studies} {request.Semester}"
                        );
                }

                Console.WriteLine($"Executing stored procedure");
                command.CommandText = "exec promote_students @studies_name, @semester";
                command.Parameters.AddWithValue("studies_name", request.Studies);
                command.Parameters.AddWithValue("semester", request.Semester);
                command.ExecuteNonQuery();
                
                Console.WriteLine($"Getting all new promoted records");
                command.CommandText = "select e.* from enrollment e " 
                                      + "inner join studies s on e.idstudy = s.idstudy " 
                                      + "where s.name = @studies_name and e.semester = @semester+1";
                command.Parameters.AddWithValue("studies_name", request.Studies);
                command.Parameters.AddWithValue("semester", request.Semester);
                reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
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
                transaction.Commit();
            }

            return response;
        }
        
        private void _insertIntoEnrollments(SqlCommand command, int idStudies)
        {
            command.CommandText = "insert into Enrollment(IDENROLLMENT, SEMESTER, IDSTUDY, STARTDATE) "
                                  + "values ((select count(*)+1 from Enrollment), 1, @idstudies, getdate())";
            command.Parameters.AddWithValue("idstudies", idStudies);
            command.ExecuteReader();
        }
        
        private int _getIdEnrollment(SqlCommand command, int idStudies)
        {
            command.CommandText = "select IdEnrollment from Enrollment " 
                                  + "where semester = 1 and idstudy = @idstudies and StartDate = "
                                  + "(select max(StartDate) from Enrollment " 
                                  + " where semester = 1 and IdStudy = @idstudies)";
            command.Parameters.AddWithValue("idstudies", idStudies); 
            var reader = command.ExecuteReader();     
            int idEnrollment = reader.HasRows ? int.Parse(reader["IdEnrollment"].ToString()) : 0;
            return idEnrollment;
        }
    }
}