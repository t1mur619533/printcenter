using System.Collections.Generic;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Technologies
{
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
