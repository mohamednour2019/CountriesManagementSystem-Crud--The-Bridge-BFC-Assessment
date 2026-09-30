using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CountriesManagementSystem.Domain.Entities;

namespace CountriesManagementSystem.Infrastructure.EntitiesConfig
{
    internal class CityEntityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            builder.HasOne(x => x.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
