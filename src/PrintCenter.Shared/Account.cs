namespace PrintCenter.Shared
{
    public class Account
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Token { get; set; }
    }

    public class LoginData
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class EditPasswordData
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}