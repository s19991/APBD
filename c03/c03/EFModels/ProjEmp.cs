using System;
using System.Collections.Generic;

namespace c03.EntityModels
{
    public partial class ProjEmp
    {
        public int Projno { get; set; }
        public int Empno { get; set; }

        public virtual Emp EmpnoNavigation { get; set; }
        public virtual Proj ProjnoNavigation { get; set; }
    }
}
