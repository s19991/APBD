using System;
using kol01.DAL;
using kol01.DTOs.Requests;
using kol01.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace kol01.Controllers
{
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public IConfiguration Configuration { get; set; }
        public PrescriptionsController(IConfiguration configuration, IDbService dbService)
        {
            _dbService = dbService;
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetPrescriptions(int id)
        {
            GetPrescriptionsResponse prescriptionsResponse;
            IActionResult response;
            try
            {
                prescriptionsResponse = _dbService.GetPrescriptions(id);
                response = Ok(prescriptionsResponse);
            }
            catch (Exception e)
            {
                response = NotFound(e.Message);
            }

            return response;
        }

        [HttpPost]
        public IActionResult PostPrescriptions(PostPrescriptionRequest request)
        {
            PostPrescriptionResponse prescriptionsResponse;
            IActionResult response;
            try
            {
                prescriptionsResponse = _dbService.PostPrescription(request);
                response = Ok(prescriptionsResponse);
            }
            catch (Exception e)
            {
                response = NotFound(e.Message);
            }

            return response;
        }
        
    }
}