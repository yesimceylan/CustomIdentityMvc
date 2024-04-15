using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class added_new_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelImage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelImage3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelImage4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeopleCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bathrooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bedrooms = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rezName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rezPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rezEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rezPerson = table.Column<int>(type: "int", nullable: true),
                    rezDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rezDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    rezEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    rezCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HotelId",
                table: "Reservations",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
