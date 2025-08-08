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
    internal class DoseTypeEntityConfiguration : IEntityTypeConfiguration<DoseType>
    {
        public void Configure(EntityTypeBuilder<DoseType> builder)
        {
            builder.HasKey(x => x.EntityId);

            builder.HasMany(x => x.VaccineDoseTypes)
                .WithOne(x => x.DoseType)
                .HasForeignKey(x => x.DoseTypeId);

            builder.HasMany(x => x.VaccinationRecords)
                .WithOne(x => x.DoseType)
                .HasForeignKey(x => x.DoseTypeId);
        }
    }
}
