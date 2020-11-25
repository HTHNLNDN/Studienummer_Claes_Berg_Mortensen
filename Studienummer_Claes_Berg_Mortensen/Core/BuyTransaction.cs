using Studienummer_Claes_Berg_Mortensen.CustomExceptions;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class BuyTransaction : Transaction
    {
        Product product;
        public BuyTransaction(User user, Product product) : base(user, product.Price * -1)
        {
            Product = product;
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
        public override string ToString()
        {
            return $"Køb: {base.ToString()}";
        }

        public override void Execute()
        {
            if (product.Active == false)
                throw new ProductInactiveException(); //inset en eller anden exception for inaktivt produkt
            if (User.Balance < Amount && product.CanBeBoughtOnCredit == false)
                throw new BalanceTooLowException(User, Product, $"Bruger: {User.Username} har ikke nok stregdollars til dette køb");
            else
                base.Execute();
        }
    }
}
