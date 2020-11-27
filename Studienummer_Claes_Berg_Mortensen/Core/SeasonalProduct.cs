using Studienummer_Claes_Berg_Mortensen.Core;

namespace Studienummer_Claes_Berg_Mortensen
{
    class SeasonalProduct : Product
    {
        string startseasonStartDate;
        string seasonEndDate;

        public SeasonalProduct(int iD, string name, decimal price, bool active, bool canBeBoughtOnCredit) : base(iD, name, price, active, canBeBoughtOnCredit)
        {
        }
    }
}
