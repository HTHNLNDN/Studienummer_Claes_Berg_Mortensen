using Studienummer_Claes_Berg_Mortensen.Core;

namespace Studienummer_Claes_Berg_Mortensen.Interfaces
{
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
        void Start();
        event StregsystemEvent CommandEntered;
    }
}
