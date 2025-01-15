namespace MoneyMe.Infrastructure.Interfaces
{
    public interface ILoanProduct
    {
        decimal PrincipalAmount { get; set; }
        int Term { get; set; }
        decimal MonthlyPaymentAmount { get; set; }
        decimal CalculateRepaymentAmount();        
    }
}
