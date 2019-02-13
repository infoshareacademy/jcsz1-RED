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
        private static int _activeMenuPosition;

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

            var res = feb.MergingTwoLists(feb.listOfReports, march.listOfReports);
            var lowestHotSpotsUserNumber = res.OrderBy(r => r.CurrentHotSpotUsers);





            StartMenu(wifi, filePath, lowestHotSpotsUserNumber);
            Console.ReadLine();
        }

        private static readonly string[] _menuItems = {
            "Najbliższy HotSpot",
            "Dodaj HotSpot",
            "Lista HotSpotów",
            "Najmniej połączeń",
            "Podejrzanie duże transfery",
            "Edytuj Dodane Hotspoty",
            "Zakończ"
        };

        private static readonly string[] _menuOverloadItems =
        {
            "Dane wychodzące",
            "Dane Przychodzące",
            "Obydwa Parametry",
            "Wróć"
        };


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
            switch (_activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    InProgress("Najbliższy HotSpot");
                    break;
                case 1:
                    Console.Clear();
                    InProgress("Dodaj HotSpot");
                    panel.AddNewHotSpot(filePath.FullFilePath[0]);
                    break;
                case 2:
                    Console.Clear();
                    InProgress("Lista HotSpotów");
                    panel.ShowAllLocalizations();
                    break;
                case 3:
                    Console.Clear();
                    InProgress("Najmniej połączeń");
                    Console.WriteLine("Podaj liczbę interesujących Cię wyników");
                    var shortedList = repList.Take(int.Parse(Console.ReadLine()));
                    foreach (var r in shortedList)
                    {
                        Console.WriteLine($"ID: {r.Id}, Miejsce: {r.LocationName}, Średnia dzienna liczba użytkowników to: {r.CurrentHotSpotUsers}", CultureInfo.CurrentUICulture.TextInfo);
                    }
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    InProgress("Podejrzanie duże transfery");
                    var exitSubMenu = false;
                    do
                    {
                        _activeMenuPosition = 0;
                        ShowMenuOverloadTransfer();
                        exitSubMenu = PickOptionOverload();
                        RunOptionOverload();
                    } while (!exitSubMenu);
                    _activeMenuPosition = 0;
                    break;
                case 5:
                    Console.Clear();
                    InProgress("Edytuj Dodane Hotspoty");
                    panel.EditAddedHotspots();

                    break;
                case 6:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }

        private static void InProgress(string temporaryString)
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
                    _activeMenuPosition = (_activeMenuPosition > 0) ? _activeMenuPosition - 1 : _menuItems.Length - 1;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    _activeMenuPosition = (_activeMenuPosition < (_menuItems.Length - 1)) ? _activeMenuPosition + 1 : 0;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    _activeMenuPosition = _menuItems.Length - 1;
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
            for (int i = 0; i < _menuItems.Length; i++)
            {
                if (i == _activeMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("{0,-40}", _menuItems[i]);
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(_menuItems[i]);
                }
            }
        }

        private static void RunOptionOverload()
        {
            switch (_activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    InProgress("Podejrzanie duże transfery danych wychodzących");

                    break;
                case 1:
                    Console.Clear();
                    InProgress("Podejrzanie duże transfery danych przychodzących");
                    break;
                case 2:
                    Console.Clear();
                    InProgress("Podejrzanie duża łączna wymiana danych");
                    break;
                case 3:
                    Console.Clear();
                    InProgress("Wróć");
                    ShowMenu();
                    break;
            }
        }

        private static bool PickOptionOverload()
        {
            var exit = false;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    _activeMenuPosition = (_activeMenuPosition > 0) ? _activeMenuPosition - 1 : _menuOverloadItems.Length - 1;
                    ShowMenuOverloadTransfer();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    _activeMenuPosition = (_activeMenuPosition < (_menuOverloadItems.Length - 1)) ? _activeMenuPosition + 1 : 0;
                    ShowMenuOverloadTransfer();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    _activeMenuPosition = _menuOverloadItems.Length - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    exit = true;
                    break;
                }
            } while (true);

            return exit;
        }

        private static void ShowMenuOverloadTransfer()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Jaki parametr sieci najbardziej Cię niepokoi?");
            Console.WriteLine();
            for (int i = 0; i < _menuOverloadItems.Length; i++)
            {
                if (i == _activeMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("{0,-40}", _menuOverloadItems[i]);
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(_menuOverloadItems[i]);
                }
            }
        }
    }
}
