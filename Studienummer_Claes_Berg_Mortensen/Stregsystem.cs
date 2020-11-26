using Studienummer_Claes_Berg_Mortensen.CustomExceptions;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Studienummer_Claes_Berg_Mortensen.Events;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    class Stregsystem : IStregsystem
    {
        List<User> _userList = new List<User>();
        List<Product> _productList = new List<Product>();
        List<Transaction> _transactionList = new List<Transaction>();

        public event Interfaces.UserBalanceNotification UserbalanceWarning;

        public Stregsystem()
        {
            UserbalanceWarning(new UserBalanceNotificationArgs(null));
        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction buyTransactions = new BuyTransaction(user, product);
            buyTransactions.Execute();
            if (user.Balance < 5000)
                UserbalanceWarning(new UserBalanceNotificationArgs(user));
            _transactionList.Add(buyTransactions);
            return buyTransactions;
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction insertCashTransaction = new InsertCashTransaction(user, amount);
            insertCashTransaction.Execute();
            _transactionList.Add(insertCashTransaction);
            return insertCashTransaction;
        }
        //void ExecuteTransaction(transaction) { }
        public Product GetProductByID(int id)
        {
            Product product = _productList.Find(p => p.ID == id);
            if (product == null)
                throw new ProductNotFoundException();
            return product;
        }
        public User GetUser(Predicate<User> predicate)
        {
            return _userList.Find(predicate);
        }
        public User GetUserByUsername(string username)
        {

            User user = _userList.Find(x => x.Username == username);
            if (user == null)
                throw new UserNotFoundException();//usernotfoundexception
            return user;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return _transactionList.OrderByDescending(d=>d.Date).Where(t => t.User.Equals(user));
        }

        public IEnumerable<Product> ActiveProducts()
        {
            return _productList.Where(p => p.Active == true);
        }
        void LoadUsers()
        {
            User user;
        }
        void LoadProducts()
        {
            Product product;
        }
    }
}
