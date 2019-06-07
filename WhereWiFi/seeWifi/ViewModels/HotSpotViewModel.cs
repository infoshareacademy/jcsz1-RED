using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace seeWifi.ViewModels
{
    public class HotSpotViewModel
    {
       public int Number { get; set; }

        public string fakeID { get; set; }

        [Required(ErrorMessage = "StreetName")]
        [MinLength(1, ErrorMessage = "StreetNameNotShorterThen")]
        [MaxLength(70, ErrorMessage = "StreetNameNotLongerThen")]
        public string LocationName { get; set; }

        
        [Required(ErrorMessage = "WhatLatitude")]
        [Range(54d, 55d, ErrorMessage = "OutOfRangeLatitude")]
        public string LatitudeX { get; set; }

        [Required(ErrorMessage = "WhatLongitude")]
        [Range(18d, 19d, ErrorMessage = "OutOfRangeLongitude")]
        public string LongitudeY { get; set; }
        public bool FavoriteHotSpot { get; set; }
    }
}
