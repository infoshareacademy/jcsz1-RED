using System.ComponentModel.DataAnnotations;


namespace WiFi.Library.Models
{
    public class HotSpotModel
    {
        public int Number { get; set; }
        public string fakeID { get; set; }
        [Required(ErrorMessage = "Podaj nazwę ulicy")]
        public string LocationName { get; set; }
        [Required]
        [Range(54,55, ErrorMessage = "Podaj szerokość geograficzną w przedziale 54-55")]
        public string LatitudeX { get; set; }
        [Required]
        [Range(18,19, ErrorMessage = "Podaj długość geograficzną w przedziale 18-19")]
        public string LongitudeY { get; set; }
        public bool FavoriteHotSpot { get; set; }
    }
}
