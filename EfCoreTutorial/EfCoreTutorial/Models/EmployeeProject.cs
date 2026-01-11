using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Models
{
    public class EmployeeProject
    {
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; } //Reference navigation property for Employee
        
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; } //Reference navigation property for Project

    }
}
