namespace MoneyMe.Infrastructure.Const
{
    public class LoanRates
    {
        public LoanRates() { }

        private const decimal AnnualInterestRate = 5;

        public const decimal MonthyInterestRate = (AnnualInterestRate / 100) / 12;

        public const decimal EstablishmentFee = 300;
    }
}
