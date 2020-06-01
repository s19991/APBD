using System.Linq;
using c12.Models;
using Microsoft.AspNetCore.Mvc;

namespace c12.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly s19991Context _context;
        
        public DoctorsController(s19991Context context)
        {
            _context = context;
        }
        
        /**
         * TODO PatientsController
         * todo wyswietlanie danych pacjenta = Index -> tabela z pacjentami button (przy kazdym przycisk)
         * todo detale pacjenta -> dane pacjenta + lista recept
         * todo dodawanie pacjenta
         * todo usuwanie pacjenta
         */

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return View(doctors);
        }
    }
}