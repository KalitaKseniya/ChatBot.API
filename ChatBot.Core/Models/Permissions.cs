
namespace ChatBot.Core.Models
{
    public class Permissions
    {
        public static class Users
        {
            public const string View = "users.view";
            public const string AddRemove = "users.addremove";
            public const string Edit = "users.edit";
            public const string EditRoles = "users.edit.roles";
            public const string ViewRoles = "users.view.roles";
            public const string ChangePassword = "users.changepassword";
        }

        public static class Chats
        {
            public const string Add = "chats.add";
            public const string Edit = "chats.edit";
            public const string ViewById = "chats.viewById";
            public const string Delete = "chats.delete";
        }

        public static class Claims
        {
            public const string View = "claims.view";
        }

        public static class Roles
        {
            public const string View = "roles.view";
            public const string AddRemove = "roles.addremove";
            public const string ViewClaims = "roles.view.claims";
            public const string EditClaims = "roles.edit.claims";
        }


    }
}
