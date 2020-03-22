using System;
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

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut]
        public IActionResult PutStudent(int IdStudent)
        {
            // todo dokonczy post
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int IdStudent)
        {
            // todo dokonczyc delete
            return Ok();
        }
        
        
    }
}