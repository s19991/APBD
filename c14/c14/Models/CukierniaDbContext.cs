using c14.Configurations;
using c14.Models;

using Microsoft.EntityFrameworkCore;

namespace c14.Models
{
    public class CukierniaDbContext : DbContext
    {
        public DbSet<Klient> Klients { get; set; }
        
        public DbSet<Pracownik> Pracowniks { get; set; }
        public DbSet<WyrobCukierniczy> WyrobCukierniczys { get; set; }
        public DbSet<Zamowienie> Zamowienies { get; set; }
        public DbSet<Zamowienie_WyrobCukierniczy> ZamowienieWyrobCukierniczies { get; set; }

        public CukierniaDbContext() {}
        
        public CukierniaDbContext(DbContextOptions options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new KlientConfiguration());
            modelBuilder.ApplyConfiguration(new PracownikConfiguration());
            modelBuilder.ApplyConfiguration(new WyrobCukierniczyConfiguration());
            modelBuilder.ApplyConfiguration(new ZamowienieConfiguration());
            modelBuilder.ApplyConfiguration(new Zamowienie_WyrobCukierniczyConfiguration());
    
        }
    }
}