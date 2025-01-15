using MoneyMe.Infrastructure.Interfaces;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Infrastructure.Data.LoanProducts
{
    public class ProductA : ILoanProduct
    {
        public decimal PrincipalAmount { get; set; }

        public int Term { get; set; }

        public decimal MonthlyPaymentAmount { get; set; }

        public decimal CalculateRepaymentAmount()
        {
            //Zero interest rate for Product A
            MonthlyPaymentAmount = LoanCalculator.CalculateMonthlyPayment(PrincipalAmount, Term, decimal.Zero);

            return MonthlyPaymentAmount * Term;
        }
    }
}
