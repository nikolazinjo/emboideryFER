using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Interfaces;
using WebAppProject.Models;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace WebAppProject.Controllers
{
    //[Authorize(Roles = "User")]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IWebAppRepository _webAppSqlRepository;
        public readonly UserManager<ApplicationUser> _appUserManager;

        public ShoppingCartController(IWebAppRepository webAppSqlRepository, UserManager<ApplicationUser> appUserManager)
        {
            this._webAppSqlRepository = webAppSqlRepository;
            this._appUserManager = appUserManager;
        }



        // GET: ShoppingCart
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUser();
            var me = _webAppSqlRepository.GetUser(user);
            if (me == null)
            {
                return new BadRequestResult();
            }
            
            ShoppingCart sc = _webAppSqlRepository.GetActiveCart(me);
            if (sc == null)
            {
                sc = new ShoppingCart(me);
                _webAppSqlRepository.AddCart(sc);
            }
            return View(_webAppSqlRepository.GetCartItems(me));
        }

        
        public async Task<IActionResult> AddToCart(Guid id)
        {
            if (_webAppSqlRepository.TakeProduct(id, 1))
            {
                var user = await GetCurrentUser();
                var me = _webAppSqlRepository.GetUser(user);
                if (me == null)
                {
                    return new BadRequestResult();
                }
                var prod = _webAppSqlRepository.GetProduct(id);
                _webAppSqlRepository.AddToCart(me, prod, 1);
               
            }
            return RedirectToAction("Index");
        }



        // GET: ShoppingCart/CreateOrder
        //[Microsoft.AspNetCore.Mvc.HttpGet("{ShoppingCartId}")]
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
                ShoppingCartId = id
            };
            return View(order);
        }

        // POST: ShoppingCart/CreateOrder
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
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

       
        // POST: ShoppingCart/Delete/5
        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //[Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await GetCurrentUser();
            var me = _webAppSqlRepository.GetUser(user);
            if (me == null)
            {
                return new BadRequestResult();
            }

            if (!_webAppSqlRepository.RemoveFromCart(me, id, 1, false))
            {
                return new BadRequestResult();
            }
            
            return RedirectToAction("Index");
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _appUserManager.GetUserAsync(HttpContext.User);
        }

        public async Task<ActionResult> DeleteAll()
        {
            var user = await GetCurrentUser();
            var me = _webAppSqlRepository.GetUser(user);
            if (me == null)
            {
                return new BadRequestResult();
            }
            _webAppSqlRepository.EmptyCart(me);
         
            return RedirectToAction("Index");
        }
    }
}