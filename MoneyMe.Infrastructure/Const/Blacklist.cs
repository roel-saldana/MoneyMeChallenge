namespace MoneyMe.Infrastructure.Const
{
    public class Blacklist
    {
        public static readonly List<string> MobileNumbers = new List<string>
        {
            "0422111333", // Example blacklisted number
            "0411222333"
        };

        public static readonly List<string> Domains = new List<string>
        {
            "blocked.com", // Example blacklisted domain
            "spam.com"
        };
    }
}
