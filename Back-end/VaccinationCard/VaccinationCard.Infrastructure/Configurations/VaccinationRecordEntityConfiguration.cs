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
    public class VaccinationRecordEntityConfiguration : IEntityTypeConfiguration<VaccinationRecord>
    {
        public void Configure(EntityTypeBuilder<VaccinationRecord> builder)
        {
            builder.HasKey(x => new { x.VaccineId, x.PersonId, x.DoseNumber });

            builder.HasOne(x => x.Vaccine)
                .WithMany(x => x.VaccineRecords)
                .HasForeignKey(x => x.VaccineId);

            builder.HasOne(x => x.Person)
                .WithMany(x => x.VaccinationRecords)
                .HasForeignKey(x => x.VaccineId);
        }
    }
}
