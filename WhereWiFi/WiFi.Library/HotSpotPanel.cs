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
                for (int i = 100; i < listOfHotSpots.Count; i++)
                {
                    Console.WriteLine($"{++counter}: {listOfHotSpots[i].Id}" +
                        $"  {listOfHotSpots[i].LocationName}" +
                        $"  {listOfHotSpots[i].LatitudeX}" +
                        $"  {listOfHotSpots[i].LongitudeY}");
                }
                counter = 0;
            }
        }
        public void EditAddedHotspots()
        {
            ShowOnlyAddedHotSpots();
            Console.WriteLine("Podaj numer hotspot który chcesz edytować i nacisnij enter\n" +
                              "'Enter' aby wrócić");
            if (int.TryParse(Console.ReadLine(), out var numberOfObjectOnList))     //gdyby dodać wybór strzałkami, ten if będzie zbędny
            {
                numberOfObjectOnList += 99;
                bool isTrue = false;
                while (isTrue == false)
                {
                    Console.WriteLine("Zmiana lokalizacji wybierz '1',\n zmiana szerokości geograficznej wybierz '2',\n zmiana wysokości geograficznej wybierz '3'");
                    if (int.TryParse(Console.ReadLine(), out var pickWhatToChange))
                    {
                        switch (pickWhatToChange)
                        {
                            case 1:
                                Console.WriteLine("Podaj nowy adres HotSpotu:");
                                listOfHotSpots[numberOfObjectOnList].LocationName = Console.ReadLine();
                                break;
                            case 2:
                                Console.WriteLine("Podaj szerokość geograficzną na jakiej znajduje się HotSpot (w formacie 54.382059)");
                                if (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out var change))
                                {
                                    Console.WriteLine("Podano zły typ danych!");
                                }
                                listOfHotSpots[numberOfObjectOnList].LatitudeX = change;
                                break;
                            case 3:
                                Console.WriteLine("Podaj długość geograficzną na jakiej znajduje się HotSpot (w formacie 18.571996)");
                                if (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out var change1))
                                {
                                    Console.WriteLine("Podano zły typ danych!");
                                }
                                listOfHotSpots[numberOfObjectOnList].LongitudeY = change1;
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine("Czy chcesz kontynuować i zmienić coś jeszcze? ['n' aby opuścić edytowanie]");
                        if (Console.ReadLine() == "n")
                        {
                            isTrue = true;
                        }
                    }
                    else Console.WriteLine("Podano zły typ danych!");
                }
            }
            else Console.WriteLine("Podano zły typ danych!");
        }
    }
}
