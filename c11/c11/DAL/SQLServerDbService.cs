using c11.Models;
using System.Collections.Generic;
using System.Linq;

namespace c11.DAL
{
    public class SQLServerDbService : IDbService
    {
        private readonly MedicamentDbContext _context;

        public SQLServerDbService(MedicamentDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }
        
        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void DeleteDoctor(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.IdDoctor == id);
            _context.Remove(doctor);
            _context.SaveChanges();
        }

        public void ModifyDoctor(Doctor doctor)
        {
            var d = _context.Doctors.FirstOrDefault(doctor2 => doctor2.IdDoctor == doctor.IdDoctor);
            d.FirstName = doctor.FirstName;
            d.LastName = doctor.LastName;
            d.Email = doctor.Email;
            _context.SaveChanges();
        }
    }
}