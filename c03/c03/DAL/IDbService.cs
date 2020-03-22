using c03.Models;
using System.Collections;

namespace c03.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}