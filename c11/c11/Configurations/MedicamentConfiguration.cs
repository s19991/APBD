using c11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace c11.Configurations
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(key => key.IdMedicament);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Type).HasMaxLength(100);

            var medicaments = new List<Medicament>()
            {
                new Medicament
                {
                    IdMedicament = 1, 
                    Name = "Linux",
                    Description = "POTĘŻNY lek", 
                    Type = "świrek" 
                },
                new Medicament
                {
                    IdMedicament = 2, 
                    Name = "Windows",
                    Description = "na co to komu", 
                    Type = "zwykły" 
                },
                new Medicament
                {
                    IdMedicament = 3, 
                    Name = "macOS",
                    Description = "drogi linux", 
                    Type = "złodziejski" 
                }
            };

            builder.HasData(medicaments);
        }
    }
}