using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Interfaces;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebAppRepository _webAppSqlRepository;
        public readonly UserManager<ApplicationUser> _appUserManager;

        public HomeController(IWebAppRepository webAppSqlRepository, UserManager<ApplicationUser> appUserManager)
        {
            _webAppSqlRepository = webAppSqlRepository;
            _appUserManager = appUserManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Account()
        {
            return View();
        }

        
   
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _appUserManager.GetUserAsync(HttpContext.User);
        }


    }
}
