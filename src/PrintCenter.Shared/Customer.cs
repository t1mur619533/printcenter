namespace PrintCenter.Shared
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class CustomersEnvelope : Envelope<Customer>
    {
        public CustomersEnvelope(Customer model, int totalCount) : base(model, totalCount)
        {
        }
    }
}