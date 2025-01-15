using MoneyMe.Core.Entities;
using MoneyMe.Infrastructure.Data;
using MoneyMe.Infrastructure.Data.LoanProducts;
using MoneyMe.Infrastructure.Interfaces;

namespace MoneyMe.Infrastructure.Utils
{
    public class LoanProcessor(LoanApplication loanApplication)
    {
        public void CalculateQuote()
        {
            ILoanProduct product;
            switch (loanApplication.ProductType)
            {
                case "ProductA":
                    product = new ProductA
                    {
                        PrincipalAmount = loanApplication.AmountRequired,
                        Term = loanApplication.Term
                    };
                    loanApplication.RepaymentAmount = product.CalculateRepaymentAmount();
                    loanApplication.MonthlyPaymentAmount = product.MonthlyPaymentAmount;
                    break;

                case "ProductB":
                    product = new ProductB
                    {
                        PrincipalAmount = loanApplication.AmountRequired,
                        Term = loanApplication.Term
                    };
                    loanApplication.Term = product.Term;
                    loanApplication.RepaymentAmount = product.CalculateRepaymentAmount();
                    loanApplication.MonthlyPaymentAmount = product.MonthlyPaymentAmount;
                    break;

                case "ProductC":
                    product = new ProductC
                    {
                        PrincipalAmount = loanApplication.AmountRequired,
                        Term = loanApplication.Term
                    };
                    loanApplication.RepaymentAmount = product.CalculateRepaymentAmount();
                    loanApplication.MonthlyPaymentAmount = product.MonthlyPaymentAmount;
                    break;

                default:
                    break;
            }
        }

        public LoanApplicationResult ApplyLoan()
        {
            List<string> errors = [];

            if(!LoanApplicationValidator.IsLegalAge(loanApplication.DateOfBirth))
            {
                errors.Add("Applicant must be of legal age");
            }

            if(!LoanApplicationValidator.IsMobileNumberAllowed(loanApplication.Mobile))
            {
                errors.Add("Mobile number is not allowed to use in this application");
            }

            if(!LoanApplicationValidator.IsEmailDomainAllowed(loanApplication.Email))
            {
                errors.Add("Email's domain is not allowed by our firm");
            }

            if (errors.Count > 0)
            {
                return new LoanApplicationResult(false,  errors);
            }

            return new LoanApplicationResult();
        }
    }
}
