using System.Collections.Generic;

namespace PrintCenter.Shared
{
    public class Technology
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Unit { get; set; }

        public string Description { get; set; }
    }
    
    public class TechnologiesEnvelope : Envelope<List<Technology>>
    {
        public TechnologiesEnvelope(List<Technology> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}
