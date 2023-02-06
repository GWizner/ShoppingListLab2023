using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ShoppingListLab2023
{
    public class Cart
    {

        public string title { get; private set; }
        public decimal price { get; private set; }
        public bool checkedOut;

        public Cart(string newTitle, decimal newPrice)
        {
            title = newTitle;
            price = newPrice;
        }

    }
    public static class ViewCart
    {
        public static void currentCart(List<Cart> cart, decimal total, bool cashOut)
        {
            bool browse = true;
            bool badTitle = false;
            
            while (browse)
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    //Thread.Sleep(500);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(i + 1 + ".{0,-50}", cart[i].title);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0,10:C}", cart[i].price);
                    Console.ResetColor();
                }

                Thread.Sleep(800);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------------" +
                    "------------");
                Console.Write(String.Format("{0, -52}", "Total: "));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("{0, 10:C}", total));
                Console.ResetColor();

                if (!cashOut)
                {
                    Console.WriteLine("\nWould you like to remove any items from your cart (y/n)?\n");
                    string yesNo = Console.ReadLine();

                    if (yesNo == "y")
                    {
                        if (cart.Count == 0)
                        {
                            Console.WriteLine("\nYour cart is currently empty.");
                            browse = false;
                        }
                        else
                        {
                            Console.WriteLine("Please enter the name or item number of the game you would like to remove: ");
                            string userInputA = Console.ReadLine().ToLower();
                            browse = int.TryParse(userInputA, out int itemNo);

                            if (browse)
                            {
                                
                                for (int i = 0; i < cart.Count; i++)
                                {
                                    if (i + 1 == itemNo)
                                    {
                                        total = total - cart[i].price;
                                        cart.RemoveAt(i);
                                        badTitle = false;
                                        break;
                                    }
                                    else
                                    {
                                        browse = true;
                                        badTitle = true;
                                    }

                                }
                                if (badTitle == true)
                                {
                                    Console.WriteLine("\nItem not found in list.\n");
                                    browse = true;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cart.Count; i++)
                                {
                                    if (cart[i].title.Equals(userInputA, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        total = total - cart[i].price;
                                        cart.RemoveAt(i);
                                        browse = true;
                                        badTitle = false;
                                        break;
                                    }
                                    else
                                    {
                                        browse = true;
                                        badTitle = true;
                                    }
                                }
                                Console.WriteLine();

                                if (badTitle == true)
                                {
                                    Console.WriteLine("\nThat item is not in your cart.\n");
                                    browse = true;
                                }
                            }
                        }
                    }
                    else if (yesNo == "n")
                    {
                        browse = false;
                    }
                    else
                    {
                        Console.WriteLine("\nSorry, didn't catch that.\n");
                        browse = true;
                    }
                }
            }
        }
    }
}