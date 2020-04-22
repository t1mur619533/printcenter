using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Users
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Role { get; set; }
    }

    public class UserEnvelope
    {
        public User User { get; set; }

        [CanBeNull]
        public List<Technology> Technologies { get; set; }

        //for mapper
        public UserEnvelope()
        {
        }

        public UserEnvelope(User user, List<Technology> technologies = null)
        {
            User = user;
            Technologies = technologies;
        }
    }

    public class UsersEnvelope
    {
        public List<UserEnvelope> Users { get; set; }

        public UsersEnvelope(List<UserEnvelope> users)
        {
            Users = users;
        }
    }
}