using c11.Models;
using System.Collections.Generic;

namespace c11.DAL
{
    public interface IDbService
    {
        public IEnumerable<Doctor> GetDoctors();
        
        public void AddDoctor(Doctor doctor);
        
        public void DeleteDoctor(int id);
        
        public void ModifyDoctor(Doctor doctor);
    }
}