using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Interfaces;
using Microsoft.AspNetCore.Http;
using WebApp.Models;
using System.Drawing;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using WebApp.Logic;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IWebAppRepository _webAppSqlRepository;

        public ProductController(IWebAppRepository webAppSqlRepository)
        {
            _webAppSqlRepository = webAppSqlRepository;
        }


        // GET: Product
        public IActionResult Index()
        {
            return View(_webAppSqlRepository.GetAll());
        }

        

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ProductList()
        {

            return View(_webAppSqlRepository.GetAll());
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            var imageTypes = new string[] {"image/gif", "image/jpeg", "image/png"};

            if (model.Image == null || model.Image.Length == 0)
            {
                ModelState.AddModelError("Image", "This field is required!");
            }
            else if (!imageTypes.Contains(model.Image.ContentType))
            {
                ModelState.AddModelError("Image", "Please coose either GIF, JPEG or PNG image!");
            }

            
            if (ModelState.IsValid)
            {
                var product = new Product(model);
                
                // Save image to folder
                var imageName = $"{DateTime.UtcNow:yyyyMMdd-HHmmssfff}";
                var extension = Path.GetExtension(model.Image.FileName).ToLower();
                using (var img = Image.FromStream(model.Image.OpenReadStream()))
                {
                   
                    product.ImagePath = $"{imageName}{extension}";
                    product.ThumbPath = $"Thumbs/{imageName}{extension}";

                    // Save Thumbnail size image 240 x 240
                    Picture.SaveToFolder(img, new Size(180, 180), $"./wwwroot/images/Thumbs/{imageName}{extension}");

                    // Save large image 800 x 800
                    Picture.SaveToFolder(img, new Size(800, 800), $"./wwwroot/images/{imageName}{extension}");

                }

                _webAppSqlRepository.AddProduct(product);

                return RedirectToAction("ProductList");
            }
            return View(model);
        }



        // GET: Product/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var product = _webAppSqlRepository.GetProduct((Guid)id);
            if (product == null)
            {
                return new NotFoundResult();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _webAppSqlRepository.Update(model);
                }
                catch (Exception)
                {
                    return new BadRequestResult();
                }
                
                return RedirectToAction("ProductList");
            }
            return View(model);
        }



        // GET: Product/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _webAppSqlRepository.GetProduct((Guid)id);
            if (product == null)
            {
                return new NotFoundResult();
            }

            return View(product);
        }



        // GET: Product/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new NotFoundResult();
            }

            var product = _webAppSqlRepository.GetProduct((Guid)id);
            if (product == null)
            {
                return new NotFoundResult();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        { 
            if (!_webAppSqlRepository.RemoveProduct(id))
            {
                return new BadRequestResult();
            }

            return RedirectToAction("ProductList");
        }


        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult CategoryProducts(string category)
        {
            
            List<Product> filtered = _webAppSqlRepository.GetFiltered(p => p.Category == category);

            return View(filtered);
        }
    }


}