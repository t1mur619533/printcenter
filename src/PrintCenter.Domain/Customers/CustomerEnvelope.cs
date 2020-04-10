namespace PrintCenter.Domain.Customers
{
    public class CustomerEnvelope
    {
        public Customer Customer { get; set; }

        public CustomerEnvelope(Customer customer)
        {
            Customer = customer;
        }
    }
}