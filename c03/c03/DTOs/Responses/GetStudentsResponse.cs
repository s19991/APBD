using System.Collections.Generic;
using c03.EntityModels;

namespace c03.DTOs.Responses
{
    public class GetStudentsResponse
    {
        public List<Student> Students { get; set; }
    }
}