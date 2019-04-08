using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WiFi.Library.Models;
using WiFi.Library.Services.IServices;

namespace WiFi.Library.Services
{
    public class ReportsService : IReportsService
    {

        public List<HotSpotReportModel> ListOfReports { get; set; }
        private readonly string path = "./data/RT02.2017.csv";

        public ReportsService()
        {
            ListOfReports = LoadingFiles(path);
        }

        public void OrganizeReports(string[] table)
        {
            foreach (var t in table)
            {
                var dividedPartOfString = t.Split(',');
                var transferDataFromReports = new HotSpotReportModel()
                {
                    fakeID = dividedPartOfString[0],
                    LocationName = dividedPartOfString[1],
                    CurrentHotSpotUsers = double.Parse(dividedPartOfString[2]),
                    IncomingTransfer = double.Parse(dividedPartOfString[3]),
                    OutgoingTransfer = double.Parse(dividedPartOfString[4])
                };
                ListOfReports.Add(transferDataFromReports);
            }
        }


        internal HotSpotReportModel ParseCSV(string lines)
        {
            var kolummny = lines.Split(',');

            return new HotSpotReportModel
            {
                fakeID = kolummny[0],
                LocationName = kolummny[1],
                CurrentHotSpotUsers = double.Parse(kolummny[2]),
                IncomingTransfer = double.Parse(kolummny[3]),
                OutgoingTransfer = double.Parse(kolummny[4])
            };
        }

        private List<HotSpotReportModel> LoadingFiles(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.UTF8)
                .Where(linia => linia.Length > 1)
                .Select(ParseCSV).ToList();
        }


        // Metoda do podawania podejrzanie dużych transferów ze względu na dane przychodzące

        public  List<HotSpotReportModel> GetSuspiciousHotSpotByIncomingTransfer()
        {
            var listByIncomingTransfer = ListOfReports
                .OrderByDescending(s => s.IncomingTransfer)
                .Take(5)
                .ToList();
            return listByIncomingTransfer;
        }

        public List<HotSpotReportModel> GetSuspiciousHotSpotByOutGoingTransfer()
        {
            var listByOutGoingTransfer = ListOfReports
                .OrderByDescending(s => s.OutgoingTransfer)
                .Take(5).ToList();
            return listByOutGoingTransfer;
        }

        public List<HotSpotReportModel> GetSuspiciousHotSpotByTotalTransfer()
        {
            var listByTotalTransfer = ListOfReports
                .OrderByDescending(s => (s.IncomingTransfer + s.OutgoingTransfer))
                .Take(5).ToList();

            return listByTotalTransfer;
        }


        
        public List<HotSpotReportModel> GetSuspiciousHotSpotsList()
        {
            var listByOutGoingTransfer = GetSuspiciousHotSpotByOutGoingTransfer();
            var listByIncomingTransfer = GetSuspiciousHotSpotByIncomingTransfer();
            var listByTotalTransfer = GetSuspiciousHotSpotByTotalTransfer();


            var listOfSuspiciousHotSpots =
                listByOutGoingTransfer.Union(listByIncomingTransfer).Union(listByTotalTransfer).ToList();

            var idListByOutGoingTransfer = listByOutGoingTransfer.Select(x => x.fakeID).ToList();
            var idListByIncomingTransfer = listByIncomingTransfer.Select(x => x.fakeID).ToList();
            var idListByTotalTransfer = listByTotalTransfer.Select(x => x.fakeID).ToList();

            foreach (var hotSpot in listOfSuspiciousHotSpots)
            {
                hotSpot.SuspiciousByIncomingTransfer = idListByIncomingTransfer.Contains(hotSpot.fakeID);
                hotSpot.SuspiciousByOutgoingTransfer = idListByOutGoingTransfer.Contains(hotSpot.fakeID);
                hotSpot.SuspiciousByTotal = idListByTotalTransfer.Contains(hotSpot.fakeID);
            }
            return listOfSuspiciousHotSpots;
        }


        //// Ostrzeżenie dla użytkownika
        //public static void WornedForUser()
        //{
        //    Console.WriteLine();
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~:>PAMIĘTAJ<:~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        //    Console.WriteLine("Traktuj każdą nieznaną sieć jako podejrzaną.");
        //    Console.WriteLine("Używaj programów zabezpieczających przed wirusami i atakami.");
        //    Console.WriteLine("Kiedy korzystamy z tych sieci nie wykonuj żadnych transakcji bankowych.");
        //    Console.WriteLine("Na czas tych operacji skorzystaj z transferu danych na telefonie.");
        //    Console.WriteLine();
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine("Wciśnij dowolny klawisz żeby wrócić do głównego menu");
        //}

        //// ========== KONIEC DUŻE TRANSFERY ==========
    }







}

