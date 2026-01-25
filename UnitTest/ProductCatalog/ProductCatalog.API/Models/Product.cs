using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        [Column(TypeName="decimal(12,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }

        //Navigation property..
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
