using System;
using System.Text.Json.Serialization;

namespace PrintCenter.Data.Models
{
    public class Ticket : IHasId
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        [JsonIgnore]
        public User User { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime DelayedDate { get; set; }

        public Ticket()
        {
            CreatedDate = DateTime.Now;
            DelayedDate = DateTime.Now;
        }
    }
}
