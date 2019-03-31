using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using seeWifi.Interfaces;
using seeWifi.Models;

namespace seeWifi.Services
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
                    Id = dividedPartOfString[0],
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
                Id = kolummny[0],
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

    }
}
