using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
