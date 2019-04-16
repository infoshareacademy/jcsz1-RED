using System;
using System.IO;
using System.Reflection;

namespace WiFi.Library.Filepath
{
    public class FilePath
    {
        private readonly string buildPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string FullFilePath { get; }
        public string TransferReportFeb { get; }
        public string TransferReportMarch { get; }

        public FilePath()
        {
            FullFilePath = buildPath + @"\data\wifigdansk.csv";
            TransferReportFeb = buildPath + @"\data\RT02.2017.csv";
            TransferReportMarch = buildPath + @"\data\RT03.2017.csv";
        }
    }

}