using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Net;
using c03.DAL;
using c03.DTOs.Requests;
using c03.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace c03.Controllers
{
    
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        // todo zadanie 3 -> przeniesc calosc do osobnej klasy
        private readonly IDbService _dbService;
        private readonly string _connectionString = "Data Source=db-mssql.pjwstk.edu.pl; " 
                                                    + "Initial Catalog=s19991; " 
                                                    + "User Id=apbds19991;"
                                                    + " Password=admin";

        public EnrollmentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                
                Console.WriteLine($"Looking for studies {request.Studies}");
                command.CommandText = "select IdStudies from studies where name=@name";
                command.Parameters.AddWithValue("name", request.Studies);
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    transaction.Rollback();
                    return BadRequest($"Unknown studies {request.Studies}");
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
                if (!reader.HasRows)
                {
                    transaction.Rollback();
                    return BadRequest($"Student {request.Studies} exists");
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

            return Created("Student enrolled succesfully", response);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            //todo sprawdzanie czy w enrollment sa rekordy dla request(studies, semester) -> inaczej 404 not found
            //todo napisac procedurke w bazie i jej odpalenie dopisac tutaj
            return Ok();
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

        private void _insertIntoEnrollments(SqlCommand command, int idStudies)
        {
            command.CommandText = "insert into Enrollment(IDENROLLMENT, SEMESTER, IDSTUDY, STARTDATE) "
                                  + "values ((select count(*)+1 from Enrollment), 1, @idstudies, getdate())";
            command.Parameters.AddWithValue("idstudies", idStudies);
            command.ExecuteReader();
        }
    }
}