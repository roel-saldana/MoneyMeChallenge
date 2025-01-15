using MoneyMe.Core.Entities;
using MoneyMe.Infrastructure.Const;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Infrastructure.Tests.Utils
{
    public class LoanProcessorTests
    {
        LoanApplication loanApplication = new()
        {
            DateOfBirth = new DateTime(1990-10-10),
            Email = "test@mail.com",
            EstablishmentFee = 0,
            FirstName = "John",
            LastName = "Doe",
            Mobile = "0123456789",
            Title = "Mr",
        };

        [Fact]
        public void CalculateQuote_ProductA_EqualPaymentAmount()
        {
            loanApplication.ProductType = "ProductA";
            loanApplication.AmountRequired = 5000;
            loanApplication.Term = 6;

            LoanProcessor lp = new LoanProcessor(loanApplication);
            lp.CalculateQuote();

            decimal expectedRepaymentAmount = 5299.98m;
            decimal expectedMonthlyPaymentAmount = 883.33m;

            decimal actualRepaymentAmount = loanApplication.RepaymentAmount;
            decimal actualMonthlyPaymentAmount = loanApplication.MonthlyPaymentAmount;

            Assert.Equal(expectedRepaymentAmount, actualRepaymentAmount);
            Assert.Equal(expectedMonthlyPaymentAmount, actualMonthlyPaymentAmount);
        }

        [Fact]
        public void CalculateQuote_ProductB_EqualPaymentAmount()
        {
            loanApplication.ProductType = "ProductB";
            loanApplication.AmountRequired = 5000;
            loanApplication.Term = 7;

            LoanProcessor lp = new LoanProcessor(loanApplication);
            lp.CalculateQuote();

            decimal expectedRepaymentAmount = 5366.41m;
            decimal expectedMonthlyPaymentAmount = 766.63m;

            decimal actualRepaymentAmount = loanApplication.RepaymentAmount;
            decimal actualMonthlyPaymentAmount = loanApplication.MonthlyPaymentAmount;

            Assert.Equal(expectedRepaymentAmount, actualRepaymentAmount);
            Assert.Equal(expectedMonthlyPaymentAmount, actualMonthlyPaymentAmount);
        }

        [Fact]
        public void CalculateQuote_ProductB_TermLessThan6Months_ValueAdjusted()
        {
            loanApplication.ProductType = "ProductB";
            loanApplication.AmountRequired = 5000;
            loanApplication.Term = 2;

            LoanProcessor lp = new LoanProcessor(loanApplication);
            lp.CalculateQuote();

            Assert.Equal(6, loanApplication.Term);
        }

        [Fact]
        public void CalculateQuote_ProductC_EqualPaymentAmount()
        {
            loanApplication.ProductType = "ProductC";
            loanApplication.AmountRequired = 5000;
            loanApplication.Term = 12;

            LoanProcessor lp = new LoanProcessor(loanApplication);
            lp.CalculateQuote();

            decimal expectedRepaymentAmount = 5444.64m;
            decimal expectedMonthlyPaymentAmount = 453.72m;

            decimal actualRepaymentAmount = loanApplication.RepaymentAmount;
            decimal actualMonthlyPaymentAmount = loanApplication.MonthlyPaymentAmount;

            Assert.Equal(expectedRepaymentAmount, actualRepaymentAmount);
            Assert.Equal(expectedMonthlyPaymentAmount, actualMonthlyPaymentAmount);
        }
    }
}
