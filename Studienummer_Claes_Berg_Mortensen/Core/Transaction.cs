using Studienummer_Claes_Berg_Mortensen.Tools;
using System;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class Transaction
    {
        int _id;
        User _user;
        DateTime _date;
        decimal _amount;



        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                if (Validation.NullValidation(value))
                    _user = value;
                else
                    throw new ArgumentNullException("User cannot be empty");
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }
        public Transaction(User user, decimal amount, int iD)
        {
            User = user;
            Amount = amount;
            ID = iD;
        }
        public virtual void Execute()
        {
            Date = DateTime.Now;
            User.Balance += _amount;
        }
        public override string ToString()
        {

            return $"Transaktions ID: {_id}, Bruger: {_user.ToString()}, Dato: {_date}";
        }
    }
}
