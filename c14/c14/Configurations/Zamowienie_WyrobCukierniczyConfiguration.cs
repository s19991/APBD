using System.Collections.Generic;
using c14.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace c14.Configurations
{
    public class Zamowienie_WyrobCukierniczyConfiguration: IEntityTypeConfiguration<Zamowienie_WyrobCukierniczy>
    {
        public void Configure(EntityTypeBuilder<Zamowienie_WyrobCukierniczy> builder)
        {
            builder.HasKey(key => key.IdWyrobCukierniczy);
            builder.HasKey(key => key.IdZamowienia);
            builder.Property(x => x.Ilosc);
            builder.Property(x => x.Uwagi).HasMaxLength(300);

            var orderConfectionaries = new List<Zamowienie_WyrobCukierniczy>
            {
                new Zamowienie_WyrobCukierniczy
                {
                    IdWyrobCukierniczy = 1, 
                    IdZamowienia = 1, 
                    Ilosc = 2137, 
                    Uwagi = "mniami ma byc"
                }
            };

            builder.HasData(orderConfectionaries);
        }
    }
}