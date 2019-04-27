using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiFi.Library.Models.ModelsForDB;

namespace WiFi.Library.DataBaseAccess
{
    class HotSpotReportsConfiguration : IEntityTypeConfiguration<HotSpotReportDbModel>
    {
        public void Configure(EntityTypeBuilder<HotSpotReportDbModel> builder)
        {
            builder
                .ToTable("HotSpotReports");

            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(x => x.HotSpotId)
                .HasColumnName("HotSpotId");
            builder
                .Property(x => x.IncomingTransfer)
                .HasColumnName("IncomingTransfer");
            builder
                .Property(x => x.OutgoingTransfer)
                .HasColumnName("OutgoingTransfer");
            builder
                .Property(x => x.CurrentHotSpotUsers)
                .HasColumnName("CurrentHotSpotUsers");
        }
    }
}
