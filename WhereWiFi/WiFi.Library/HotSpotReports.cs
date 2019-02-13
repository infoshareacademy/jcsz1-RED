using System;
using System.Collections.Generic;
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
                    CurrentHotSpotUsers =double.Parse(dividedPartOfString[2]),
                    IncomingTransfer=double.Parse(dividedPartOfString[3]),
                    OutgoingTransfer=double.Parse(dividedPartOfString[4])

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
                    if (f.Id==v.Id)
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


        // Metoda do podawania podejrzanie dużych transferów ze względu na dane przychodzące
        public List<HotSpotReports> ShowOverloadHotSpotByIncomingTransfer(List<HotSpotReports> itemOne, List<HotSpotReports> itemTwo)
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

        // Metoda do podawania podejrzanie dużych transferów ze względu na dane wychodzące
        public List<HotSpotReports> ShowOverloadHotSpotByOutgoingTransfer(List<HotSpotReports> itemOne, List<HotSpotReports> itemTwo)
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

        // Metoda do podawania podejrzanie dużych transferów wychodzących i przychodzących
        public List<HotSpotReports> ShowOverloadHotSpotByOutgoingAndInTransfer(List<HotSpotReports> itemOne, List<HotSpotReports> itemTwo)
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






    }
}