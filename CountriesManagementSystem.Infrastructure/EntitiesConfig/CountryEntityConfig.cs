using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CountriesManagementSystem.Domain.Entities;

namespace CountriesManagementSystem.Infrastructure.EntitiesConfig
{
    internal class CountryEntityConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
