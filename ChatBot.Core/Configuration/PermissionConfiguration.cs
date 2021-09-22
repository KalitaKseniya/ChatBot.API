using ChatBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot.Core.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
               //chats
               new Permission
               {
                   Id = 1,
                   Name = Permissions.Chats.AddRemove,
                   Description = "Добавить и удалить чаты"
               }, new Permission
               {
                   Id = 2,
                   Name = Permissions.Chats.Edit,
                   Description = "Редактировать чаты"
               }, new Permission
               {
                   Id = 3,
                   Name = Permissions.Chats.ViewById,
                   Description = "Просмотр чатов по id"
               },

               //users
               new Permission
               {
                   Id = 4,
                   Name = Permissions.Users.AddRemove,
                   Description = "Добавить и удалить пользователей"
               },
               new Permission
               {
                   Id = 5,
                   Name = Permissions.Users.ChangePassword,
                   Description = "Сменить пароль пользователя"
               },
               new Permission
               {
                   Id = 6,
                   Name = Permissions.Users.Edit,
                   Description = "Редактировать пользователя"
               },
               new Permission
               {
                   Id = 7,
                   Name = Permissions.Users.EditRoles,
                   Description = "Редактировать роли для пользователя"
               },
               new Permission
               {
                   Id = 8,
                   Name = Permissions.Users.View,
                   Description = "Просмотр всех пользователей"
               },
               new Permission
               {
                   Id = 9,
                   Name = Permissions.Users.ViewRoles,
                   Description = "Просмотр ролей для пользователя"
               },
               //roles
               new Permission
               {
                   Id = 10,
                   Name = Permissions.Roles.AddRemove,
                   Description = "Просмотр ролей для пользователя"
               },
               new Permission
               {
                   Id = 11,
                   Name = Permissions.Roles.EditClaims,
                   Description = "Редактировать права доступа для роли"
               },
               new Permission
               {
                   Id = 12,
                   Name = Permissions.Roles.View,
                   Description = "Просмотр всех ролей"
               },
               new Permission
               {
                   Id = 13,
                   Name = Permissions.Roles.ViewClaims,
                   Description = "Просмотр прав доступа для роли"
               },
               //claims
               new Permission
               {
                   Id = 14,
                   Name = Permissions.Claims.View,
                   Description = "Просмотр всех прав доступа"
               });
        }
    }
}
