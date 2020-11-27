using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.Events;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.Interfaces
{
    public delegate void CommandEntered(CommandEnteredArgs e);
    /// <summary>
    /// IStregsystemUI acts as an interface for all UI, meaning all that is printed to the console
    /// </summary>
    public interface IStregsystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayuProductNotFound(string product);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash(User user, Product product);
        void DisplayGeneralError(string errorString);
        void DisplayPastTransactions(User user);
        void Start();
        event CommandEntered CommandEntered;
    }
}
