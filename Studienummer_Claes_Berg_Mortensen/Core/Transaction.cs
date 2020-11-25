using Studienummer_Claes_Berg_Mortensen.Tools;
using System;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class Transaction
    {
        int id;
        User user;
        DateTime date;
        decimal amount;

        public Transaction(User user, decimal amount)
        {

        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (Validation.NullValidation(value))
                    user = value;
                else
                    throw new ArgumentNullException("User cannot be empty");
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        public virtual void Execute()
        {
            Date = DateTime.Now;
            User.Balance += amount;
        }
        public override string ToString()
        {
            return $"Transaktions ID: {id}, Bruger: {user.ToString()}, Dato: {date}";
        }
    }
}
