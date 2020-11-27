using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.Events;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using System;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen
{
    public class StregsystemCLI : IStregsystemUI
    {
        bool stayAlive = false;
        string purchaceSyntax = "";
        string systemTitle = "TREOENS STREGSYSTEM: Den virtuelle stue";
        IStregsystem _stregsystem;

        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
        }

        public event Interfaces.CommandEntered CommandEntered;

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

        public void DisplayuProductNotFound(string productID)
        {
            Console.WriteLine($"Produkt med ID: {productID} kunne ikke findes, prøv igen.");
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
            Console.WriteLine($"Bruger: {user} ");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"Brugeren {username} kunne ikke findes i systemet, tjek om navnet er skrevet rigtigt");
        }
        public void DisplayPastTransactions(User user)
        {
            foreach (Transaction transaction in _stregsystem.GetTransactions(user, 10))
                Console.WriteLine(transaction);
        }

        public void Start()
        {
            Menu();
            _stregsystem.UserbalanceWarning += UserBalanceNotification;
            stayAlive = true;

            while (stayAlive)
            {
                Menu();
                CommandEntered(new CommandEnteredArgs(Console.ReadLine()));
            }
        }
        public void Menu()
        {
            Console.WriteLine($"________________________________________________________");
            Console.WriteLine($"{systemTitle}");
            Console.WriteLine($"________________________________________________________");
            foreach (Product product in _stregsystem.ActiveProducts()) Console.WriteLine(product);
            Console.WriteLine($"{Environment.NewLine}");
            //find en eller anden måde at tegne en menu på lol
        }
        public void UserBalanceNotification(UserBalanceNotificationArgs e)
        {
            Console.WriteLine("Lmao u need to put money on your account peasant");
        }
    }
}
