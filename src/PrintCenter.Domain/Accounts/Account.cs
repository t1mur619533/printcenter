namespace PrintCenter.Domain.Accounts
{
    public class Account
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Token { get; set; }
    }

    public class AccountEnvelope
    {
        public Account Account { get; set; }

        public AccountEnvelope(Account account)
        {
            Account = account;
        }
    }
}