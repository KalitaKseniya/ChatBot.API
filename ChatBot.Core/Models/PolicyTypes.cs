
namespace ChatBot.Core.Models
{
    public static class PolicyTypes
    {
        public static class Users
        {
            public const string View = "users.view.policy";
            public const string AddRemove = "users.addremove.policy";
            public const string Edit = "users.edit.policy";
            public const string EditRoles = "users.edit.roles.policy";
            public const string ViewRoles = "users.view.roles.policy";
            public const string ChangePassword = "users.changepassword.policy";
        }

        public static class Chats
        {
            public const string AddRemove = "chats.addremove.policy";
            public const string Edit = "chats.edit.policy";
            public const string ViewById = "chats.viewById.policy";
        }

        public static class Claims
        {
            public const string View = "claims.view.policy";
        }

        public static class Roles
        {
            public const string View = "roles.view.policy";
            public const string AddRemove = "roles.addremove.policy";
            public const string ViewClaims = "roles.view.claims.policy";
            public const string EditClaims = "roles.edit.claims.policy";
        }
    }
}
