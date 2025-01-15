namespace MoneyMe.Core.Entities
{
    public class LoanPaymentDetail : BasicEntity
    {
        public int LoanApplicationId { get; set; }

        public int TermNo { get; set; }

        public decimal PaymentAmount { get; set; }

        public bool IsPaid { get; set; }
    }
}
