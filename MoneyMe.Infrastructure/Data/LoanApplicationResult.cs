namespace MoneyMe.Infrastructure.Data
{
    public class LoanApplicationResult
    {
        public LoanApplicationResult()
        {
        }

        public LoanApplicationResult(bool isSuccess, List<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; set; } = true;

        public List<string>? Errors { get; set; }
    }
}
