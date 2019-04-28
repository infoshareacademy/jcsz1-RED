using System.ComponentModel.DataAnnotations;


namespace WiFi.Library.Models
{
    public class HotSpotModel
    {
        public int Number { get; set; }
        public string fakeID { get; set; }
        [Required(ErrorMessage = "Podaj nazwę ulicy")]
        [MinLength(5, ErrorMessage = "Nazwa ulicy powinna być dłuższa niż 5 znaków")]
        [MaxLength(70, ErrorMessage = "Nazwa ulicy powinna być krótsza niż 70 znaków")]
        public string LocationName { get; set; }
        [Required(ErrorMessage = "Podaj szerokość geograficzną")]
        [Range(54,55, ErrorMessage = "Obszar nieobsługiwany, podaj szerokość geograficzną w przedziale 54-55.")]
        public string LatitudeX { get; set; }
        [Required(ErrorMessage = "Podaj długość geograficzną")]
        [Range(18,19, ErrorMessage = "Obszar nieobsługiwany, podaj długość geograficzną w przedziale 18-19.")]
        public string LongitudeY { get; set; }
        public bool FavoriteHotSpot { get; set; }
    }
}
