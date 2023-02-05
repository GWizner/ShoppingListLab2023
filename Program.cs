

using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection;

Dictionary<string, decimal> shoppingList = new Dictionary<string, decimal>();
shoppingList["Resident Evil IV"] = 59.99m;
shoppingList["Final Fantasy VII Remake"] = 59.99m;
shoppingList["Octopath Traveler II"] = 59.99m;
shoppingList["The Callisto Protocol"] = 59.99m;
shoppingList["Ghost of Tsushima"] = 59.99m;
shoppingList["God of War Ragnarok"] = 59.99m;
shoppingList["Sonic Frontiers"] = 59.99m;
shoppingList["Call of Duty: Modern Warfare II Cross-Gen Edition"] = 59.99m;

var byTitle = shoppingList.OrderBy(x => x.Key.ToLower().Replace("the ", ""))
                              .ToDictionary(x => x.Key, x => x.Value);
List<string> keyCart = new();
List<decimal> valueCart = new();



Console.WriteLine("Welcome to our game store. Here are our hot ticket items: ");
Console.WriteLine();

decimal total = 0;
int counter = 0;
int index = 0;
bool menu = true;


while (menu)
{
    foreach (var pair in byTitle)
    {
        if (counter == 0)
        {
            Thread.Sleep(500);
        }
        else
        {
            Thread.Sleep(250);
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(String.Format("{0, -50}", pair.Key));
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(String.Format("{0, 10}", pair.Value));
        Console.ResetColor();

    }    
    
    bool noMenu = true;
    while (noMenu)
    {

        Console.WriteLine("\nPlease select which game you would like to purchase: ");
        string userInput1 = Console.ReadLine().ToLower();

        string selectedKey = byTitle.Keys.FirstOrDefault(x => x.Equals(userInput1, StringComparison.OrdinalIgnoreCase));
        if (selectedKey != null)
        {
            counter++;

            foreach (var game in byTitle)
            {
                if (game.Key.Equals(selectedKey, StringComparison.OrdinalIgnoreCase))
                {
                    keyCart.Add(selectedKey);
                    valueCart.Add(byTitle[selectedKey]);
                    total = total + byTitle[selectedKey];
                }
            }

            Console.Write("\nOkay, I'll add that to your cart. ");

            bool purchaseLoop = true;
            while (purchaseLoop)
            {
                Console.WriteLine("Would you like to purchase another game (y/n)?\n");
                string yesNo = Console.ReadLine().ToLower();

                while (true)
                {
                    if (yesNo == "y")
                    {
                        Console.WriteLine("\nIf you would like to see the menu again, please input '1': ");
                        string userInput2 = Console.ReadLine();
                        menu = int.TryParse(userInput2, out int userChoice);

                        if (String.IsNullOrWhiteSpace(userInput2))
                        {
                            menu = false;
                        }
                        else if (userChoice == 1)
                        {
                            Console.WriteLine();
                            noMenu = false;
                            menu = true;
                        }
                        else
                        {
                            menu = false;
                        }
                        purchaseLoop = false;
                        break;
                    }
                    else if (yesNo == "n")
                    {
                        Console.WriteLine("\nSo you've got: \n");

                        for (int i = 0; i < keyCart.Count; i++)
                        {
                            Thread.Sleep(500);
                            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0, -50}", cultureInfo.TextInfo.ToTitleCase(keyCart[i])));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(String.Format("{0, 10}", valueCart[i]));
                            Console.ResetColor();
                        }
                        Thread.Sleep(800);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("--------------------------------------------------" +
                            "----------");
                        Console.Write(String.Format("{0, -50}", "Total: "));
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(String.Format("{0, 10}", total));
                        Console.ResetColor();

                        noMenu = false;
                        menu = false;
                        purchaseLoop = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nNrrrp! Try again!\n");
                        purchaseLoop = true;
                        break;
                    }
                }
            }
        }
        else
        {
            Console.Write($"\nNrrrp! \"{userInput1}\" don't exist! ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"(Check your spelling!)");
            Console.ResetColor();
        }
    }
}
Console.WriteLine("\n\nGoodbye!");
Console.WriteLine();
Console.WriteLine();
Thread.Sleep(700);




