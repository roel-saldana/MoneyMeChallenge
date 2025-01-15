using Microsoft.AspNetCore.Mvc;
using MoneyMe.Core.Entities;
using MoneyMe.Infrastructure.Const;
using MoneyMe.Infrastructure.Data;
using MoneyMe.Infrastructure.Interfaces;
using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController(ILoanApplicationRepository repo, ILoanPaymentDetailsRepository pdRepo) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<string>> Create(LoanApplication loanApplication)
        {
            var applicationId = await repo.CreateLoanApplication(loanApplication);

            //Sample URL returned to user
            //Redirect the user to this url
            return Content($"http://localhost:4200/view/{applicationId}");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanApplication>> GetLoanApplicationById(int id)
        {
            var loanApplication = await repo.GetLoanApplicationById(id);

            if (loanApplication == null)
            {
                return NotFound("Loan application details is not found");
            }

            return loanApplication;
        }

        [HttpPost("calculate")]
        public ActionResult<LoanApplication> CalculateLoanApplicationQuote(LoanApplication application)
        {
            return repo.CalculateQuote(application);
        }

        [HttpPost("apply")]
        public async Task<ActionResult<LoanApplicationResult>> Apply(LoanApplication application)
        {
            var result = repo.ApplyLoan(application);

            if (result.IsSuccess)
            {
                application.EstablishmentFee = LoanRates.EstablishmentFee;
                application.TotalInterest = LoanCalculator.CaclulateTotalInterest(application.RepaymentAmount, application.AmountRequired);
                repo.UpdateLoanApplication(application);
                pdRepo.GenerateLoanPaymentDetails(application);

                await repo.SaveChanges();
            }

            return result;
        }
    }
}
