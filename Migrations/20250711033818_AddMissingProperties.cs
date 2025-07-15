using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVMStudio.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Portfolios",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ContactMessages",
                type: "TEXT",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ContactMessages");
        }
    }
}
