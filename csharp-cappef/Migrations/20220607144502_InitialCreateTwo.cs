using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_cappef.Migrations
{
    public partial class InitialCreateTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_student_student_email",
                table: "student");

            migrationBuilder.AlterColumn<string>(
                name: "student_email",
                table: "student",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_student_student_email",
                table: "student",
                column: "student_email",
                unique: true,
                filter: "[student_email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_student_student_email",
                table: "student");

            migrationBuilder.AlterColumn<string>(
                name: "student_email",
                table: "student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_student_email",
                table: "student",
                column: "student_email",
                unique: true);
        }
    }
}
