using System;
using System.ComponentModel.DataAnnotations;

namespace c03.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required]
        [MaxLength(255)]
        public string Studies { get; set; }
        
        [Required] 
        [Range(1, 10)]
        public int Semester { get; set; }    
    }
}