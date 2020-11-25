using Studienummer_Claes_Berg_Mortensen.CustomExceptions;
using Studienummer_Claes_Berg_Mortensen.Interfaces;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    class Stregsystem : IStregsystem
    {
        List<User> userList = new List<User>();
        List<Product> productList = new List<Product>();
        List<Transaction> transactionList = new List<Transaction>();

        public Stregsystem()
        {

        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction a = new BuyTransaction(user, product);
            a.Execute();
            transactionList.Add(a);
            return a;
        }
        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction b = new InsertCashTransaction(user, amount);
            b.Execute();
            transactionList.Add(b);
            return b;
        }
        //void ExecuteTransaction(transaction) { }
        public Product GetProductByID(int id)
        {
            Product product = productList.Find(p => p.ID == id);
            if (product == null)
                throw new ProductNotFoundException();
            return product;
        }
        public void GetUser(predicate<User> predicate)
        {
            return userList.Find(predicate);
        }
        public User GetUserByUsername(string username)
        {

            User user = userList.Find(x => x.Username == username);
            if (user == null)
                throw new UserNotFoundException();//usernotfoundexception
            return user;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return transactionList.Find(t => t.User.Equals(user));
        }

        public Product ActiveProducts()
        {
            return productList.Find(p => p.Active == true);
        }
    }
}
