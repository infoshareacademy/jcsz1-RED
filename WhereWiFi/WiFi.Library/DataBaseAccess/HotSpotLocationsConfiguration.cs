using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiFi.Library.Models.ModelsForDB;

namespace WiFi.Library.DataBaseAccess
{
    class HotSpotLocationsConfiguration : IEntityTypeConfiguration<HotSpotLocationDbModel>
    {
        public void Configure(EntityTypeBuilder<HotSpotLocationDbModel> builder)
        {
            builder
                .ToTable("HotSpotLocations");

            builder
                .HasKey(x => x.HotSpotId);

            builder
                .Property(x => x.HotSpotId)
                .HasColumnName("HotSpotId")
                .IsRequired();

            builder
                .Property(x => x.Identifier)
                .HasColumnName("Identifier");

            builder
                .Property(x => x.LocationName)
                .HasColumnName("LocationName")
                .IsRequired();

            builder
                .Property(x => x.LatitudeX)
                .HasMaxLength(8)
                .HasColumnName("LatitudeX")
                .IsRequired();

            builder
                .Property(x => x.LongitudeY)
                .HasMaxLength(8)
                .HasColumnName("LongitudeY")
                .IsRequired();
        }
    }
}
