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

        /// <summary>
        /// Handles the buying of products using a user and the product they're trying to buy
        /// </summary>
        /// <param name="user"></param>
        /// <param name="product"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Takes a user and inserts credits to their account based on the amount passed in this method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public InsertCashTransaction AddCreditsToAccount(User user, int amount) 
        {
            InsertCashTransaction insertCashTransaction = new InsertCashTransaction(user, amount, _transactionList.Count);
            insertCashTransaction.Execute();
            FileLogger.WriteToLogFile(insertCashTransaction.ToString(), "transactions.txt");
            _transactionList.Add(insertCashTransaction);
            return insertCashTransaction;
        }

        /// <summary>
        /// takes a product id and searches the product list for the matching product, returns matching product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductByID(int id)
        {
            Product product = _productList.Find(p => p.ID == id);
            if (product == null)
                throw new ProductNotFoundException();
            return product;
        }
        /// <summary>
        /// Takes a predicate and finds a user based on this, returns a user
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public User GetUser(Predicate<User> predicate)
        {
            return _userList.Find(predicate);
        }

        /// <summary>
        /// Finds a user by seraching the user list for a matching username, returns the matching user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUsername(string username)
        {

            User user = _userList.Find(x => x.Username == username);
            if (user == null)
                throw new UserNotFoundException(username, "Username Not Found");
            return user;
        }

        /// <summary>
        /// searches the transaction list for transactions of a certain user and returns a list of x (based on count) transactions
        /// </summary>
        /// <param name="user"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {

            List<Transaction> newTransactionList = _transactionList.OrderByDescending(d => d.Date).Where(t => t.User.Equals(user)).ToList();
            if (newTransactionList.Count > 10)
                return newTransactionList.GetRange(0, count);
            return newTransactionList;
        }

        /// <summary>
        /// Returns a list of active products in the product list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> ActiveProducts()
        {
            return _productList.Where(p => p.Active == true);
        }

        /// <summary>
        /// Loads all users from a file into a user list
        /// </summary>
        void LoadUsers()
        {
            CSVUserParser cSVUserParser = new CSVUserParser("users.csv", ',');
            _userList = cSVUserParser.ParseCSV( cSVUserParser.ReadAndSeperateCSVFile());
        }

        /// <summary>
        /// Loads all products from a file into a product list
        /// </summary>
        void LoadProducts()
        {
            CSVProductParser cSVProductParser = new CSVProductParser("products.csv", ';');
            _productList = cSVProductParser.ParseCSV(cSVProductParser.ReadAndSeperateCSVFile());
        }
    }
}
