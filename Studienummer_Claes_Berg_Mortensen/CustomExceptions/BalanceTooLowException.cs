using Studienummer_Claes_Berg_Mortensen.Core;
using System;

namespace Studienummer_Claes_Berg_Mortensen.CustomExceptions
{
    public class BalanceTooLowException : Exception
    {
        User _user;
        Product _product;


        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        public Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
            }
        }

        public BalanceTooLowException(User user, Product product, string message) : base(message)
        {
            User = user;
            Product = product;
        }
    }
}
