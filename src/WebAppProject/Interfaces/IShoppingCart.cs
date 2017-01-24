using System;
using System.Collections.Generic;
using WebAppProject.Logic;
using WebAppProject.Models;

namespace WebAppProject.Interfaces
{
    public interface IShoppingCart
    {
        ShoppingCart GetActiveCart(User user);

        void MarkAsCompleted(User user);
    
        void MarkAsCompleted(Guid cartId);

        void UpdateCart(ShoppingCart shoppingCart);

        void AddToCart(User user, Product product, int quantity);

        bool RemoveFromCart(User user, Guid productId, int quantity, bool all);

        void EmptyCart(User user);

        List<CartProduct> GetCartItems(User user);

        int GetCount(User user);
       
        decimal GetTotalPrice(User user);

        Guid? GetCartId(User user);

        bool CreateOrder(Order order);

    }
}
