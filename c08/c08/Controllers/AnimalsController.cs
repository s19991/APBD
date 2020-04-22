using System;
using c08.DAL;
using c08.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace c08.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public IConfiguration Configuration { get; set; }
        
        public AnimalsController(IConfiguration configuration, IDbService dbService)
        {
            _dbService = dbService;
            Configuration = configuration;
        }
        

        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
        {
            GetAnimalsResponse animalsResponse;
            IActionResult response;
            try
            {
                animalsResponse = _dbService.GetAnimals(orderBy);
                response = Ok(animalsResponse);
            }
            catch (Exception e)
            {
                response = NotFound($"{e}");
            }
            
            
            return response;
        } 
    }
}