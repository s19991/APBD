using c03.Models;
using System.Collections.Generic;
using c03.DTOs.Requests;
using c03.DTOs.Responses;

namespace c03.DAL
{
    public interface IDbService
    {
        // public IEnumerable<Student> GetStudents();
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request);
    }
}