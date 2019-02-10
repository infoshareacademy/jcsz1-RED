using System;
using System.IO;

namespace Main
{
    public class PathToFile
    {
        private string currentDirectory = Environment.CurrentDirectory;
        string[] _fullFilePath;
        public string[] FullFilePath { get => _fullFilePath;}
        // nowy prop z getem który będzie przetrzymywał sciezke do transferow
        public PathToFile()
        {
            _fullFilePath = Directory.GetFiles(currentDirectory.Remove(currentDirectory.Length - 28, 28), "wifigdansk.csv", SearchOption.AllDirectories);
            //trzeba dodac jeszcze jedna sciezke do transferów
        }
    }

}