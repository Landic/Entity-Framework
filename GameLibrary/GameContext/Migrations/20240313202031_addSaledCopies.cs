using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameContext.Migrations
{
    /// <inheritdoc />
    public partial class addSaledCopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaledCopies",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaledCopies",
                table: "Games");
        }
    }
}
