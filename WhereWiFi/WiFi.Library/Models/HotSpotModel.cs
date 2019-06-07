using System.ComponentModel.DataAnnotations;


namespace WiFi.Library.Models
{
    public class HotSpotModel
    {
        public int Number { get; set; }
        public string fakeID { get; set; }
        
        public string LocationName { get; set; }
        
        public string LatitudeX { get; set; }
    
        public string LongitudeY { get; set; }
        public bool FavoriteHotSpot { get; set; }
    }
}
