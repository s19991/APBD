using c11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace c11.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(key => key.IdPatient);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);

            var patients = new List<Patient>
            {
                new Patient
                {
                  IdPatient  = 1,
                  FirstName = "Leon",
                  LastName = "Linuksiarz",
                  BirthDate = Convert.ToDateTime("1990-12-12")
                },
                new Patient
                {
                    IdPatient  = 2,
                    FirstName = "Wojciech",
                    LastName = "Windowsowski",
                    BirthDate = Convert.ToDateTime("1991-01-01")
                },
                new Patient
                {
                    IdPatient  = 3,
                    FirstName = "Marian",
                    LastName = "Makowiak",
                    BirthDate = Convert.ToDateTime("1991-01-02")
                }
            };
            
            builder.HasData(patients);
        }
    }
}