using System;
using System.IO;
using System.Reflection;
using Model;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.StartMenu();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\wifigdansk.csv");
            string[] files = File.ReadAllLines(path);

            foreach (var f in files)
            {
                Console.WriteLine($"your file{f}");
            }

        }
    }
}
