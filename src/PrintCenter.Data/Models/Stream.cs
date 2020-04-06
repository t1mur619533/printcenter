using System.Collections.Generic;
using System.ComponentModel;

namespace PrintCenter.Data.Models
{
    public class Stream : IHasId
    {
        public int Id { get; set; }

        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public double SizeX { get; set; }

        public double SizeY { get; set; }
        
        public int PackageSize { get; set; }
        
        public string FilePath { get; set; }

        public string Description { get; set; }

        public Technology Technology { get; set; }
        
        public List<MaterialConsumption> MaterialConsumptions { get; set; }

        public int PackagesCount { get; set; }

        public string YaltaNumber { get; set; }

        public string YaltaPosition { get; set; }

        public Customer Customer { get; set; }

        public Request Request { get; set; }

        public Plan Plan { get; set; }

        public Invoice Invoice { get; set; }

        public Stream()
        {
            MaterialConsumptions = new List<MaterialConsumption>();
        }
    }

    public enum Priority
    {
        [Description("Низкий")]
        Low,

        [Description("Ниже среднего")]
        BelowAverage,

        [Description("Средний")]
        Average,

        [Description("Выше среднего")]
        AboveAverage,

        [Description("Высокий")]
        High
    }
}