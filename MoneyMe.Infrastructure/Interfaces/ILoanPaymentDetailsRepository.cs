using MoneyMe.Core.Entities;

namespace MoneyMe.Infrastructure.Interfaces
{
    public interface ILoanPaymentDetailsRepository
    {
        void GenerateLoanPaymentDetails(LoanApplication loanApplication);

        Task<bool> SaveChanges();
    }
}
