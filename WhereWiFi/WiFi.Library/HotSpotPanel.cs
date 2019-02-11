using System;
using System.Collections.Generic;
using System.Globalization;
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
        public double LatitudeX { get; set; }
        public double LongitudeY { get; set; }
        private int counter = 0;

        public void OrganizeData(string[] table)
        {
            foreach (var t in table) 
            {
                var dividedPartOfString = t.Split(',');
                var transferDataToList = new HotSpotPanel()
                {
                    Id = dividedPartOfString[0],
                    LocationName = dividedPartOfString[1],
                    LatitudeX = double.Parse(dividedPartOfString[2], CultureInfo.InvariantCulture),
                    LongitudeY = double.Parse(dividedPartOfString[3], CultureInfo.InvariantCulture),
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
            newHotSpot.LatitudeX = float.Parse(Console.ReadLine()); //należy dopisać jakiś test na to, żeby nie wpadały bzdury
            Console.WriteLine("Podaj długość geograficzną na jakiej znajduje się HotSpot (w formacie 18.571996)");
            newHotSpot.LongitudeY = float.Parse(Console.ReadLine()); //należy dopisać jakiś test na to, żeby nie wpadały bzdury

            listOfHotSpots.Add(newHotSpot);

            List<string> output = new List<string>();
            output.Add($"{newHotSpot.Id},{newHotSpot.LocationName},{newHotSpot.LongitudeY},{newHotSpot.LongitudeY}");

            
            File.AppendAllLines(totalPath, output);

        }
        public void ShowAllLocalizations()
        {
            Console.WriteLine("lokalizacja,x,y");
            foreach (var hotSpot in listOfHotSpots)
            {
                Console.Write($"{++counter}: ");
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
