using Microsoft.EntityFrameworkCore.Migrations;

namespace SVMStudio.Migrations
{
    public partial class AddServiceIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Services",
                type: "TEXT",
                maxLength: 100,
                nullable: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Services"
            );
        }
    }
}
