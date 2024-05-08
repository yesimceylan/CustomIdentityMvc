using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class mig_0705_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "priceToPay",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priceToPay",
                table: "Reservations");
        }
    }
}
