using EfCoreTutorial.Models;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]                       
    public int EmployeeId { get; set; }

    public string? EmpFirstName { get; set; }
    public string? EmpLastName { get; set; }
    public long EmpSalary { get; set; }

    public virtual EmployeeDetails EmployeeDetails { get; set; }

    public int ManagerId { get; set; }
    public virtual Manager Manager { get; set; }

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
}
