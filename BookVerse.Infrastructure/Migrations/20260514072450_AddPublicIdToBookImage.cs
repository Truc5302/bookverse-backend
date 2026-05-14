using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicIdToBookImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "BookImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "BookImages");
        }
    }
}
