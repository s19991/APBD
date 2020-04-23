using System;

namespace kol01.DTOs.Requests
{
    public class PostPrescriptionRequest
    {
        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }
    }
}