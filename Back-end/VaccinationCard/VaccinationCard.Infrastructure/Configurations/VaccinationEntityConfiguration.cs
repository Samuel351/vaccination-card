using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Configurations
{
    public class VaccinationEntityConfiguration : IEntityTypeConfiguration<Vaccination>
    {
        public void Configure(EntityTypeBuilder<Vaccination> builder)
        {
            builder.HasKey(x => new {  x.EntityId, x.VaccineId, x.PersonId, x.DoseNumber });

            builder.Property(x => x.EntityId).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Vaccine)
                .WithMany(x => x.VaccineRecords)
                .HasForeignKey(x => x.VaccineId)
                .OnDelete(DeleteBehavior.ClientSetNull) ;

            builder.HasOne(x => x.Person)
                .WithMany(x => x.Vaccinations)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
