using MoneyMe.Infrastructure.Utils;

namespace MoneyMe.Core.Tests.Utils
{
    public class LoanApplicationValidatorTests
    {
        [Fact]
        public void IsLegalAge_AllowLegalAge()
        {
            var dateOfBirth = new DateTime(2000, 12, 25);
            var result = LoanApplicationValidator.IsLegalAge(dateOfBirth);

            Assert.True(result);
        }

        [Fact]
        public void IsLegalAge_DenyMinor()
        {
            var dateOfBirth = new DateTime(DateTime.Now.Year - 10, 01, 01);
            var result = LoanApplicationValidator.IsLegalAge(dateOfBirth);

            Assert.False(result);
        }

        [Fact]
        public void IsMobileNumberAllowed_AllowEmptyMobile()
        {
            var result = LoanApplicationValidator.IsMobileNumberAllowed(string.Empty);

            Assert.True(result);
        }

        [Fact]
        public void IsMobileNumberAllowed_AllowValidMobile()
        {
            var result = LoanApplicationValidator.IsMobileNumberAllowed("0123456789");

            Assert.True(result);
        }

        [Fact]
        public void IsMobileNumberAllowed_DenyInvalidMobile()
        {
            //Sample blacklisted mobile numbers from Const/Blacklist.cs
            //"0422111333"
            //"0411222333"
            var result = LoanApplicationValidator.IsMobileNumberAllowed("0422111333");

            Assert.False(result);
        }

        [Fact]
        public void IsEmailDomainAllowed_AllowEmptyEmail()
        {
            var result = LoanApplicationValidator.IsEmailDomainAllowed(string.Empty);

            Assert.True(result);
        }

        [Fact]
        public void IsEmailDomainAllowed_AllowValidEmail()
        {
            var result = LoanApplicationValidator.IsEmailDomainAllowed("test@gmail.com");

            Assert.True(result);
        }

        [Fact]
        public void IsEmailDomainAllowed_DenyInvalidEmail()
        {
            //Sample blacklisted mobile numbers from Const/Blacklist.cs
            //"blocked.com",
            //"spam.com"
            var result = LoanApplicationValidator.IsEmailDomainAllowed("test@spam.com");

            Assert.False(result);
        }
    }
}