using System;
using System.Collections.Generic;
using c14.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;

namespace c14.Configurations
{
    public class ZamowienieConfiguration: IEntityTypeConfiguration<Zamowienie>
    {
        public void Configure(EntityTypeBuilder<Zamowienie> builder)
        {
            builder.HasKey(key => key.IdZamowienia);
            builder.Property(x => x.DataPrzyjecia);
            builder.Property(x => x.DataRealizacji);
            builder.Property(x => x.Uwagi).HasMaxLength(300);
            builder.Property(x => x.IdKlient);
            builder.Property(x => x.IdPracownik);

            var orders = new List<Zamowienie>
            {
                new Zamowienie
                {
                    IdZamowienia = 1, 
                    DataPrzyjecia = DateTime.Now, 
                    DataRealizacji = DateTime.Now,
                    Uwagi = "aaaaaaaaaaaaaaaa",
                    IdKlient = 1,
                    IdPracownik = 1
                }
            };

            builder.HasData(orders);
        }
    }
}