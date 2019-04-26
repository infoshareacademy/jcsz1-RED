namespace WiFi.Library.Models.ModelsForDB
{
    public class HotSpotLocationDbModel
    {
        public int Identifier { get; set; }
        public int HotSpotId { get; set; }
        public string LocationName { get; set; }
        public decimal LatitudeX { get; set; }
        public decimal LongitudeY { get; set; }
    }
}
