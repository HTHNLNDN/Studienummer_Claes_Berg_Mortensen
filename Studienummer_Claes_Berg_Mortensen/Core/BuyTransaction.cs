using Studienummer_Claes_Berg_Mortensen.CustomExceptions;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class BuyTransaction : Transaction
    {
        Product _product;
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
        public BuyTransaction(User user, Product product, int iD) : base(user, product.Price * -1, iD)
        {
            Product = product;
        }
        public override string ToString()
        {
            return $"Køb af {_product.Name}: {base.ToString()}";
        }

        public override void Execute()
        {
            if (_product.Active == false)
                throw new ProductInactiveException(_product, "Product is inactive"); 
            if (User.Balance < Amount && _product.CanBeBoughtOnCredit == false)
                throw new BalanceTooLowException(User, Product, $"Bruger: {User.Username} har ikke nok stregdollars til dette køb");
            else
                base.Execute();
        }
    }
}
