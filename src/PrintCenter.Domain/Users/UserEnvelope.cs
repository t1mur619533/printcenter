using System.Collections.Generic;
using PrintCenter.Data.Models;

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
}