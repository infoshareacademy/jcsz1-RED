namespace WiFi.Library.Models
{
    public class HotSpotReportModel : HotSpotModel
    {
        public double CurrentHotSpotUsers { get; set; }
        public double IncomingTransfer { get; set; }
        public double OutgoingTransfer { get; set; }
        public bool SuspiciousByIncomingTransfer { get; set; }
        public bool SuspiciousByOutgoingTransfer { get; set; }
        public bool SuspiciousByTotal { get; set; }

    }
}
