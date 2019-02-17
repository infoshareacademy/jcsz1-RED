using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WiFi.Library;

namespace Main
{
    public class HotSpotReports : HotSpotPanel
    {
        public List<HotSpotReports> listOfReports = new List<HotSpotReports>();

        public double CurrentHotSpotUsers { get; set; }
        public double IncomingTransfer { get; set; }
        public double OutgoingTransfer { get; set; }
        private int counter = 0;

        public void OrganizeReports(string[] table)
        {
            foreach (var t in table)
            {
                var dividedPartOfString = t.Split(',');
                var transferDataFromReports = new HotSpotReports()
                {
                    Id = dividedPartOfString[0],
                    LocationName = dividedPartOfString[1],
                    CurrentHotSpotUsers = double.Parse(dividedPartOfString[2]),
                    IncomingTransfer = double.Parse(dividedPartOfString[3]),
                    OutgoingTransfer = double.Parse(dividedPartOfString[4])
                };
                listOfReports.Add(transferDataFromReports);
            }
        }
        public void ShowTransferStatus()
        {
            Console.WriteLine("lokalizacja,liczba użytkowników,transfer przychodzący, transfer wychodzący");
            foreach (var hotSpot in listOfReports)
            {
                Console.Write($"{++counter}: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{hotSpot.LocationName,-50}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{hotSpot.CurrentHotSpotUsers,-30}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{hotSpot.IncomingTransfer,-30}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{hotSpot.OutgoingTransfer}");
            }
            Console.ReadKey();
        }

        public List<HotSpotReports> MergingTwoLists(List<HotSpotReports> itemOne, List<HotSpotReports> itemTwo)
        {
            List<HotSpotReports> resultList = new List<HotSpotReports>();
            foreach (var f in itemOne)
            {
                foreach (var v in itemTwo)
                {
                    if (f.Id == v.Id)
                    {
                        var smallInstance = new HotSpotReports()
                        {
                            Id = f.Id,
                            LocationName = f.LocationName,
                            CurrentHotSpotUsers = (f.CurrentHotSpotUsers + v.CurrentHotSpotUsers) / 2,
                            IncomingTransfer = (f.IncomingTransfer + v.IncomingTransfer) / 2,
                            OutgoingTransfer = (f.OutgoingTransfer + v.OutgoingTransfer) / 2
                        };
                        resultList.Add(smallInstance);
                    }
                }
            }

            return resultList;
        }

        // ========== DUŻE TRANSFERY ==========

        internal static HotSpotReports ParseCSV(string lines)
        {
            var kolummny = lines.Split(',');

            return new HotSpotReports
            {
                Id = kolummny[0],
                LocationName = kolummny[1],
                CurrentHotSpotUsers = double.Parse(kolummny[2]),
                IncomingTransfer = double.Parse(kolummny[3]),
                OutgoingTransfer = double.Parse(kolummny[4])
            };
        }

        private static List<HotSpotReports> LodingFiles(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Where(linia => linia.Length > 1)
                .Select(HotSpotReports.ParseCSV).ToList();
        }


        // Metoda do podawania podejrzanie dużych transferów ze względu na dane przychodzące

        public static void ShowOverloadHotSpotByIncomingTransfer()
        {
            Console.WriteLine($"{null,-15}PODEJRZANIE DUŻE TRANSFERY DANYCH PRZYCHODZĄCYCH");
            Console.WriteLine();

            var raportMarchfilePath = new PathToFile();
            var overloadHotSpotList = LodingFiles(raportMarchfilePath.TransferReportMarch);
            var queryToOverloadHotSpotList = overloadHotSpotList
                .OrderByDescending(s => s.IncomingTransfer)
                .Take(5);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"NAZWA LOKALIZACJI HOT SPOT {null,-23}");
            Console.Write($"TRANSFER PRZYCHODZĄCY");
            Console.WriteLine();

            foreach (var lines in queryToOverloadHotSpotList)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.OutputEncoding = Encoding.UTF8; // walczę z polskimi znakami
                Console.Write($"{lines.LocationName,-50}");
                Console.Write($"{lines.IncomingTransfer}");
                Console.WriteLine();
            }
            WornedForUser();
        }

        public static void ShowOverloadHotSpotByOutgoingTransfer()
        {
            Console.WriteLine($"{null,-15}PODEJRZANIE DUŻE TRANSFERY DANYCH WYCHODZĄCYCH");
            Console.WriteLine();

            var raportMarchfilePath = new PathToFile();
            var overloadHotSpotList = LodingFiles(raportMarchfilePath.TransferReportMarch);
            var queryToOverloadHotSpotList = overloadHotSpotList
                .OrderByDescending(s => s.OutgoingTransfer)
                .Take(5);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"NAZWA LOKALIZACJI HOT SPOT {null,-23}");
            Console.Write($"TRANSFER WYCHODZĄCY");
            Console.WriteLine();

            foreach (var lines in queryToOverloadHotSpotList)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.OutputEncoding = Encoding.UTF8; // walczę z polskimi znakami
                Console.Write($"{lines.LocationName,-50}");
                Console.Write($"{lines.OutgoingTransfer}");
                Console.WriteLine();
            }
            WornedForUser();
        }

        // Metoda do podawania podejrzanie dużych transferów wychodzących i przychodzących

        public static void ShowOverloadHotSpotByOutgoingAndInTransfer()
        {
            Console.WriteLine($"{null,-15}PODEJRZANIE DUŻA ŁĄCZNA WYMIANA DANYCH");
            Console.WriteLine();

            var raportMarchfilePath = new PathToFile();
            var overloadHotSpotList = LodingFiles(raportMarchfilePath.TransferReportMarch);
            var queryToOverloadHotSpotList = overloadHotSpotList
                .OrderByDescending(s => (s.IncomingTransfer+ s.OutgoingTransfer))
                .Take(5);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"NAZWA LOKALIZACJI HOT SPOT {null,-5}");
            Console.Write($"TRANSFER PRZYCHODZĄCY {null,-5}");
            Console.Write($"TRANSFER WYCHODZĄCY {null,-5}");
            Console.Write($"ŁĄCZNA WYMIANA DANYCH");
           
            Console.WriteLine();

            foreach (var lines in queryToOverloadHotSpotList)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.OutputEncoding = Encoding.UTF8; // walczę z polskimi znakami
                Console.Write($"{lines.LocationName,-40}");
                Console.Write($"{lines.IncomingTransfer, -25}");
                Console.Write($"{lines.OutgoingTransfer, - 25}");
                Console.Write($"{lines.OutgoingTransfer + lines.IncomingTransfer}");
                Console.WriteLine();
            }
            WornedForUser();
        }

        // Ostrzeżenie dla użytkownika
        public static void WornedForUser()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~:>PAMIĘTAJ<:~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Traktuj każdą nieznaną sieć jako podejrzaną.");
            Console.WriteLine("Używaj programów zabezpieczających przed wirusami i atakami.");
            Console.WriteLine("Kiedy korzystamy z tych sieci nie wykonuj żadnych transakcji bankowych.");
            Console.WriteLine("Na czas tych operacji skorzystaj z transferu danych na telefonie.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Wciśnij dowolny klawisz żeby wrócić do głównego menu");
        }

        // ========== KONIEC DUŻE TRANSFERY ==========
    }
}