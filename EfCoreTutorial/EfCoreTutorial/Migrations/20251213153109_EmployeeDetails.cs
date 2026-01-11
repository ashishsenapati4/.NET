using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeEmpId",
                table: "EmployeeDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmployeeEmpId",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpId",
                table: "EmployeeDetails");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails",
                column: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmpId",
                table: "EmployeeDetails",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmpId",
                table: "EmployeeDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmpId",
                table: "EmployeeDetails");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeEmpId",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeEmpId",
                table: "EmployeeDetails",
                column: "EmployeeEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeEmpId",
                table: "EmployeeDetails",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
