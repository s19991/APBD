using System;
using System.Collections.Generic;

namespace c12.Models
{
    public partial class Medicaments
    {
        public Medicaments()
        {
            PrescriptionMedicaments = new HashSet<PrescriptionMedicaments>();
        }

        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<PrescriptionMedicaments> PrescriptionMedicaments { get; set; }
    }
}
