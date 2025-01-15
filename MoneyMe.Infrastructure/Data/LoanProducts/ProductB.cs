using MoneyMe.Infrastructure.Const;
using MoneyMe.Infrastructure.Interfaces;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Infrastructure.Data.LoanProducts
{
    public class ProductB : ILoanProduct
    {
        public decimal PrincipalAmount { get; set; }

        public int Term { get; set; }

        public decimal MonthlyPaymentAmount { get; set; }

        public decimal CalculateRepaymentAmount()
        {
            //Enforce minimum 6 months Term rule
            if (Term < 6)
            {
                Term = 6;
            } 

            //First 2 months have no interest fee
            MonthlyPaymentAmount = LoanCalculator.CalculateMonthlyPayment(PrincipalAmount, Term, LoanRates.MonthyInterestRate, 2);

            return MonthlyPaymentAmount * Term;
        }
    }
}
