using System;
using System.Collections.Generic;

namespace kol01.DTOs.Responses
{
    public class GetPrescriptionsResponse
    {
        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public List<MedicamentsResponse> Medicaments { get; set; }
    }
}