using Microsoft.EntityFrameworkCore.Migrations;

namespace ElearningWebsite.API.Migrations
{
    public partial class Remove_Unecessary_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Budget",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fee",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
