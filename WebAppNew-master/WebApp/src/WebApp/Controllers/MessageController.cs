using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index(MessageVm messageModel)
        {
            return View(messageModel);
        }
    }
}
