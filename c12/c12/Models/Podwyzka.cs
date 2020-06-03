using System;
using System.Collections.Generic;

namespace c12.Models
{
    public partial class Podwyzka
    {
        public int Id { get; set; }
        public int IdOsoba { get; set; }
        public string Nazwisko { get; set; }
        public int? Mgr { get; set; }
        public int? Dzial { get; set; }
        public int? StaraPensja { get; set; }
        public int? NowaPensja { get; set; }
        public DateTime? DataPodwyzki { get; set; }
    }
}
