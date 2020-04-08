using System;

namespace PrintCenter.Data.Models
{
    public class Notification : IHasId
    {
        public int Id { get; set; }
        
        public User User { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}
