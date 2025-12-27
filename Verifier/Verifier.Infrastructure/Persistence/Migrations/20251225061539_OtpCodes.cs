using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Verifier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OtpCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OtpCodes",
                schema: "dbo",
                newName: "OtpCodes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OtpCodes",
                newName: "OtpCodes",
                newSchema: "dbo");
        }
    }
}
