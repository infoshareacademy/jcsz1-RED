using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WiFi.Library;


namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {

            PathToFile filePath = new PathToFile();
            var fileWifiGdansk = File.ReadAllLines(filePath.FullFilePath[0]);
            var fileReportFeb = File.ReadAllLines(filePath.TransferReportFeb[0]);
            var fileReportMarch = File.ReadAllLines(filePath.TransferReportMarch[0]);

            HotSpotPanel wifi = new HotSpotPanel();
            wifi.OrganizeData(fileWifiGdansk);
            HotSpotReports feb = new HotSpotReports();
            feb.OrganizeReports(fileReportFeb);
            HotSpotReports march = new HotSpotReports();
            march.OrganizeReports(fileReportMarch);
            
            var res = feb.MergingTwoLists(feb.listOfReports,march.listOfReports);
            var lowestHotSpotsUserNumber = res.OrderBy(r => r.CurrentHotSpotUsers).Take(20);
            
          
            


            StartMenu(wifi, filePath, lowestHotSpotsUserNumber);
            Console.ReadLine();
        }
        private static string[] menuItems = { "Najbliższy HotSpot", "Dodaj HotSpot",
            "Lista HotSpotów", "Najmniej połączeń", "Zakończ" };
        private static int activeMenuPosition = 0;
        public static void StartMenu(HotSpotPanel panel, PathToFile filePath, IEnumerable<HotSpotReports> repList)
        {
            Console.Title = "Where WiFi?";
            Console.CursorVisible = false;

            while (true)
            {
                ShowMenu();
                PickOption();
                RunOption(panel, filePath, repList);
            }
        }
        private static void RunOption(HotSpotPanel panel, PathToFile filePath, IEnumerable<HotSpotReports> repList)
        {
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    inProgress("Najbliższy HotSpot");
                    break;
                case 1:
                    Console.Clear();
                    inProgress("Dodaj HotSpot");
                    panel.AddNewHotSpot(filePath.FullFilePath[0]);
                    break;
                case 2:
                    Console.Clear();
                    inProgress("Lista HotSpotów");
                    panel.ShowAllLocalizations();
                    break;
                case 3:
                    Console.Clear();
                    inProgress("Najmniej połączeń");
                    foreach (var r in repList)
                    {
                        Console.WriteLine($"ID: {r.Id}, Miejsce: {r.LocationName}, Średnia dzienna liczba użytkowników to: {r.CurrentHotSpotUsers}", CultureInfo.CurrentUICulture.TextInfo);
                    }
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }

        private static void inProgress(string temporaryString)
        {
            Console.WriteLine(temporaryString);
        }

        private static void PickOption()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    activeMenuPosition = (activeMenuPosition > 0) ? activeMenuPosition - 1 : menuItems.Length - 1;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activeMenuPosition = (activeMenuPosition < (menuItems.Length - 1)) ? activeMenuPosition + 1 : 0;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activeMenuPosition = menuItems.Length - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
        }

        private static void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Znajdź Swój HotSpot w Trójmieście :D");
            Console.WriteLine();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == activeMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("{0,-40}", menuItems[i]);
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(menuItems[i]);
                }
            }
        }
    }
}
