using System;
using AdvertApi.DAL;
using AdvertApi.DTO.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AdvertApi.Controllers
{
    [Route("adverts")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IDbService _dbService;
        
        public AdvertController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserRequest request)
        {
            // todo 4 koncowka do rejestracji uzytkowniko
            return Ok();
        }
        
        // todo 5 koncowka do odswiezania access tokenu

        [HttpPost("login")]
        public IActionResult LoginUser(LoginUserRequest request)
        {
            // todo 6 koncowka do logowania
            return Ok();
        }

        [HttpGet("campaigns")]
        public IActionResult GetCampaigns()
        {
            IActionResult response;
            try
            {
                response = Ok(_dbService.GetCampaigns());
            }
            catch (Exception e)
            {
                response = BadRequest(e);
            }
            return response;
        }

        [HttpPost("campaign")]
        public IActionResult CreateCampaign(CreateCampaignRequest request)
        {
            // todo 8 Tworzenie nowej kampanii
            return Ok();
        }
    }
}