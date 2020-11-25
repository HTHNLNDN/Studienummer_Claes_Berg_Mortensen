using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using System;

namespace Studienummer_Claes_Berg_Mortensen
{
    public class StregsystemCLI : IStregsystemUI
    {
        bool stayAlive;
        string purchaceSyntax = "";
        string systemTitle = "TREOENS STREGSYSTEM: Den virtuelle stue";

        public event StregsystemEvent CommandEntered;

        public void Close()
        {
            stayAlive = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"Admin kommandoen {adminCommand} kunne ikke findes, tjek om alt er stavet rigtigt");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"Der skete en fejl: {errorString}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"Bruger {user.Username} har {user.Balance} stregdollars på kontoen men skal bruge {product.Price} for at købe {product.Name}, køb annulleret");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Der er blevet indtastet for mange argumenter, syntax for køb er: {purchaceSyntax}");
        }

        public void DisplayuProductNotFound(string product)
        {
            Console.WriteLine($"Produkt med ID: {product} kunne ikke findes, prøv igen.");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"Du har lige købt {transaction}");
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"Du har lige købt {count} x {transaction}");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"Bruger: {user}");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"Brugeren {username} kunne ikke findes i systemet, tjek om navnet er skrevet rigtigt");
        }

        public void Start()
        {
            stayAlive = true;

            while (stayAlive)
            {
                string input = Console.ReadLine();
            }
        }
        public void Menu()
        {
            //find en eller anden måde at tegne en menu på lol
        }
    }
}
