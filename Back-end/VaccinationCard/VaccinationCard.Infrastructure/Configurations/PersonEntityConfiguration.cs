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
    internal class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.EntityId);

            builder.HasMany(x => x.Vaccinations)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("CURDATE()");

            builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate().HasDefaultValueSql("CURDATE()");
        }
    }
}
