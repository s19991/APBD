using System;
using System.Collections.Generic;

namespace c03.EntityModels
{
    public partial class Owner
    {
        public Owner()
        {
            Animal = new HashSet<Animal>();
        }

        public int IdOwner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Animal> Animal { get; set; }
    }
}
