using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WiFi.Library.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotSpotLocations",
                columns: table => new
                {
                    HotSpotId = table.Column<string>(nullable: false),
                    Identifier = table.Column<int>(nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    LatitudeX = table.Column<decimal>(nullable: false),
                    LongitudeY = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotSpotLocations", x => x.HotSpotId);
                });

            migrationBuilder.CreateTable(
                name: "HotSpotReports",
                columns: table => new
                {
                    Id = table.Column<double>(nullable: false),
                    CurrentHotSpotUsers = table.Column<double>(nullable: false),
                    IncomingTransfer = table.Column<double>(nullable: false),
                    OutgoingTransfer = table.Column<double>(nullable: false),
                    HotSpotId = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotSpotReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotSpotUsersFavorites",
                columns: table => new
                {
                    HotSpotId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotSpotUsersFavorites", x => x.HotSpotId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotSpotLocations");

            migrationBuilder.DropTable(
                name: "HotSpotReports");

            migrationBuilder.DropTable(
                name: "HotSpotUsersFavorites");
        }
    }
}
