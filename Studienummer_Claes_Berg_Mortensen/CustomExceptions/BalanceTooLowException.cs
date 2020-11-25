using Studienummer_Claes_Berg_Mortensen.Core;
using System;

namespace Studienummer_Claes_Berg_Mortensen.CustomExceptions
{
    public class BalanceTooLowException : Exception
    {
        User user;
        Product product;
        public BalanceTooLowException(User user, Product product, string message):base(message)
        {
            User = user;
            Product = product;
        }

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
            }
        }
    }
}
