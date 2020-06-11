using System.Collections.Generic;
using c14.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace c14.Configurations
{
    public class WyrobCukierniczyConfiguration: IEntityTypeConfiguration<WyrobCukierniczy>
    {
        public void Configure(EntityTypeBuilder<WyrobCukierniczy> builder)
        {
            builder.HasKey(key => key.IdWyrobCukierniczy);
            builder.Property(x => x.Nazwa).HasMaxLength(200);
            builder.Property(x => x.CenaZaSzt);
            builder.Property(x => x.Typ).HasMaxLength(40);

            var confectionaries = new List<WyrobCukierniczy>
            {
                new WyrobCukierniczy()
                {
                    IdWyrobCukierniczy = 1, 
                    Nazwa = "Mniami", 
                    CenaZaSzt = 21.37f,
                    Typ = "Mniamuwa"
                }
            };

            builder.HasData(confectionaries);
        }
    }
}