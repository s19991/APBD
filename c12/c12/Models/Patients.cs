using System;
using System.Collections.Generic;

namespace c12.Models
{
    public partial class Patients
    {
        public Patients()
        {
            Prescriptions = new HashSet<Prescriptions>();
        }

        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
