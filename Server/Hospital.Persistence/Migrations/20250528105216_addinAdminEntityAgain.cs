using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addinAdminEntityAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$12$6tAoLXokSm2FgdSegUhmouXBrGtzx3aS4zKGDsjP9XooCaRIxoMmO");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$12$7RE.99TIQKIQZzJwli0YL.etFh3Y8QG1QTQUQvEwORv5TXjP0eaNa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$iruWxCBr0FLQZ76E7ldzze5r4SgGi7zhmDa0pq3oQeVh101Auieea");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$9yOukezcNqbX0GI0EaD6weWdcrFfPlj7J8KwlTbaXivcEGR7f3zCS");
        }
    }
}
