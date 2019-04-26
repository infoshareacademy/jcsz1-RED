namespace WiFi.Library.Models.ModelsForDB
{
    public class HotSpotReportDbModel
    {
        public int CurrentHotSpotUsers { get; set; }
        public decimal IncomingTransfer { get; set; }
        public decimal OutgoingTransfer { get; set; }
        public int HotSpotId { get; set; }
        public int Id { get; set; }
    }
}
