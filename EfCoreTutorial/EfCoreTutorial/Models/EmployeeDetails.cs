using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Models
{
    public class EmployeeDetails
    {
        public int Id { get; set; }

        public string? EmpAddress { get; set; }

        public string? EmpPhoneNo { get; set; }

        public int EmployeeId { get; set; } //foreign key

        public virtual Employee Employee { get; set; } // Reference Navigation property
    }
}
