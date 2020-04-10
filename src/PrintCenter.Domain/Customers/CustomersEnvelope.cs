using System.Collections.Generic;

namespace PrintCenter.Domain.Customers
{
    public class CustomersEnvelope
    {
        public List<Customer> Customers { get; set; }

        public CustomersEnvelope(List<Customer> customers)
        {
            Customers = customers;
        }
    }
}