using System;
using System.Collections.Generic;
using WebAppProject.Logic;
using WebAppProject.Models;

namespace WebAppProject.Interfaces
{
    public interface IWebAppRepository : IShoppingCart
    {


        #region Product



        void AddProduct(Product product);


        bool RemoveProduct(Guid productId);


        Product GetProduct(Guid productId);


        bool TakeProduct(Guid productId, int quantity);


        void Update(Product product);


        List<Product> GetAll();

        List<Product> GetProductsByPriority(int n);

        int GetProductQuantity(Guid productId);


        List<Product> GetFiltered(Func<Product, bool> filterFunction);

        #endregion



        #region Shopping Cart (for admin monitoring)


        void AddCart(ShoppingCart shoppingCart);

        bool RemoveCart(Guid cartId);


        ShoppingCart GetCart(Guid cartId);


        List<ShoppingCart> GetUserCarts(User user);


        List<ShoppingCart> GetFiltered(Func<ShoppingCart, bool> filterFunction);


        List<Product> GetMostPopularProducts(int n);


        

        #endregion

        #region User

        void AddUser(User user);


        User GetUser(ApplicationUser user);


      

        #endregion 
    }
}
