using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Infrastructure.Configurations;

namespace VaccinationCard.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Vaccine> Vaccines { get; set; }

        public DbSet<VaccineDoseType> VaccineDoseTypes { get; set; }

        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

        public DbSet<DoseType> DoseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VaccineDoseTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DoseTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationRecordEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineEntityConfiguration());
        }
    }
}
