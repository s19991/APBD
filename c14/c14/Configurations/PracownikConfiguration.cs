using System.Collections.Generic;
using c14.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace c14.Configurations
{
    public class PracownikConfiguration : IEntityTypeConfiguration<Pracownik>
    {
        public void Configure(EntityTypeBuilder<Pracownik> builder)
        {
            builder.HasKey(key => key.IdPracownik);
            builder.Property(x => x.Imie).HasMaxLength(50);
            builder.Property(x => x.Nazwisko).HasMaxLength(60);

            var employees = new List<Pracownik>
            {
                new Pracownik
                {
                    IdPracownik = 1, 
                    Imie = "Cymbał", 
                    Nazwisko = "Polak"
                }
            };

            builder.HasData(employees);
        }
    }
}