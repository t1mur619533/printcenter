using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace PrintCenter.Shared
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Role { get; set; }

        [CanBeNull]
        public List<Technology> Technologies { get; set; }
    }

    public class UsersEnvelope : Envelope<List<User>>
    {
        public UsersEnvelope(List<User> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}