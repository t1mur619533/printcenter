using System.Collections.Generic;

namespace PrintCenter.Domain.Users
{
    public class UsersEnvelope
    {
        public List<UserEnvelope> Users { get; set; }

        public UsersEnvelope(List<UserEnvelope> users)
        {
            Users = users;
        }
    }
}