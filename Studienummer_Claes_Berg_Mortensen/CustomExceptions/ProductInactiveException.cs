using Studienummer_Claes_Berg_Mortensen.Core;
using System;

namespace Studienummer_Claes_Berg_Mortensen.CustomExceptions
{
    public class ProductInactiveException : Exception
    {
        public Product Product { get; set; }
        public ProductInactiveException(Product product, string message) : base(message)
        {
            Product = product;
        }

    }
}
