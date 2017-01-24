using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index(MessageVm messageModel)
        {
            return View(messageModel);
        }
    }
}
