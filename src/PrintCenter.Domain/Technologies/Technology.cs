using System.Collections.Generic;

namespace PrintCenter.Domain.Technologies
{
    public class Technology
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Unit { get; set; }

        public string Description { get; set; }
    }
    
    public class TechnologyEnvelope
    {
        public Technology Technology { get; set; }

        public TechnologyEnvelope(Technology technology)
        {
            Technology = technology;
        }
    }

    public class TechnologiesEnvelope
    {
        public List<Technology> Technologies { get; set; }

        public TechnologiesEnvelope(List<Technology> technologies)
        {
            Technologies = technologies;
        }
    }
}
