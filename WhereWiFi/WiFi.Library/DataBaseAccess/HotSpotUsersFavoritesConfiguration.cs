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
                .Property(x => x.HotSpotNumber)
                .HasColumnName("HotSpotNumber")
                .IsRequired();

            builder
                .HasKey(x => x.Id);

            //builder
            //    .Property(x => x.UserId)
            //    .HasColumnName("UserId")
            //    .IsRequired();
        }
    }
}
