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
        private readonly UserManager<ApplicationUser> _appUserManager;

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



        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _appUserManager.GetUserAsync(HttpContext.User);
        }
    }
}