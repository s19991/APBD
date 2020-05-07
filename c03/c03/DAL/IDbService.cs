using c03.Models;
using System.Collections.Generic;
using c03.DTOs.Requests;
using c03.DTOs.Responses;

namespace c03.DAL
{
    public interface IDbService
    {
        public Student GetStudent(string indexNumber);
        public GetStudentsResponse GetStudents();
        public void ModifyStudent(ModifyStudentRequest request);
        public void DeleteStudent(DeleteStudentRequest request);
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request);
        public LoginResponse Login(string login, string password);
    }
}