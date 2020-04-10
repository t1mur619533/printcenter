namespace PrintCenter.Domain.Users
{
    public class UserEnvelope
    {
        public User User { get; set; }

        public UserEnvelope(User user)
        {
            User = user;
        }
    }
}