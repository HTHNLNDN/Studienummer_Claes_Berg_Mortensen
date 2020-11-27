using Studienummer_Claes_Berg_Mortensen.CustomExceptions;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Studienummer_Claes_Berg_Mortensen.Events;
using Studienummer_Claes_Berg_Mortensen.Tools.CSVWriter;

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
            LoadUsers();
            LoadProducts();
        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction buyTransactions = new BuyTransaction(user, product, _transactionList.Count);
            buyTransactions.Execute();
            FileLogger.WriteToLogFile(buyTransactions.ToString(), "transactions.txt");
            if (user.Balance < 5000)
                UserbalanceWarning(new UserBalanceNotificationArgs(user));
            _transactionList.Add(buyTransactions);
            return buyTransactions;
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount) 
        {
            InsertCashTransaction insertCashTransaction = new InsertCashTransaction(user, amount, _transactionList.Count);
            insertCashTransaction.Execute();
            FileLogger.WriteToLogFile(insertCashTransaction.ToString(), "transactions.txt");
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
                throw new UserNotFoundException(username, "Username Not Found");
            return user;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {

            List<Transaction> newTransactionList = _transactionList.OrderByDescending(d => d.Date).Where(t => t.User.Equals(user)).ToList();
            if (newTransactionList.Count > 10)
                return newTransactionList.GetRange(0, count);
            return newTransactionList;
        }

        public IEnumerable<Product> ActiveProducts()
        {
            return _productList.Where(p => p.Active == true);
        }
        void LoadUsers()
        {
            CSVUserParser cSVUserParser = new CSVUserParser("users.csv", ',');
            _userList = cSVUserParser.ParseCSV( cSVUserParser.ReadAndSeperateCSVFile());
        }
        void LoadProducts()
        {
            CSVProductParser cSVProductParser = new CSVProductParser("products.csv", ';');
            _productList = cSVProductParser.ParseCSV(cSVProductParser.ReadAndSeperateCSVFile());
        }
    }
}
