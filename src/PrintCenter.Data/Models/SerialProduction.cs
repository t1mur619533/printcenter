using System.Collections.Generic;

namespace PrintCenter.Data.Models
{
    public class SerialProduction
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double SizeX { get; set; }

        public double SizeY { get; set; }

        public int PackageSize { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public Material FirstMaterial { get; set; }

        public Material SecondMaterial { get; set; }

        public List<Material> Materials { get; set; }

        public Technology Technology { get; set; }
    }
}