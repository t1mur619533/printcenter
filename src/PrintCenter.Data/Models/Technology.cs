using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PrintCenter.Data.Models
{
    public class Technology : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Unit { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public List<UserTechnology> UserTechnologies { get; set; }

        [NotMapped]
        public List<User> Users => UserTechnologies.Where(technology => technology.TechnologyId.Equals(Id)).Select(technology => technology.User).ToList();
    }
}
