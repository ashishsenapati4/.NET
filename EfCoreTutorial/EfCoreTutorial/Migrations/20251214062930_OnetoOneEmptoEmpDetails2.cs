using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Migrations
{
    /// <inheritdoc />
    public partial class OnetoOneEmptoEmpDetails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmpId",
                table: "EmployeeDetails");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "EmployeeDetails",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails",
                newName: "IX_EmployeeDetails_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "EmpId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeDetails",
                newName: "EmpId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails",
                newName: "IX_EmployeeDetails_EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmpId",
                table: "EmployeeDetails",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
