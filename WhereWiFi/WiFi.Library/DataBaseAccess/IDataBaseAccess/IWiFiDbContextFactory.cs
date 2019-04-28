namespace WiFi.Library.DataBaseAccess.IDataBaseAccess
{
    public interface IWiFiDbContextFactory
    {
        WiFiDbContext GetDbContext();
    }
}