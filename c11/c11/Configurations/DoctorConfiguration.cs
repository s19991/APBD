using c11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace c11.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(key => key.IdDoctor);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.Email).HasMaxLength(100);

            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    IdDoctor = 1, 
                    FirstName = "Linus", 
                    LastName = "Torvals", 
                    Email = "linus@torvalds.io"
                },
                new Doctor
                {
                    IdDoctor = 2, 
                    FirstName = "Bill", 
                    LastName = "Gates", 
                    Email = "bill@gates.io"
                },
                new Doctor
                {
                    IdDoctor = 3, 
                    FirstName = "Steve", 
                    LastName = "Jobs", 
                    Email = "steve@jobs.io"
                }
            };

            builder.HasData(doctors);
        }
    }
}