namespace PrintCenter.Domain.Materials
{
    public class Material
    {
        public string Name { get; set; }

        public double Parameter { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public double Count { get; set; }

        public double NormalCount { get; set; }

        public double MinimalCount { get; set; }
    }
}