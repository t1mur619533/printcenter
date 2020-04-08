using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PrintCenter.Data.Models
{
    public class Technology : IHasId
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Unit { get; set; }

        public string Description { get; set; }

        public List<UserTechnology> UserTechnologies { get; set; }
    }
}
