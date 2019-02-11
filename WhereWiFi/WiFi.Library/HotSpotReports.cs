using System;
using System.Collections.Generic;
using WiFi.Library;

namespace Main
{
    public class HotSpotReports : HotSpotPanel
    {
        public List<HotSpotReports> listOfReports = new List<HotSpotReports>();

        public int CurrentHotSpotUsers { get; set; }
        public long IncomingTransfer { get; set; }
        public long OutgoingTransfer { get; set; }
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
                    CurrentHotSpotUsers =int.Parse(dividedPartOfString[2]),
                    IncomingTransfer=long.Parse(dividedPartOfString[3]),
                    OutgoingTransfer=long.Parse(dividedPartOfString[4])

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
    }
}