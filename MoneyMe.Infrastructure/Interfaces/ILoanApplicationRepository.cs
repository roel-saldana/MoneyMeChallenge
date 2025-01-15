using MoneyMe.Core.Entities;
using MoneyMe.Infrastructure.Data;

namespace MoneyMe.Infrastructure.Interfaces
{
    public interface ILoanApplicationRepository
    {
        Task<int> CreateLoanApplication(LoanApplication application);

        void UpdateLoanApplication(LoanApplication application);

        Task<LoanApplication?> GetLoanApplicationById(int id);

        LoanApplication CalculateQuote(LoanApplication application);

        LoanApplicationResult ApplyLoan(LoanApplication loanApplication);

        Task<bool> SaveChanges();
    }
}
