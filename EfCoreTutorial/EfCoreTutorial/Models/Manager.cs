using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }

        public string? ManagerFirstName { get; set; }

        public string? ManagerLastName { get; set; }

        //one-to-many relation ship from manager to employee
        public virtual ICollection<Employee> Employees { get; set; }

       // public Employee Employee { get; set; } //Rfernece Navigation Property.
    }
}
