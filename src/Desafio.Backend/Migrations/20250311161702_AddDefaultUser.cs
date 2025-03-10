using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DateTime birthday = new DateTime(1980, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            migrationBuilder.InsertData(
                       table: "employees",
                       columns: new[] { "first_name", "last_name", "email", "document", "phones", "manager_id", "password", "birthday", "role_id" },
                       values: new object[] { "Super", "User", "admin@email.com", "docadmin", new string[] { "0800-723-1111" }, null, "PBKDF2$iQmMocXfQO+Xwd1YFSyZV1toRUuhUCGqPOIyxCEQYpXn/tHrgKzk78Yl2T/G4zYi", birthday, 1 }
                   );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
