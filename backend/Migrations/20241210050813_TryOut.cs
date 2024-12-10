using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TryOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43e31e95-c1b5-4d24-afa2-5354de442edb", "AQAAAAIAAYagAAAAEDqPWziwnFT/fS41v1bQXkyxBCa97V8ek3zNNPQGOfc6Uov2jQHN0nmMKH8j7Pxo7w==", "9b2b448d-22c7-4e7b-bd78-dded699cf4e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a71ef9ac-8137-4433-948c-cfbd318ec701", "AQAAAAIAAYagAAAAEKpVitLIjQaZtBcJ4OrewaY2Ax+G9j4qDbWlHNYv3AHS+qHOYjHDk9/vnNElhzACMw==", "1a11dcd6-7faf-4d05-a150-563445e53f3b" });
        }
    }
}
