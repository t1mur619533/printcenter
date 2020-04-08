using System.Collections.Generic;

namespace PrintCenter.Domain.Users
{
    public class UsersEnvelope
    {
        public List<User> User { get; set; }

        public UsersEnvelope(List<User> user)
        {
            User = user;
        }
    }
}