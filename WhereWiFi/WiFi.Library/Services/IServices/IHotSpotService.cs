using System.Collections.Generic;
using WiFi.Library.Models;

namespace WiFi.Library.Services.IServices
{
    public interface IHotSpotService
    {
        List<HotSpotModel> GetAll();
        HotSpotModel AddHotSpot(HotSpotModel hotspot);
    }
}
