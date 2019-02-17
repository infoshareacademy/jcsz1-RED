using System;
using System.IO;

namespace Main
{
    public class PathToFile
    {
        private readonly string _currentDirectory = Environment.CurrentDirectory;
        public string FullFilePath { get; }
        public string TransferReportFeb { get; }
        public string TransferReportMarch { get; }

        public PathToFile()
        {
            FullFilePath = Path.Combine(_currentDirectory, "Files", "wifigdansk.csv");
            TransferReportFeb = Path.Combine(_currentDirectory, "Files", "RT02.2017.csv");
            TransferReportMarch = Path.Combine(_currentDirectory, "Files", "RT03.2017.csv");
        }
    }

}