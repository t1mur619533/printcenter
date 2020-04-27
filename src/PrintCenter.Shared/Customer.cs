using System.Collections.Generic;

namespace PrintCenter.Shared
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class CustomersEnvelope : Envelope<List<Customer>>
    {
        public CustomersEnvelope(List<Customer> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}