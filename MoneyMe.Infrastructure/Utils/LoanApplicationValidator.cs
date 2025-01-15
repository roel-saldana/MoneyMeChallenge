using MoneyMe.Infrastructure.Const;

namespace MoneyMe.Infrastructure.Utils
{
    public class LoanApplicationValidator
    {
        public static bool IsLegalAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;

            return age >= 18;
        }

        public static bool IsMobileNumberAllowed(string mobileNumber)
        {
            return !Blacklist.MobileNumbers.Contains(mobileNumber);
        }

        public static bool IsEmailDomainAllowed(string email)
        {
            var emailDomain = email.Split('@').Last();
            return !Blacklist.Domains.Contains(emailDomain);
        }
    }
}
