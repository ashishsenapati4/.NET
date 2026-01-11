using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Migrations
{
    /// <inheritdoc />
    public partial class OnetoOneEmptoEmpDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails",
                column: "EmpId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails",
                column: "EmpId");
        }
    }
}
