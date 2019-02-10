using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WiFi.Library
{
    public class HotSpotPanel
    {
        public List<HotSpotPanel> listOfHotSpots = new List<HotSpotPanel>();
        public string Id { get; set; }
        public string LocationName { get; set; }
        public string LatitudeX { get; set; }
        public string LongitudeY { get; set; }

        public void OrganizeData(string[] table) // wyslana tablica
        {
            foreach (var t in table) // petla sprawdzajaca
            {
                var something = t.Split(',');
                var transferDataToList = new HotSpotPanel()
                {
                    Id = something[0],
                    LocationName = something[1],
                    LatitudeX = something[2],
                    LongitudeY = something[3],
                };
                listOfHotSpots.Add(transferDataToList);
            }

        }
        public void AddNewHotSpot(string totalPath)
        {
            HotSpotPanel newHotSpot = new HotSpotPanel();

            newHotSpot.Id = "HotspotAddByUser";
            Console.WriteLine("Podaj nazwę nowego HotSpotu:");
            newHotSpot.LocationName = Console.ReadLine();
            Console.WriteLine("Podaj szerokość geograficzną na jakiej znajduje się HotSpot (w formacie 54.382059)");
            newHotSpot.LatitudeX = Console.ReadLine(); //należy dopisać jakiś test na to, żeby nie wpadały bzdury
            Console.WriteLine("Podaj długość geograficzną na jakiej znajduje się HotSpot (w formacie 18.571996)");
            newHotSpot.LongitudeY = Console.ReadLine(); //należy dopisać jakiś test na to, żeby nie wpadały bzdury

            listOfHotSpots.Add(newHotSpot);

            List<string> output = new List<string>();
            output.Add($"{newHotSpot.Id},{newHotSpot.LocationName},{newHotSpot.LongitudeY},{newHotSpot.LongitudeY}");

            
            File.AppendAllLines(totalPath, output);

        }
        public void ShowListOfAllLocations()
        {
            //List<HotSpotPanel> hotSpotLocations
            foreach (var hotSpot in listOfHotSpots)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{hotSpot.LocationName,-50}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{hotSpot.LatitudeX,-30}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{hotSpot.LongitudeY}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            Console.ReadKey();
        }

    }
}
