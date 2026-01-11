using EfCoreTutorial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        public string ConnString { get; set; }
        public AppDbContext()
        {
            ConnString = "Server=localhost\\SQLEXPRESS;Database=EmployeeMgmt_EFCore;Trusted_Connection=True;TrustServerCertificate=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(ConnString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure Primary key for employeetable using Fluent API
            //modelBuilder.Entity<Employee>()
            //    .HasKey(e =>  e.EmpId);

            ////making firstname field as required
            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.EmpFirstName)
            //    .IsRequired();


            //Many-to-many relationship
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmpId, ep.ProjectId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmpId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);
        }
    }
}
