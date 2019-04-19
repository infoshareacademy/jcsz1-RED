using System.ComponentModel.DataAnnotations;


namespace WiFi.Library.Models
{
    public class HotSpotModel
    {
        public int Number { get; set; }
        [Required(ErrorMessage = "Uzupełnij pole id, przykładowo LOK000")]
        public string fakeID { get; set; }
        [Required(ErrorMessage = "Podaj nazwę ulicy")]
        public string LocationName { get; set; }
        [Required(ErrorMessage = "Podaj szerokość geograficzną w formacie 54.XXXXX")]
        public string LatitudeX { get; set; }
        [Required(ErrorMessage = "Podaj długość geograficzną w formacie 18.XXXXX")]
        public string LongitudeY { get; set; }
        public bool FavoriteHotSpot { get; set; }
    }
}
