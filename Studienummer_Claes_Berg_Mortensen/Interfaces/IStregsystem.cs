using Studienummer_Claes_Berg_Mortensen.Core;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.Interfaces
{
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser();
        User GetUserByUsername(string username);
        event UserBalanceNotification UserbalanceWarning;
    }
}
