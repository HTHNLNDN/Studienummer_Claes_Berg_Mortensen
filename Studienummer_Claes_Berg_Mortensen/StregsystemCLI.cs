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

        /// <summary>
        /// Closes the program(stregsystem)
        /// </summary>
        public void Close()
        {
            stayAlive = false;
        }

        /// <summary>
        /// Prints that an admin command was not found to the console
        /// </summary>
        /// <param name="adminCommand"></param>
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"Admin kommandoen {adminCommand} kunne ikke findes, tjek om alt er stavet rigtigt");
        }

        /// <summary>
        /// Prints that a eneral error occourred to the console
        /// </summary>
        /// <param name="errorString"></param>
        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"Der skete en fejl: {errorString}");
        }

        /// <summary>
        /// Prints that a user does not have enough credits to buy a certain product to the console
        /// </summary>
        /// <param name="user"></param>
        /// <param name="product"></param>
        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"Bruger {user.Username} har {user.Balance} stregdollars på kontoen men skal bruge {product.Price} for at købe {product.Name}, køb annulleret");
        }

        /// <summary>
        /// Prints to the console that there were too many argumants passed
        /// </summary>
        /// <param name="command"></param>
        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Der er blevet indtastet for mange argumenter, syntax for køb er: {purchaceSyntax}");
        }

        /// <summary>
        /// Prints to the console that a certain product wasn't found
        /// </summary>
        /// <param name="productID"></param>
        public void DisplayuProductNotFound(string productID)
        {
            Console.WriteLine($"Produkt med ID: {productID} kunne ikke findes, prøv igen.");
        }

        /// <summary>
        /// Prints to the console what product a user bought, as well as how many credits that user now has on their account
        /// </summary>
        /// <param name="transaction"></param>
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"Du har lige købt {transaction}");
        }

        /// <summary>
        /// Prints to the console how many of a product a user bought, in case of multiple buy, as well as how much money is now on their account
        /// </summary>
        /// <param name="count"></param>
        /// <param name="transaction"></param>
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"Du har lige købt {count} x {transaction}");
        }

        /// <summary>
        /// Print to the console a users info, containing name and balance
        /// </summary>
        /// <param name="user"></param>
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"Bruger: {user} ");
        }

        /// <summary>
        /// prints to the console that the username which was passed does not exist in the system
        /// </summary>
        /// <param name="username"></param>
        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"Brugeren {username} kunne ikke findes i systemet, tjek om navnet er skrevet rigtigt");
        }

        /// <summary>
        /// Prints to the console the latest 10 transactions a user has made
        /// </summary>
        /// <param name="user"></param>
        public void DisplayPastTransactions(User user)
        {
            foreach (Transaction transaction in _stregsystem.GetTransactions(user, 10))
                Console.WriteLine(transaction);
        }

        /// <summary>
        /// starts the program by printing the menu and initiating all variables
        /// </summary>
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

        /// <summary>
        /// Prints the menu onto the console
        /// </summary>
        public void Menu()
        {
            Console.WriteLine($"________________________________________________________");
            Console.WriteLine($"{systemTitle}");
            Console.WriteLine($"________________________________________________________");
            foreach (Product product in _stregsystem.ActiveProducts()) Console.WriteLine(product);
            Console.WriteLine($"{Environment.NewLine}");
            //find en eller anden måde at tegne en menu på lol
        }

        /// <summary>
        /// prints to the console if a users balance is below 50 credits, warning them that their balance is low
        /// </summary>
        /// <param name="e"></param>
        public void UserBalanceNotification(UserBalanceNotificationArgs e)
        {
            Console.WriteLine("Du har under 50 stregdollars på din konto, overvej at indsætte penge");
        }
    }
}
