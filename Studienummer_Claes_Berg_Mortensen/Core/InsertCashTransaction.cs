namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base(user, amount)
        {

        }
        public override string ToString()
        {
            return $"Indbetaling: {base.ToString()}";
        }
        public override void Execute()
        {
            base.Execute();
        }
    }
}
