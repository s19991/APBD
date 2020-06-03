using System;
using System.Collections.Generic;

namespace c03.EntityModels
{
    public partial class Lekarz
    {
        public int IdLekarz { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Specjalizacja { get; set; }
        public int? Pensja { get; set; }
    }
}
