﻿using System.Collections.Generic;

namespace PrintCenter.Domain.Customers
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
    }
    
    public class CustomerEnvelope
    {
        public Customer Customer { get; set; }

        public CustomerEnvelope(Customer customer)
        {
            Customer = customer;
        }
    }
    
    public class CustomersEnvelope
    {
        public List<Customer> Customers { get; set; }

        public CustomersEnvelope(List<Customer> customers)
        {
            Customers = customers;
        }
    }
}