using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class mig_2604 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rezChild",
                table: "Reservations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rezChild",
                table: "Reservations");
        }
    }
}
