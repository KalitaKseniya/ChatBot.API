using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "Chats",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            UserRequest = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
        //            BotResponse = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
        //            NextIDs = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Chats", x => x.Id);
        //        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Chats");
        }
    }
}
