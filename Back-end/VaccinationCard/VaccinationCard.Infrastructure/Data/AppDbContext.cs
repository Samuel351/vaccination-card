using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Infrastructure.Configurations;

namespace VaccinationCard.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Vaccine> Vaccines { get; set; }

        public DbSet<Vaccination> Vaccination { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineEntityConfiguration());
        }
    }
}
