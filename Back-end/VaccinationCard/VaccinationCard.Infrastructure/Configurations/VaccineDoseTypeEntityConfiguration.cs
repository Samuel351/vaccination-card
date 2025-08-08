using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Configurations
{
    public class VaccineDoseTypeEntityConfiguration : IEntityTypeConfiguration<VaccineDoseType>
    {
        public void Configure(EntityTypeBuilder<VaccineDoseType> builder)
        {
            builder.HasKey(x => new { x.VaccineId, x.DoseTypeId });
        }
    }
}
