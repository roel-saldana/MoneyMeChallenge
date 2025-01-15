using MoneyMe.Infrastructure.Const;

namespace MoneyMe.Infrastructure.Utils
{
    public static class LoanCalculator
    {
        public static decimal CalculateMonthlyPayment(decimal loanAmount, int numberOfPayments,
            decimal monthlyInterestRate, int noOfMonthsWithNoInterest = 0)
        {
            if (loanAmount <= 0)
            {
                throw new ArgumentException("Loan cannot be less than or equal to 0, please enter a valid amount");
            }

            //Add establishment fee to the loan amount
            loanAmount += LoanRates.EstablishmentFee;

            if (numberOfPayments <= 0)
            {
                throw new ArgumentException("Number of payments cannot be less than or equal to zero");
            }

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
            if(amountRequired > repaymentAmount)
            {
                throw new ArgumentException("Amount Required must not be greater than Repayment Amount");
            }

            return repaymentAmount - amountRequired - LoanRates.EstablishmentFee;
        }
    }
}
