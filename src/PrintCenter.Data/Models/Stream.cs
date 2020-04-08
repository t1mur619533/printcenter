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

        public int PackagesCount { get; set; }

        public string YaltaNumber { get; set; }

        public string YaltaPosition { get; set; }

        public Technology Technology { get; set; }

        public Customer Customer { get; set; }

        public Request Request { get; set; }

        public Plan Plan { get; set; }

        public Invoice Invoice { get; set; }

        public List<MaterialConsumptionStream> MaterialConsumptionStreams { get; set; }
    }

    public enum Priority
    {
        [Description("Низкий")]
        Low = 0,

        [Description("Ниже среднего")]
        BelowAverage = 1,

        [Description("Средний")]
        Average = 2,

        [Description("Выше среднего")]
        AboveAverage = 3,

        [Description("Высокий")]
        High = 4
    }
}