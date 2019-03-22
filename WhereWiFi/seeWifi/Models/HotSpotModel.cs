using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace seeWifi.Models
{
    public class HotSpotModel
    {
        public string Id { get; set; }
        public string LocationName { get; set; }
        public double LatitudeX { get; set; }
        public double LongitudeY { get; set; }
    }
}
