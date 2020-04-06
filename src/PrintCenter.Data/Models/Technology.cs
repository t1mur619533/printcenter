namespace PrintCenter.Data.Models
{
    public class Technology : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Unit { get; set; }

        public string Description { get; set; }
    }
}