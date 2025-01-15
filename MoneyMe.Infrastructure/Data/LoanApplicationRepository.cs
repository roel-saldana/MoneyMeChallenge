using Microsoft.EntityFrameworkCore;
using MoneyMe.Core.Entities;
using MoneyMe.Infrastructure.Interfaces;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Infrastructure.Data
{
    public class LoanApplicationRepository(ApplicationDbContext context) : ILoanApplicationRepository
    {   
        public async Task<int> CreateLoanApplication(LoanApplication loanApplication)
        {
            int applicationId = CheckLoanApplication(loanApplication);

            if (applicationId == 0)
            {
                context.LoanApplications.Add(loanApplication);
                await SaveChanges();

                applicationId = loanApplication.Id;
            }

            return applicationId;            
        }

        public LoanApplication CalculateQuote(LoanApplication loanApplication)
        {
            LoanProcessor lp = new LoanProcessor(loanApplication);
            lp.CalculateQuote();

            return loanApplication;
        }

        public void UpdateLoanApplication(LoanApplication loanApplication)
        {
            context.Entry(loanApplication).State = EntityState.Modified;
        }

        private int CheckLoanApplication(LoanApplication loanApplication)
        {
            return context.LoanApplications
                .Where(x => x.FirstName == loanApplication.FirstName &&
                    x.LastName == loanApplication.LastName &&
                    x.DateOfBirth == loanApplication.DateOfBirth)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public async Task<LoanApplication?> GetLoanApplicationById(int id)
        {
            return await context.LoanApplications.FindAsync(id);
        }

        public LoanApplicationResult ApplyLoan(LoanApplication loanApplication)
        {
            LoanProcessor lp = new LoanProcessor(loanApplication);         

            return lp.ApplyLoan();
        }

        public async Task<bool> SaveChanges()
        {
            await context.SaveChangesAsync();
            return true;
        }
    }
}
