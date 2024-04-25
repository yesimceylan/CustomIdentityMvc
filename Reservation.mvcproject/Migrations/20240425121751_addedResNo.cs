using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class addedResNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rezNumber",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rezNumber",
                table: "Reservations");
        }
    }
}
