
namespace ChatBot.Core.Models
{
    public static class PolicyTypes
    {
        public static class Users
        {
            public const string Manage = "users.manage.policy";
            public const string EditRole = "users.edit.role.policy";
        }

        public static class Chats
        {
            public const string Manage = "chats.manage.policy";

            public const string AddRemove = "chats.addremove.policy";
        }
    }
}
