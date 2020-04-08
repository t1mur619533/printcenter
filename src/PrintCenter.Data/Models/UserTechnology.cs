namespace PrintCenter.Data.Models
{
    public class UserTechnology
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }
}
