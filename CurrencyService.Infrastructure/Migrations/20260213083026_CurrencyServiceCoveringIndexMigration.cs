using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyServiceCoveringIndexMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_currencies_name",
                table: "currencies");

            migrationBuilder.CreateIndex(
                name: "IX_currencies_name",
                table: "currencies",
                column: "name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "rate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_currencies_name",
                table: "currencies");

            migrationBuilder.CreateIndex(
                name: "IX_currencies_name",
                table: "currencies",
                column: "name",
                unique: true);
        }
    }
}
