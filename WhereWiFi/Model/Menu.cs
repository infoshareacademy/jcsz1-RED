using System;

namespace Model
{
    public class Menu
    {
        private static string[] menuItems = { "Najbliższy HotSpot", "Dodaj HotSpot", "Lista HotSpotów", "Zakończ" };
        private static int activeMenuPosition = 0;

        public static void StartMenu()
        {
            Console.Title = "Where WiFi?";
            Console.CursorVisible = false;

            while (true)
            {
                ShowMenu();
                PickOption();
                RunOption();
            }
        }

        private static void RunOption()
        {
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    inProgress("Najbliższy HotSpot");
                    break;
                case 1:
                    Console.Clear();
                    inProgress("Dodaj HotSpot");
                    break;
                case 2:
                    Console.Clear();
                    inProgress("Lista HotSpotów");
                    break;
                case 3:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }

        private static void inProgress(string temporaryString)
        {
            Console.WriteLine(temporaryString);
            Console.ReadKey();
        }

        private static void PickOption()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    activeMenuPosition = (activeMenuPosition > 0) ? activeMenuPosition - 1 : menuItems.Length - 1;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activeMenuPosition = (activeMenuPosition < (menuItems.Length - 1)) ? activeMenuPosition + 1 : 0;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activeMenuPosition = menuItems.Length - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
        }

        private static void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Znajdź Swój HotSpot w Trójmieście :D");
            Console.WriteLine();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == activeMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("{0,-40}", menuItems[i]);
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(menuItems[i]);
                }
            }
        }
    }
}
