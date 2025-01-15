using MoneyMe.Infrastructure.Const;
using MoneyMe.Infrastructure.Data;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Infrastructure.Tests.Utils
{
    public class LoanCalculatorTests
    {
        [Fact]
        public void CalculateMonthlyPayment_NoInterest_NoGracePeriod()
        {
            decimal loanAmount = 10000;
            int numberOfPayments = 10;
            decimal monthlyInterestRate = decimal.Zero;

            decimal expectedMonthlyPayment = 1030;

            decimal actualMonthlyPayment = LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments, 
                monthlyInterestRate);

            Assert.Equal(expectedMonthlyPayment, Math.Round(actualMonthlyPayment, 2));
        }

        [Fact]
        public void CalculateMonthlyPayment_NoInterest_WithGracePeriod()
        {
            decimal loanAmount = 10000;
            int numberOfPayments = 10;
            decimal monthlyInterestRate = decimal.Zero;
            int gracePeriod = 2;

            decimal expectedMonthlyPayment = 1287.5m;

            decimal actualMonthlyPayment = LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate, gracePeriod);

            Assert.Equal(expectedMonthlyPayment, Math.Round(actualMonthlyPayment, 2));
        }

        [Fact]
        public void CalculateMonthlyPayment_WithInterest_NoGracePeriod()
        {
            decimal loanAmount = 10000;
            int numberOfPayments = 12;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;

            decimal expectedMonthlyPayment = 881.76m;

            decimal actualMonthlyPayment = LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments, 
                monthlyInterestRate);

            Assert.Equal(expectedMonthlyPayment, Math.Round(actualMonthlyPayment, 2));
        }

        [Fact]
        public void CalculateMonthlyPayment_WithInterest_WithGracePeriod()
        {
            decimal loanAmount = 10000;
            int numberOfPayments = 12;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;
            int gracePeriod = 2;

            decimal expectedPayment = 878.13m;

            decimal actualMonthlyPayment = LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments, 
                monthlyInterestRate, gracePeriod);

            Assert.Equal(expectedPayment, Math.Round(actualMonthlyPayment, 2));
        }

        [Fact]
        public void CalculateMonthlyPayment_DenyGracePeriodHigherThanTermOfPayment()
        {
            decimal loanAmount = 10000;
            int numberOfPayments = 6;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;
            int gracePeriod = 7;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate, gracePeriod));
        }

        [Fact]
        public void CalculateMonthlyPayment_DenyGracePeriodEqualToTermOfPayment()
        {
            decimal loanAmount = 10000;
            int numberOfPayments = 6;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;
            int gracePeriod = 6;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate, gracePeriod));
        }

        [Fact]
        public void CalculateMonthlyPayment_DenyZeroLoanAmount()
        {
            decimal loanAmount = 0;
            int numberOfPayments = 6;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;
            int gracePeriod = 6;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate, gracePeriod));
        }

        [Fact]
        public void CalculateMonthlyPayment_DenyNegativeLoanAmount()
        {
            decimal loanAmount = -500.20m;
            int numberOfPayments = 6;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;
            int gracePeriod = 6;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate, gracePeriod));
        }

        [Fact]
        public void CalculateMonthlyPayment_DenyZeroNumberOfPayments()
        {
            decimal loanAmount = 420.69m;
            int numberOfPayments = 0;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate));
        }

        [Fact]
        public void CalculateMonthlyPayment_DenyNegativeNumberOfPayments()
        {
            decimal loanAmount = -1000;
            int numberOfPayments = -10;
            decimal monthlyInterestRate = LoanRates.MonthyInterestRate;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CalculateMonthlyPayment(loanAmount, numberOfPayments,
                monthlyInterestRate));
        }

        [Fact]
        public void CalculateTotalInterest_PositiveInterest()
        {
            decimal repaymentAmount = 11000 + LoanRates.EstablishmentFee;
            decimal amountRequired = 10000m;

            decimal expectedInterest = 1000;

            decimal actualInterest = LoanCalculator.CaclulateTotalInterest(repaymentAmount, amountRequired);

            Assert.Equal(expectedInterest, actualInterest);
        }

        [Fact]
        public void CalculateTotalInterest_ZeroInterest()
        {
            decimal repaymentAmount = 5000 + LoanRates.EstablishmentFee;
            decimal amountRequired = 5000m;

            decimal expectedInterest = 0;

            decimal actualInterest = LoanCalculator.CaclulateTotalInterest(repaymentAmount, amountRequired);

            Assert.Equal(expectedInterest, actualInterest);
        }

        [Fact]
        public void CalculateTotalInterest_DenyAmountRequiredGreaterThanRepaymentAmount()
        {
            decimal repaymentAmount = 5000 + LoanRates.EstablishmentFee;
            decimal amountRequired = 10000;

            Assert.Throws<ArgumentException>(() => LoanCalculator.CaclulateTotalInterest(repaymentAmount, amountRequired));
        }
    }
}
