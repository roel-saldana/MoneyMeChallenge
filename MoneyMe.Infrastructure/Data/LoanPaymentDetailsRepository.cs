using MoneyMe.Core.Entities;
using MoneyMe.Infrastructure.Interfaces;

namespace MoneyMe.Infrastructure.Data
{
    public class LoanPaymentDetailsRepository(ApplicationDbContext context) : ILoanPaymentDetailsRepository
    {

        public void GenerateLoanPaymentDetails(LoanApplication loanApplication)
        {
            List<LoanPaymentDetail> loanPaymentDetails = [];

            for (int i = 1; i <= loanApplication.Term; i++)
            {
                loanPaymentDetails.Add(new LoanPaymentDetail
                {
                    LoanApplicationId = loanApplication.Id,
                    TermNo = i,
                    PaymentAmount = loanApplication.MonthlyPaymentAmount,
                    IsPaid = false
                });
            }

            context.LoanPaymentDetails.AddRange(loanPaymentDetails);
        }

        public async Task<bool> SaveChanges()
        {
            await context.SaveChangesAsync();
            return true;
        }
    }
}
