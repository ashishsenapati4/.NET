using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBuddy.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select a category")]
        public int CategoryId { get; set; }

        
        public Category? Category { get; set; }

        [Range(double.Epsilon,double.MaxValue,ErrorMessage ="Please Enter a value greater than 0")]
        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon    
        {
            get
            {
                return Category == null ? "" : Category.Title+" "+Category.Icon;
            }
        }

        [NotMapped]
        public string FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("c0");
            }
        }
    }
}
