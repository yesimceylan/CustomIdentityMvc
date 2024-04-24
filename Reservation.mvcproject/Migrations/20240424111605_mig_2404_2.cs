using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class mig_2404_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckIn",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckOut",
                table: "Hotels",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "Hotels");
        }
    }
}
