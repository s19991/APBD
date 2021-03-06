﻿using System;
using System.Collections.Generic;

namespace c03.EntityModels
{
    public partial class Kategoria
    {
        public Kategoria()
        {
            Pokoj = new HashSet<Pokoj>();
        }

        public int IdKategoria { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }

        public virtual ICollection<Pokoj> Pokoj { get; set; }
    }
}
