using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using WiFi.Library.Models.ModelsForDB;
using WiFi.Library.Models.RestModels;

namespace WiFi.Library.DataBaseAccess
{
    public class WiFiDbContext : DbContext
    {
        //public DbSet<HotSpotLocationDbModel> HotSpotLocations { get; set; }
        //public DbSet<HotSpotReportDbModel> HotSpotReports{ get; set; }
        public DbSet<HotSpotUserFavoriteDbModel> HotSpotUsersFavorites { get; set; }
        public DbSet<ApplicationUserDbModel> ApplicationUser { get; set; }
        public DbSet<RestReportsModel> RestReports { get; set; }

        private static ILoggerFactory GetFactory()
        {
            return new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.None,true)
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(GetFactory())
                .UseSqlServer(@"Server=(localdb)\bazaS;Database=BazaRed;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new HotSpotLocationsConfiguration());
            //modelBuilder.ApplyConfiguration(new HotSpotReportsConfiguration());
            modelBuilder.ApplyConfiguration(new HotSpotUsersFavoritesConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

   
}
