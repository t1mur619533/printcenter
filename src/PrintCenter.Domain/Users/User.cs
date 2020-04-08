using System.Collections.Generic;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Users
{
    public class User
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Role Role { get; set; }

        public string Token { get; set; }

        public List<Technology> Technologies { get; set; }
    }

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
}