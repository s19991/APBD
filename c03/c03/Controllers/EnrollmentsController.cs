using System;
using System.Data.SqlTypes;
using c03.DAL;
using c03.DTOs.Requests;
using c03.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace c03.Controllers
{
    
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        
        public EnrollmentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse studentResponse;
            IActionResult response;
            try
            {
                studentResponse = _dbService.EnrollStudent(request);
                response = Created("Student enrolled succesfully", studentResponse);
            }
            catch (ArgumentException e)
            {
                response = BadRequest(e.Message);
            }
            catch (Exception e)
            {
                response = BadRequest($"Some other error occured {e.StackTrace}");
            }

            return response;
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            PromoteStudentResponse studentResponse;
            IActionResult response;
            try
            {
                studentResponse = _dbService.PromoteStudent(request);
                response = Created("Student promoted succesfully", studentResponse);
            }
            catch (SqlNullValueException e)
            {
                response = NotFound(e.Message);
            }
            catch (Exception e)
            {
                response = BadRequest($"Some other error occured {e.StackTrace}");
            }

            return response;
        }
    }
}