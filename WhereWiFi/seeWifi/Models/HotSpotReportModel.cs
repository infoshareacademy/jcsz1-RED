namespace seeWifi.Models
{
    public class HotSpotReportModel:HotSpotModel
    {
        public double CurrentHotSpotUsers { get; set; }
        public double IncomingTransfer { get; set; }
        public double OutgoingTransfer { get; set; }
    }
}
