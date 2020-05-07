using System;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using c03.DAL;
using c03.DTOs.Requests;
using c03.DTOs.Responses;
using c03.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace c03.Controllers
{
    
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public IConfiguration Configuration { get; set; }
        public EnrollmentsController(IConfiguration configuration, IDbService dbService)
        {
            _dbService = dbService;
            Configuration = configuration;
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            // todo napisac na EF
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
                response = BadRequest(
                    $"Some other error occured:\n[StackTrace]: {e.StackTrace}\n[Message]: {e.Message}"
                );
            }

            return response;
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            // todo napisac na EF
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
                response = BadRequest(
                    $"Some other error occured:\n[StackTrace]: {e.StackTrace}\n[Message]: {e.Message}"
                    );
            }

            return response;
        }

        [HttpGet("getstudents")]
        public IActionResult GetStudents()
        {
            IActionResult response;
            
            try
            {
                response = Ok(_dbService.GetStudents());
            }
            catch (Exception e)
            {
                response = BadRequest(e);
            }
            
            return response;
        }
        
        [HttpPost("modifystudent")]
        public IActionResult ModifyStudent(ModifyStudentRequest request)
        {
            IActionResult response = Ok($"Successfully modified {request.IndexNumber}");
            try
            {
                _dbService.ModifyStudent(request);
            }
            catch (Exception e)
            {
                response = BadRequest(e);
            }
            return response;
        }
        
        [HttpPost("deletestudent")]
        public IActionResult DeleteStudent(DeleteStudentRequest request)
        {
            IActionResult response = Ok($"Successfully deleted {request.IndexNumber}");
            try
            {
                _dbService.DeleteStudent(request);
            }
            catch (Exception e)
            {
                response = BadRequest(e);
            }
            return response;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                _dbService.Login(request.Login, request.Password);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
            
            Console.WriteLine($"Logged in {request.Login}");
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.Login),
                new Claim(ClaimTypes.Name, "test"), 
                new Claim(ClaimTypes.Role, "employee")
            };
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"])); 
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: "s19991",
                audience: "employees",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );
            
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });
        }
    }
}