using Studienummer_Claes_Berg_Mortensen.Tools;
using System;


namespace Studienummer_Claes_Berg_Mortensen.Core
{
    delegate void UserBalanceNotification(User user, decimal balance);
    public class User : IComparable
    {
        int id;
        string _firstname;
        string _lastname;
        string _username;
        string _email;
        decimal _balance;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value >= 1)
                    id = value;
                else throw new ArgumentOutOfRangeException(" ID must be greater than 0");
            }
        }
        public string Firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                if (Validation.NameValidation(value))
                    _firstname = value;
                else
                    throw new ArgumentOutOfRangeException("First name cannot be empty");
            }
        }
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                if (Validation.NameValidation(value))
                    _lastname = value;
                else
                    throw new ArgumentOutOfRangeException("Last name cannot be empty");
            }
        }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (Validation.UsernameVAlidation(value))
                    _username = value;
                else
                    throw new ArgumentOutOfRangeException("Username is either empty or contains illegal characters");
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (Validation.EmailValidation(value))
                    _email = value;
                else
                    throw new ArgumentException("Invalid Email");
            }
        }
        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
            }
        }

        public User(int iD, string firstname, string lastname, string username, string email, decimal balance)
        {
            ID = iD;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Balance = balance;
        }

        
        public int CompareTo(object obj)
        {
            return id.CompareTo(obj);
        }

        /// <summary>
        /// Converts user to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{_firstname} {_lastname} Balance: {_balance}";
        }
    }
}
