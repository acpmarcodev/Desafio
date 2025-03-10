using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role_id",
                table: "employees");
        }
    }
}
