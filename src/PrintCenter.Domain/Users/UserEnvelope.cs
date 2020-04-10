using System.Collections.Generic;
using JetBrains.Annotations;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Users
{
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
}