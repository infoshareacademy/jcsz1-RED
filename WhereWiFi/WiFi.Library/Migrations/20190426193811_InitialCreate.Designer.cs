﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WiFi.Library.DataBaseAccess;

namespace WiFi.Library.Migrations
{
    [DbContext(typeof(WiFiDataBaseContext))]
    [Migration("20190426193811_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WiFi.Library.Models.ModelsForDB.HotSpotLocationDbModel", b =>
                {
                    b.Property<string>("HotSpotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("HotSpotId");

                    b.Property<int>("Identifier");

                    b.Property<decimal>("LatitudeX");

                    b.Property<string>("LocationName");

                    b.Property<decimal>("LongitudeY");

                    b.HasKey("HotSpotId");

                    b.ToTable("HotSpotLocations");
                });

            modelBuilder.Entity("WiFi.Library.Models.ModelsForDB.HotSpotReportDbModel", b =>
                {
                    b.Property<double>("Id");

                    b.Property<double>("CurrentHotSpotUsers");

                    b.Property<double>("HotSpotId");

                    b.Property<double>("IncomingTransfer");

                    b.Property<double>("OutgoingTransfer");

                    b.HasKey("Id");

                    b.ToTable("HotSpotReports");
                });

            modelBuilder.Entity("WiFi.Library.Models.ModelsForDB.HotSpotUserFavoriteDbModel", b =>
                {
                    b.Property<int>("HotSpotId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId");

                    b.HasKey("HotSpotId");

                    b.ToTable("HotSpotUsersFavorites");
                });
#pragma warning restore 612, 618
        }
    }
}
