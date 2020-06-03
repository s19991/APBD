using System;
using System.Collections.Generic;

namespace c03.EntityModels
{
    public partial class Pacjent
    {
        public int IdPacjent { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUr { get; set; }
    }
}
