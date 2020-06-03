using System;
using System.Collections.Generic;

namespace c03.EntityModels
{
    public partial class Proj
    {
        public Proj()
        {
            ProjEmp = new HashSet<ProjEmp>();
        }

        public int Projno { get; set; }
        public string Pname { get; set; }
        public decimal? Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<ProjEmp> ProjEmp { get; set; }
    }
}
