using System;
using System.IO;

namespace Main
{
    public class PathToFile
    {
        private string currentDirectory = Environment.CurrentDirectory;
        string[] _fullFilePath;
        string[] _transferReportFeb;
        string[] _transferReportMarch;
        public string[] FullFilePath { get => _fullFilePath; }
        public string[] TransferReportFeb { get => _transferReportFeb; }
        public string[] TransferReportMarch { get => _transferReportMarch; }

        public PathToFile()
        {
            _fullFilePath = Directory.GetFiles(currentDirectory.Remove(currentDirectory.Length - 28, 28), "wifigdansk.csv", SearchOption.AllDirectories);
            _transferReportFeb= Directory.GetFiles(currentDirectory.Remove(currentDirectory.Length - 28, 28), "RT02.2017.csv", SearchOption.AllDirectories);
            _transferReportMarch= Directory.GetFiles(currentDirectory.Remove(currentDirectory.Length - 28, 28), "RT03.2017.csv", SearchOption.AllDirectories);
        }
    }

}