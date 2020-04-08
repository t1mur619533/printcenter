using System.Collections.Generic;

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

    public class UsersEnvelope
    {
        public List<User> User { get; set; }

        public UsersEnvelope(List<User> user)
        {
            User = user;
        }
    }

    public class User
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}