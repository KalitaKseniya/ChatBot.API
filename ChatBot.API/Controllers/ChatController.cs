using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
