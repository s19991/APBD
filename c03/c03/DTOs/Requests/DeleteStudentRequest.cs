using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace c03.DTOs.Requests
{
    public class DeleteStudentRequest
    {
        [Required]
        public string IndexNumber { get; set; }
    }
}