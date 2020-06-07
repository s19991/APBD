using AdvertApi.DTO.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AdvertApi.Controllers
{
    public class AdvertController : ControllerBase
    {
        public IActionResult RegisterUser(RegisterUserRequest request)
        {
            // todo 4 koncowka do rejestracji uzytkowniko
            return Ok();
        }
        
        // todo 5 koncowka do odswiezania access tokenu

        public IActionResult LoginUser(LoginUserRequest request)
        {
            // todo 6 koncowka do logowania
            return Ok();
        }

        public IActionResult GetCampaigns(GetCampaignsRequest request)
        {
            // todo 7 Lista kampanii
            return Ok();
        }

        public IActionResult CreateCampaign(CreateCampaignRequest request)
        {
            // todo 8 Tworzenie nowej kampanii
            return Ok();
        }
    }
}