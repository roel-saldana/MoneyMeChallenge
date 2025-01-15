using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Core.Entities
{
    public class LoanApplication : BasicEntity
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal AmountRequired { get; set; }

        public int Term { get; set; }

        [MaxLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public required DateTime DateOfBirth { get; set; }

        [MaxLength(15)]
        public string Mobile { get; set; }

        public string Email { get; set; }

        public string ProductType { get; set; }

        public decimal RepaymentAmount { get; set; }

        public decimal EstablishmentFee { get; set; }

        public decimal TotalInterest { get; set; }

        [NotMapped]
        public decimal MonthlyPaymentAmount { get; set; }
    }
}
