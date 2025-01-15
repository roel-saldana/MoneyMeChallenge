using MoneyMe.Infrastructure.Const;

namespace MoneyMe.Infrastructure.Utils
{
    public static class LoanCalculator
    {
        public static decimal CalculateMonthlyPayment(decimal loanAmount, int numberOfPayments,
            decimal monthlyInterestRate, int noOfMonthsWithNoInterest = 0)
        {
            //Add establishment fee to the loan amount
            loanAmount += LoanRates.EstablishmentFee;

            if (numberOfPayments <= noOfMonthsWithNoInterest)
            {
                throw new ArgumentException("Number of payments must be greater than the grace period.");
            }

            decimal returnValue = decimal.Zero;
            if (monthlyInterestRate == 0)
            {
                returnValue = -loanAmount / (numberOfPayments - noOfMonthsWithNoInterest);
            }
            else
            {
                int remainingPayments = numberOfPayments - noOfMonthsWithNoInterest;
                decimal presentValueInterestFactor = (decimal)Math.Pow(1 + (double)monthlyInterestRate, remainingPayments);
                decimal monthlyPayment = monthlyInterestRate / (presentValueInterestFactor - 1) * -(loanAmount * presentValueInterestFactor);

                if (noOfMonthsWithNoInterest > 0)
                {
                    decimal totalRepayment = monthlyPayment * remainingPayments;
                    monthlyPayment = totalRepayment / numberOfPayments;
                }

                returnValue = monthlyPayment;
            }

            return Math.Abs(Math.Round(returnValue, 2));
        }

        public static decimal CaclulateTotalInterest(decimal repaymentAmount, decimal amountRequired)
        {
            return repaymentAmount - amountRequired - LoanRates.EstablishmentFee;
        }
    }
}
