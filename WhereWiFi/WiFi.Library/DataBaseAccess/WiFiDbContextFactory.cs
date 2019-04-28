using WiFi.Library.DataBaseAccess.IDataBaseAccess;

namespace WiFi.Library.DataBaseAccess
{
    public class WiFiDbContextFactory : IWiFiDbContextFactory
    {
        public WiFiDbContext GetDbContext()
        {
            return new WiFiDbContext();
        }
    }
}
