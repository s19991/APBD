using System;
using System.Collections.Generic;

namespace c12.Models
{
    public partial class PrescriptionMedicaments
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }

        public virtual Medicaments IdMedicamentNavigation { get; set; }
        public virtual Prescriptions IdPrescriptionNavigation { get; set; }
    }
}
