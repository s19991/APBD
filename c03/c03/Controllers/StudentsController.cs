using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using c03.DAL;
using c03.Models;
using Microsoft.AspNetCore.Mvc;

namespace c03.Controllers
{
    [ApiController]
    [Route("api/students")]
    
    public class StudentsController: ControllerBase
    {
        private readonly IDbService _dbService;
        private readonly string _connectionString = "Data Source=db-mssql.pjwstk.edu.pl; " 
                                                    + "Initial Catalog=s19991; " 
                                                    + "User Id=apbds19991;"
                                                    + " Password=admin";
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet]
        public IActionResult GetStudents()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using(SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select * from Student";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (!reader.HasRows) return NotFound($"No students");
                
                List<Object> queryResult = new List<Object>();
                Student student = new Student();
                int idStudent = 1;
                while (reader.Read())
                {
                    student = new Student
                    {
                        IdStudent = idStudent++,
                        IndexNumber = reader["IndexNumber"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                        IdEnrollment = int.Parse(reader["IdEnrollment"].ToString())
                    };
                    queryResult.Add(student);                    
                }
                return Ok(queryResult);
            }
        }
        

        [HttpGet("{idStudent}")]
        public IActionResult GetStudent(int idStudent)
        {
            // todo naprawic parsowanie id <-> index, lub w bazce pozbyc sie zer przed s
            string indexNumber = $"s000{idStudent}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using(SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select s.*, st.Name, e.Semester, e.StartDate " 
                                      + "from Student s "
                                      + "inner join Enrollment e on s.IdEnrollment = e.IdEnrollment "
                                      + "inner join Studies st on e.IdStudy = st.IdStudy " 
                                      + " where s.IndexNumber=@indexNumber";
                command.Parameters.AddWithValue("indexNumber", indexNumber);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return NotFound($"No student for {indexNumber}");

                List<Object> queryResult = new List<Object>();
                Student student;
                Object enrollment;
                while (reader.Read())
                {
                    student = new Student
                    {
                        IdStudent = idStudent,
                        IndexNumber = reader["IndexNumber"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        BirthDate = DateTime.Parse(reader["BirthDate"].ToString())
                    };
                    enrollment = new
                    {
                        enrollmentName = reader["Name"].ToString(),
                        semester = reader["Semester"].ToString()
                    };
                    var returnedValue = new
                    {
                        student,
                        enrollment
                    };
                    queryResult.Add(returnedValue);
                }
                
                return Ok(queryResult);
            }
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{student.IdStudent}";
            return Ok($"{student} created");
        }

        [HttpPut]
        public IActionResult PutStudent(Student student)
        {
            return Ok($"{student} updated");
        }

        [HttpDelete]
        public IActionResult DeleteStudent(Student student)
        {
            return Ok($"{student} deleted");
        }
        
        
    }
}