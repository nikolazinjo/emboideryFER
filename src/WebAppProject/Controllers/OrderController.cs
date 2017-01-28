using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Interfaces;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {


        private readonly IWebAppRepository _webAppSqlRepository;
        private readonly UserManager<ApplicationUser> _appUserManager;

        public OrderController(IWebAppRepository webAppSqlRepository, UserManager<ApplicationUser> appUserManager)
        {
            this._webAppSqlRepository = webAppSqlRepository;
            this._appUserManager = appUserManager;
        }



        public ActionResult UnCompleted()
        {
            var orders = _webAppSqlRepository.GetUnCompletedOrders();

            return View(orders);
        }


        public ActionResult Completed()
        {
            var orders = _webAppSqlRepository.GetCompletedOrders();

            return View(orders);
        }



        public ActionResult MarkCompleted(Guid id)
        {
            try
            {
                _webAppSqlRepository.MarkAsCompletedOrder(id);

                return RedirectToAction("UnCompleted");
            }
            catch(KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        
        public ActionResult Details(Guid id)
        {
            var order = _webAppSqlRepository.GetOrder(id);

            if (order == null)
            {
                return new BadRequestResult();
            }

            return View(order);
        }

        
        public ActionResult Products(Guid id)
        {
            
            var products = _webAppSqlRepository.ViewOrderProducts(id);

            return View(products);
        }

        

        // GET: Order/CreateOrder
        //[Microsoft.AspNetCore.Mvc.HttpGet("{ShoppingCartId}")]
        [Authorize]
        public async Task<ActionResult> CreateOrder(Guid id)
        {
            var user = await GetCurrentUser();
            var me = _webAppSqlRepository.GetUser(user);
            if (me == null)
            {
                return new BadRequestResult();
            }

            var order = new Order()
            {
                Email = me.Email,
                ShoppingCartId = id,
                RequestCompleted = false
            };
            return View(order);
        }

        // POST: ShoppingCart/CreateOrder
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CreateOrder(Order order)
        {

            if (ModelState.IsValid)
            {
                var user = await GetCurrentUser();
                var me = _webAppSqlRepository.GetUser(user);
                if (me == null)
                {
                    return new BadRequestResult();
                }
                var sc = _webAppSqlRepository.GetActiveCart(me);
                if (sc == null || sc.CartedProducts.Count == 0)
                {

                    return RedirectToAction("Index", "Message",
                        MessageVm.Create(
                        urlService: Url,
                        message: "You don't have any products in your chart!",
                        returnAction: "Index",
                        returnController: "ShoppingCart"
                       ));
                }

                order.OrderDate = DateTime.UtcNow;
                order.Total = _webAppSqlRepository.GetTotalPrice(me);
                if (!_webAppSqlRepository.CreateOrder(order))
                {
                    return RedirectToAction("Index", "Message",
                        MessageVm.Create(
                        urlService: Url,
                        message: "This option is not allowed for you :D Try not to hack this webpage",
                        returnAction: "Index",
                        returnController: "ShoppinCart"
                       ));
                }
                sc.IsCompleted = true;
                sc.DateRequested = order.OrderDate;
                sc.Order = order;
                // hoce li punkut ovdje?
                _webAppSqlRepository.UpdateCart(sc);

                return RedirectToAction("Index", "Message",
                        MessageVm.Create(
                        urlService: Url,
                        message: "Your order have been created. Thank you! You will be redirected to Home page.",
                        returnAction: "Index",
                        returnController: "Home"
                       ));
            }
            return View(order);
        }



        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _appUserManager.GetUserAsync(HttpContext.User);
        }

    }
}