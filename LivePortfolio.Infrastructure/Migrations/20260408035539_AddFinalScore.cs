using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivePortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FinalScore",
                schema: "public",
                table: "Game",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalScore",
                schema: "public",
                table: "Game");
        }
    }
}
