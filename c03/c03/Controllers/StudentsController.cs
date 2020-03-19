using Microsoft.AspNetCore.Mvc;

namespace c03.Controllers
{
    [ApiController]
    [Route("api/students")]
    
    public class StudentsController: ControllerBase
    {
        [HttpGet]
        public string GetStudent()
        {
            return "Kowalski, Malewski, Andrzejewski";
        }
    }
}