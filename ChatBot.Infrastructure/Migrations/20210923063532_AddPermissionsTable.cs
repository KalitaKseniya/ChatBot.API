using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Infrastructure.Migrations
{
    public partial class AddPermissionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "8fd2971d-b714-4472-a679-f7807a4a54e6");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "eb281119-9438-4923-91f1-208b1a34a88a");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "8fd2971d-b714-4472-a679-f7807a4a54e6", "01dd8f2e-7fea-4f8a-92a7-6b09c157c4e2", "Manager", "MANAGER" });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "eb281119-9438-4923-91f1-208b1a34a88a", "9c332b77-829f-41a1-9ba6-dfedb1271a0d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
