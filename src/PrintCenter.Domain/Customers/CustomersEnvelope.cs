using System.Collections.Generic;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Customers
{
    public class CustomersEnvelope
    {
        public List<Data.Models.Customer> Customers { get; set; }
    }
}