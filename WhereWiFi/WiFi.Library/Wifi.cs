using System.Collections.Generic;

namespace WiFi.Library
{
    // Logika biznesowa

    //wprowadzenie danych o punktach WiFi, tak aby system był zawsze aktualny
    //miejsca w których jest najmniej użytkowników na przestrzeni czasu, do wybrania z menu konsolowego
    //wprowadzenie danych z menu konsolowego
    public class Wifi : IWiFi
    {
        public string SSID { get; } //identyfikator ssid
        public List<bool> checkedNetworkProfiles = new List<bool>(); // lista sprawdzonych hot spotow

        public List<bool> CheckNetworkProfiles() //profil siecowy publiczny czy prywatny
        {
            throw new System.NotImplementedException();
            //if network profile posiada typ zabezpieczeń, pomiń i nie dodawaj do listy profili wifi
        }

        public bool AddHotSpot()
        {
            throw new System.NotImplementedException();
        }

        public int NearestHotSpot()
        {
            throw new System.NotImplementedException();
        }

}




}
