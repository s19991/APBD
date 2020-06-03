using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApp.Migrations
{
    public partial class AddedSubjectType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectType",
                table: "Grades",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectType",
                table: "Grades");
        }
    }
}
