using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiFi.Library.Models.ModelsForDB;

namespace WiFi.Library.DataBaseAccess
{
    class HotSpotUsersFavoritesConfiguration : IEntityTypeConfiguration<HotSpotUserFavoriteDbModel>
    {
        public void Configure(EntityTypeBuilder<HotSpotUserFavoriteDbModel> builder)
        {
            builder
                .ToTable("HotSpotUsersFavorites");
            builder
                .HasKey(x => x.HotSpotId);
            builder
                .Property(x => x.HotSpotId)
                .HasColumnName("HotSpotId")
                .IsRequired();
            builder
                .Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();
        }
    }
}
