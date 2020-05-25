using System;
using System.Collections.Generic;
using c11.Models;
using c11.DAL;
using Microsoft.AspNetCore.Mvc;

namespace c11.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _service;
        
        public DoctorsController(IDbService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult GetDoctors()
        {
            IActionResult response;
            try
            {
                IEnumerable<Doctor> doctors = _service.GetDoctors();
                response = Ok(doctors);
            }
            catch (Exception e)
            {
                response = BadRequest($"Could not get doctors due to:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }
        
        [HttpPost]
        public IActionResult AddNewDoctor(Doctor doctor)
        {
            IActionResult response;
            try
            {
                _service.AddDoctor(doctor);
                response = Ok($"Doctor: {doctor.IdDoctor} has been added");
            }
            catch (Exception e)
            {
                response = BadRequest($"Could not add: {doctor.IdDoctor} due to:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }
        
        [HttpPut]
        public IActionResult ModifyDoctor(Doctor doctor)
        {
            IActionResult response;
            try
            {
                _service.ModifyDoctor(doctor);
                response = Ok($"Doctor: {doctor.IdDoctor} has been modified");
            }
            catch (Exception e)
            {
                response = BadRequest($"Could not modify: {doctor.IdDoctor} due to:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }

        [HttpPost("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            IActionResult response;
            try
            {
                _service.DeleteDoctor(id);
                response = Ok($"Doctor: {id} has been deleted");
            }
            catch (Exception e)
            {
                response = BadRequest($"Could not delete: {id} due to:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }
    }
}