using Studienummer_Claes_Berg_Mortensen.Tools;
using System;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class Product
    {
        int _id;
        string _name;
        decimal _price;
        bool _active;
        bool _canBeBoughtOnCredit;

 
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value > 0)
                    _id = value;
                else
                    throw new ArgumentOutOfRangeException("Product ID must be 1 or above");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (Validation.NameValidation(value))
                    _name = value;
                else
                    throw new ArgumentOutOfRangeException("Name cannot be empty");
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        public bool CanBeBoughtOnCredit
        {
            get
            {
                return _canBeBoughtOnCredit;
            }
            set
            {
                _canBeBoughtOnCredit = value;
            }
        }
        public Product(int iD, string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            ID = iD;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }
        public override string ToString()
        {
            return $"{_id,-10} {_name,-40} {_price,-20}";
        }
    }
}
