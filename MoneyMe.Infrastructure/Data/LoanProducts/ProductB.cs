using MoneyMe.Infrastructure.Const;
using MoneyMe.Infrastructure.Interfaces;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Infrastructure.Data.LoanProducts
{
    public class ProductB : ILoanProduct
    {
        public decimal PrincipalAmount { get; set; }

        private int _term;

        public int Term { 
            get {
                return _term;
            } 
            set {
                //Enforce minimum 6 months Term rule
                if (value < 6) {
                    _term = 6;
                } else {
                    _term = value;   
                }
            }
        }

        public decimal MonthlyPaymentAmount { get; set; }

        public decimal CalculateRepaymentAmount()
        {
            //First 2 months have no interest fee
            MonthlyPaymentAmount = LoanCalculator.CalculateMonthlyPayment(PrincipalAmount, Term, LoanRates.MonthyInterestRate, 2);

            return MonthlyPaymentAmount * Term;
        }
    }
}
