using System;
using System.Collections.Generic;

namespace c12.Models
{
    public partial class Doctors
    {
        public Doctors()
        {
            Prescriptions = new HashSet<Prescriptions>();
        }

        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
