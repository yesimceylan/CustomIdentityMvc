using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class mig_2404 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Shower",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TV",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Wifi",
                table: "Hotels",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shower",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "TV",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Wifi",
                table: "Hotels");
        }
    }
}
