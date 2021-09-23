
using ChatBot.Core.Models;
using System.Collections.Generic;

namespace ChatBot.Core.Dtos
{
    public class RoleForCreationDto
    {
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
