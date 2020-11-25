using Studienummer_Claes_Berg_Mortensen.Tools;
using System;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class Product
    {
        int id;
        string name;
        decimal price;
        bool active;
        bool canBeBoughtOnCredit;

        public Product(int iD, string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            ID = iD;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value > 0)
                    id = value;
                else
                    throw new ArgumentOutOfRangeException("Product ID must be 1 or above");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Validation.NameValidation(value))
                    name = value;
                else
                    throw new ArgumentOutOfRangeException("Name cannot be empty");
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        public bool CanBeBoughtOnCredit
        {
            get
            {
                return canBeBoughtOnCredit;
            }
            set
            {
                canBeBoughtOnCredit = value;
            }
        }
        public override string ToString()
        {
            return id + name + price;
        }
    }
}
