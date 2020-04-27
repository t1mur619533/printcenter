using System.Collections.Generic;
using JetBrains.Annotations;

namespace PrintCenter.Shared
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        [CanBeNull]
        public List<string> TechnologyNames { get; set; }
    }

    public class UserDetail
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Role { get; set; }

        [CanBeNull]
        public List<Technology> Technologies { get; set; }
    }

    public class UsersEnvelope : Envelope<List<UserDetail>>
    {
        public UsersEnvelope()
        {
        }
        public UsersEnvelope(List<UserDetail> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}