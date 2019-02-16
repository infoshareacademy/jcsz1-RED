using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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
            newHotSpot.LatitudeX = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Podaj długość geograficzną na jakiej znajduje się HotSpot (w formacie 18.571996)");
            newHotSpot.LongitudeY = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
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
            counter = 0;
            Console.ReadKey();
        }
        private void ShowOnlyAddedHotSpots()
        {
            if (listOfHotSpots.Count > 100)
            {
                Console.WriteLine();

                for (int i = 100; i < listOfHotSpots.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{++counter}{null,-1}:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{listOfHotSpots[i].LocationName,-10}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"  {listOfHotSpots[i].LatitudeX,-20}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  {listOfHotSpots[i].LongitudeY}");
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine();
                counter = 0;
            }
        }

        public void EditAddedHotspots()
        {
            ShowOnlyAddedHotSpots();
            Console.WriteLine("Podaj numer hotspot który chcesz edytować i nacisnij enter");
            var numberOfObjectOnList = int.Parse(Console.ReadLine()) + 99;
            bool isTrue = false;
            var pickWhatToChange = 0;
            while (isTrue == false)
            {
                Console.WriteLine("Zmiana lokalizacji wybierz 1,\n zmiana szerokości geograficznej wybierz 2,\n zmiana wysokości geograficznej wybierz3");
                pickWhatToChange = int.Parse(Console.ReadLine());
                switch (pickWhatToChange)
                {
                    case 1:
                        Console.WriteLine("Podaj nowy adres HotSpotu:");
                        listOfHotSpots[numberOfObjectOnList].LocationName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Podaj szerokość geograficzną na jakiej znajduje się HotSpot (w formacie 54.382059)");
                        listOfHotSpots[numberOfObjectOnList].LatitudeX = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    case 3:
                        Console.WriteLine("Podaj długość geograficzną na jakiej znajduje się HotSpot (w formacie 18.571996)");
                        listOfHotSpots[numberOfObjectOnList].LongitudeY = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Czy chcesz kontynuować i zmienić coś jeszcze? [y/n]");
                if (Console.ReadLine() == "n")
                {
                    isTrue = true;
                }
            }
        }
    }
}
