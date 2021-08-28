#nullable disable

namespace ChatBot.Core.Models
{
    public partial class Chat
    {
        public int Id { get; set; }
        public string UserRequest { get; set; }
        public string BotResponse { get; set; }
        public string NextIds { get; set; }
    }
}
