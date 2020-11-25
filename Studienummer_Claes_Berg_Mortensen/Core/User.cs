using Studienummer_Claes_Berg_Mortensen.Tools;
using System;


namespace Studienummer_Claes_Berg_Mortensen.Core
{
    delegate void UserBalanceNotification(User user, decimal balance);
    public class User : IComparable
    {
        int id;
        string firstname;
        string lastname;
        string username;
        string email;
        decimal balance;

        public User(int iD, string firstname, string lastname, string username, string email, decimal balance)
        {
            ID = iD;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
        }

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
                return firstname;
            }
            set
            {
                if (Validation.NameValidation(value))
                    firstname = value;
                else
                    throw new ArgumentOutOfRangeException("First name cannot be empty");
            }
        }
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                if (Validation.NameValidation(value))
                    firstname = value;
                else
                    throw new ArgumentOutOfRangeException("Last name cannot be empty");
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (Validation.UsernameVAlidation(value))
                    username = value;
                else
                    throw new ArgumentOutOfRangeException("Username is either empty or contains illegal characters");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (Validation.EmailValidation(value))
                    email = value;
                else
                    throw new ArgumentException("Invalid Email");
            }
        }
        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {

            }
        }

        public int CompareTo(object obj)
        {
            return id.CompareTo(obj);
        }
        private void IsBalanceAboveRequired()
        {
            if (balance < 50)
            {
                //must invoke UserBalanceNotif
            }
        }
        public override string ToString()
        {
            return firstname + lastname + email;
        }
    }
}
