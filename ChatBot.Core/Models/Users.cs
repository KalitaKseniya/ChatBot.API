
namespace ChatBot.Core.Models
{
    public class Permissions 
    {
        public static class Users
        {
            public const string Add = "users.add";
            public const string Edit = "users.edit";
            public const string EditRole = "users.edit.role";
        }

        public static class Chats
        {
            //public const string AddRemove = "chats.addremove";
            public const string Delete = "chats.delete";
        }
    }
}
