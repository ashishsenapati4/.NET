using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetBuddy.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage ="Title is Required")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(10)")]
        [Required(ErrorMessage ="Icon is Required")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; } = "Expense";

        [NotMapped]
        public string TitleWithIcon
        {
            get
            {
                return Icon + " " + Title;
            }
        }
    }
}
