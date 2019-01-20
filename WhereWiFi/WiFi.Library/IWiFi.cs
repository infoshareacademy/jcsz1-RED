using System.Collections.Generic;

namespace WiFi.Library
{
   
    public interface IWiFi
    {
        List<bool> CheckNetworkProfiles();
        bool AddHotSpot();
        int NearestHotSpot();
    }


}
