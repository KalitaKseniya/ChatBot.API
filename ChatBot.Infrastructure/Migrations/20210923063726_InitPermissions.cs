using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatBot.Infrastructure.Migrations
{
    public partial class InitPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Добавить и удалить чаты", "chats.addremove" },
                    { 2, "Редактировать чаты", "chats.edit" },
                    { 3, "Просмотр чатов по id", "chats.viewById" },
                    { 4, "Добавить и удалить пользователей", "users.addremove" },
                    { 5, "Сменить пароль пользователя", "users.changepassword" },
                    { 6, "Редактировать пользователя", "users.edit" },
                    { 7, "Редактировать роли для пользователя", "users.edit.roles" },
                    { 8, "Просмотр всех пользователей", "users.view" },
                    { 9, "Просмотр ролей для пользователя", "users.view.roles" },
                    { 10, "Просмотр ролей для пользователя", "roles.addremove" },
                    { 11, "Редактировать права доступа для роли", "roles.edit.claims" },
                    { 12, "Просмотр всех ролей", "roles.view" },
                    { 13, "Просмотр прав доступа для роли", "roles.view.claims" },
                    { 14, "Просмотр всех прав доступа", "claims.view" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
