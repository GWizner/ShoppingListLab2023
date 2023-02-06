using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ShoppingListLab2023
{
    class Program
    {

        public static void Main(string[] args)
        {

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

            List<Cart> cart = new List<Cart>();
           
            Console.WriteLine("Welcome to our game store. Here are our hot ticket items: ");
            Console.WriteLine();

            decimal total = 0;
            int counter = 0;
            int index = 0;
            bool menu = true;
            bool cashOut = false;


            while (menu)
            {
                foreach (var pair in byTitle)
                {
                    //if (counter == 0)
                    //{
                    //    Thread.Sleep(500);
                    //}
                    //else
                    //{
                    //    Thread.Sleep(250);
                    //}
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(String.Format("{0, -50}", pair.Key));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(String.Format("{0, 10:C}", pair.Value));
                    Console.ResetColor();

                }

                bool noMenu = true;
                while (noMenu)
                {

                    Console.Write("\nPlease select which game you would like to purchase ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"(Check your spelling!)");
                    Console.ResetColor();
                    Console.WriteLine(". To view your cart, input '2': ");
                    string userInput1 = Console.ReadLine().ToLower();

                    string selectedKey = byTitle.Keys.FirstOrDefault(x => x.Equals(userInput1, StringComparison.OrdinalIgnoreCase));
                    if (selectedKey != null)
                    {
                        counter++;

                        foreach (var game in byTitle)
                        {
                            if (game.Key.Equals(selectedKey, StringComparison.OrdinalIgnoreCase))
                            {
                                cart.Add(new Cart(selectedKey, byTitle[selectedKey]));
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
                                    Console.WriteLine("\nIf you would like to see the menu again, please input '1'. If you " +
                                        "would like to view your cart input '2': ");
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
                                    else if (userChoice == 2)
                                    {
                                        cashOut = false;
                                        ViewCart.currentCart(cart, total, cashOut);
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
                                    cashOut = true;
                                    ViewCart.currentCart(cart, total, cashOut);

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
                    else if (userInput1 == "2")
                    {
                        Console.WriteLine();
                        ViewCart.currentCart(cart, total, cashOut);
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
        }
    }
}




