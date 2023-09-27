using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EPGDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class temporarelyRemovedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_ServiceUserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ServiceUserId",
                table: "Notes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b43b41f0-b90c-483f-b465-dd9c725889c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4b590ff-f2b6-4240-9f87-e0694f145f74");

            migrationBuilder.DropColumn(
                name: "ServiceUserId",
                table: "Notes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11c6e960-f329-4621-a1c3-88ba28286ac3", null, "Admin", "ADMIN" },
                    { "e2dcc57a-98a5-43b1-830d-2f7239feec31", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11c6e960-f329-4621-a1c3-88ba28286ac3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2dcc57a-98a5-43b1-830d-2f7239feec31");

            migrationBuilder.AddColumn<string>(
                name: "ServiceUserId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b43b41f0-b90c-483f-b465-dd9c725889c8", null, "Admin", "ADMIN" },
                    { "b4b590ff-f2b6-4240-9f87-e0694f145f74", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ServiceUserId",
                table: "Notes",
                column: "ServiceUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_ServiceUserId",
                table: "Notes",
                column: "ServiceUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
