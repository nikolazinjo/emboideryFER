using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Interfaces;
using WebApp.Logic;
using WebApp.Models;


namespace WebApp.Controllers
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

        

        [HttpPost]
        public IActionResult ViewShoppingCart(ShoppingCart model)
        {
            if (model.CartedProducts.Count > 0)
            {
                _webAppSqlRepository.AddCart(model);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }


        public IActionResult Product(/*some category*/)
        {
            var products = _webAppSqlRepository.GetNProducts(8);
            return View(products);
        }

        /*

        public IActionResult AddToCart(Guid productId)
        {
            var product = _webAppSqlRepository.GetProduct(productId);
            if (product == null)
            {
                return new NotFoundResult();
            }
            return View(product);
        }

    */
        

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
