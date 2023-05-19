using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiclesAPI.Migrations
{
    /// <inheritdoc />
    public partial class createDbVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    displacement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    length = table.Column<double>(type: "float", nullable: false),
                    witdh = table.Column<double>(type: "float", nullable: false),
                    height = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
