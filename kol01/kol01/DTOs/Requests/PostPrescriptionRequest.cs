using System;
using System.ComponentModel.DataAnnotations;

namespace kol01.DTOs.Requests
{
    public class PostPrescriptionRequest
    {
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int IdPatient { get; set; }

        [Required]
        public int IdDoctor { get; set; }
    }
}