using System;
using System.IO;

namespace WiFi.Library.Filepath
{
    public class PathToFile
    {
        private readonly string _currentDirectory = Environment.CurrentDirectory;
        public string FullFilePath { get; }
        public string TransferReportFeb { get; }
        public string TransferReportMarch { get; }

        public PathToFile()
        {
            FullFilePath = AppDomain.CurrentDomain.BaseDirectory;
            TransferReportFeb = Path.Combine(_currentDirectory, "data", "RT02.2017.csv");
            TransferReportMarch = Path.Combine(_currentDirectory, "data", "RT03.2017.csv");
            

                //Path.Combine(_currentDirectory, "data", "wifigdansk.csv");
        }
    }

}