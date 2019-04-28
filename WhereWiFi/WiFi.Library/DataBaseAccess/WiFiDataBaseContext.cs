using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using WiFi.Library.Models.ModelsForDB;

namespace WiFi.Library.DataBaseAccess
{
    class WiFiDataBaseContext: DbContext
    {
        //public DbSet<HotSpotLocationDbModel> HotSpotLocations { get; set; }
        //public DbSet<HotSpotReportDbModel> HotSpotReports{ get; set; }
        public DbSet<HotSpotUserFavoriteDbModel> HotSpotUsersFavorites { get; set; }

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
                .UseSqlServer(@"Server=(localdb)\bazaS;Database=Baza5;Trusted_Connection=True;");
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
