using c14.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;


namespace c14.Configurations
{
    public class KlientConfiguration : IEntityTypeConfiguration<Klient>
    {
        public void Configure(EntityTypeBuilder<Klient> builder)
        {
            builder.HasKey(key => key.IdKlient);
            builder.Property(x => x.Imie).HasMaxLength(50);
            builder.Property(x => x.Nazwisko).HasMaxLength(60);

            var clients = new List<Klient>
            {
                new Klient
                {
                    IdKlient = 1, 
                    Imie = "Linus", 
                    Nazwisko = "Torvalds"
                }
            };

            builder.HasData(clients);
        }
    }
}