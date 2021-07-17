using Microsoft.EntityFrameworkCore.Migrations;

namespace grad2021.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectionStatus",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "الهندسة الصناعية",
                column: "SelectionStatus",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "الهندسة المدنية",
                column: "SelectionStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "الهندسة المعمارية",
                column: "SelectionStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "الهندسة الميكانيكية",
                column: "SelectionStatus",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "هندسة الإلكترونيات والاتصالات الكهربية",
                column: "SelectionStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "هندسة الإنتاج والتصميم الميكانيكي",
                column: "SelectionStatus",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "هندسة الحاسبات والنظم",
                column: "SelectionStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "هندسة القوى الميكانيكية",
                column: "SelectionStatus",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchName",
                keyValue: "هندسة القوى والآلات الكهربية",
                column: "SelectionStatus",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectionStatus",
                table: "Branches");
        }
    }
}
