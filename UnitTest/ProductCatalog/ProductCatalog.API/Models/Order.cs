using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal BaseAmount { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName ="decimal(12,2)")] 
        public decimal TotalAmount { get; set; }

        //Navigation Property..
        public List<OrderItem> OrderItems { get; set; } = new();
    }

}
