namespace ProductCatalog.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        //NAvigation Property..
        public List<Order> Orders { get; set; }
    }
}
