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
    public class VaccineEntityConfiguration : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.HasKey(x => x.EntityId);

            builder.HasMany(x => x.VaccineRecords)
                .WithOne(x => x.Vaccine)
                .HasForeignKey(x => x.VaccineId);
        }
    }
}
