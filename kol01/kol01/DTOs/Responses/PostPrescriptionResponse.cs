using System;

namespace kol01.DTOs.Responses
{
    public class PostPrescriptionResponse
    {
        public int IdPrescription { get; set; }
        
        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }
    }
}