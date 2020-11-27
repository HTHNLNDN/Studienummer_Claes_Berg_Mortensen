using Studienummer_Claes_Berg_Mortensen.Core;
using Studienummer_Claes_Berg_Mortensen.Events;
using System;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.Interfaces
{

    public delegate void UserBalanceNotification(UserBalanceNotificationArgs e);

    /// <summary>
    /// IStregsystem acts as an interface for all logic in the program
    /// </summary>
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts();
        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser(Predicate<User> predicate);
        User GetUserByUsername(string username);
        event UserBalanceNotification UserbalanceWarning;
    }
}
