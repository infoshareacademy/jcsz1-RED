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
           // Menu.StartMenu();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"WiFi.Library\Files\wifigdansk.csv");
            string[] files = File.ReadAllLines(path);

            foreach (var f in files)
            {
                Console.WriteLine($"your file{f}");
            }
            //ok rozwiązanie jest nie do końca takie jakie miało być! mianowicie! plik wifigdansk mimo wszystko jest w:
            //jcsz1 - RED\WhereWiFi\Main\bin\Debug\netcoreapp2.1\WiFi.Library\Files
            //aale jest też w:
            //jcsz1-RED\WhereWiFi\WiFi.Library\Files
            Console.ReadLine();
        }
    }
}
