
//create instance of Manager class
using EfCoreTutorial.Data;
using EfCoreTutorial.Models;
using Microsoft.EntityFrameworkCore;

#region LazyLoading

using(var context = new AppDbContext())
{
    var managers = context.Managers.ToList();
    foreach(var mng in managers)
    {
        Console.WriteLine($"Manager: {mng.ManagerFirstName}");
        if(mng.Employees.Count != 0)
        {
            //We'll try to use Employee without loading it explicitly or eagerly... Lazy Loading will do that for us via proxies
            //Imp:- All the navigation properties should be virtual if you are using Lazy Loading to load data...
            foreach(var emp in mng.Employees)
            {
                Console.WriteLine($"Employee name: {emp.EmpFirstName}");
            }
            
        }
    }
}

#endregion

#region ExplicitLoading

//using (var context = new AppDbContext())
//{
    //explicit loading using Reference)(OneToOne)

    //load main entity..
    //var employees = context.Employees.ToList();
    //foreach(Employee emp in employees)
    //{
    //    //load related entity..
    //    context.Entry(emp).Reference(e => e.EmployeeDetails).Load();
    //    Console.WriteLine($"Id: {emp.EmployeeDetails.Id}; Name:{emp.EmpFirstName};" +
    //        $"Address: {emp.EmployeeDetails.EmpAddress}");

    //}


    //Explicit loading using Collection(One-To-Many)

    //(one to many from manager to employee)
    //load main entity..
    //var managers = context.Managers.ToList();
    //foreach(var mng in managers)
    //{
    //    Console.WriteLine($"Manager Name: {mng.ManagerFirstName}");
    //    //load related entity..
    //    context.Entry(mng).Collection(e => e.Employees).Load();

    //    if(mng.Employees.Any())
    //    {
    //        Console.WriteLine("Employees...");
    //        foreach(var emp in mng.Employees)
    //        {
    //            Console.WriteLine($"Employee: {emp.EmpFirstName}");
    //        }
    //    }
    //}

    //(one to many from employee to EmployeeProjects)
    //load main Entity...
    //var employees = context.Employees.ToList();
    
    //foreach(var emp in employees)
    //{
    //    Console.WriteLine($"Emp Name: {emp.EmpFirstName}");
    //    context.Entry(emp).Collection(e => e.EmployeeProjects).Load();

    //    if(emp.EmployeeProjects.Any())
    //    {
    //        Console.WriteLine("Projects...");
    //        foreach(var project in emp.EmployeeProjects)
    //        {
    //            Console.WriteLine($"Project Details: {project.ProjectId}");
    //        }
    //    }
    //}
//}

#endregion

#region EagerLoading
//using (var context = new AppDbContext())
//{
//    //Eager-loading --> Many-To-Many
//    Console.WriteLine("Eager-loading --> Many-To-Many");
//    var Projects = context.Projects.Include(e => e.EmployeeProjects).ThenInclude(e => e.Employee).ToList();
//    foreach(var project in Projects)
//    {
//        Console.WriteLine($"Project Name: {project.ProjectName}");
//        foreach(var empProj in project.EmployeeProjects)
//        {
//            Console.WriteLine($"Employee: {empProj.Employee.EmpFirstName}");

//        }
//        Console.WriteLine();
//    }



//    var employees = context.Employees.Include(e => e.EmployeeDetails).ToList();
//    foreach (Employee emp in employees)
//    {
//        Console.WriteLine($"Id: {emp.EmployeeDetails.Id}; Name:{emp.EmpFirstName};" +
//            $"Address: {emp.EmployeeDetails.EmpAddress}");
//    }
//}
#endregion

#region Adding & Querying Data
//

//using(var context = new AppDbContext())
//{
//    Project project = new Project();
//    project.ProjectName = "WaterFlowMonitoring";


//    context.Projects.Add(project);


//    context.SaveChanges();



//    EmployeeDetails empDetails = new EmployeeDetails();
//    empDetails.EmpId = 1;
//    empDetails.EmpAddress = "BBSR";
//    empDetails.EmpPhoneNo = "977741984";
//    context.EmployeeDetails.Add(empDetails);
//    context.SaveChanges(); }


//Querying Data

//using(var context = new AppDbContext())
//{
//    int id = 1;
//    var employee = context.Employees.FirstOrDefault(x => x.EmpId == id);
//    Console.WriteLine("Employee info: ");
//    Console.WriteLine("Name: " + employee.EmpFirstName + " Salary: " + employee.EmpSalary);

//    var empDetails = context.EmployeeDetails.Single(x => x.Employee == employee);
//    Console.WriteLine("Employee Details: ");
//    Console.WriteLine("Address: " + empDetails.EmpAddress + " PhNo: " + empDetails.EmpPhoneNo);
//}
#endregion