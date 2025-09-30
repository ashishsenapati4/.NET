using System.ComponentModel.DataAnnotations;

namespace CRUDAppUsingAdo.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public String Designation { get; set; }

        [Required]
        public string City { get; set; }
    }
}
