using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAppProject.Logic;

namespace WebAppProject.Models
{
    public class ShoppingCart
    {
        
        public Guid CartId { get; set; }

        [BindNever]
        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }

        [BindNever]
        public DateTime? DateRequested { get; set; }

        [BindNever]
        public bool IsCompleted { get; set; }

        public List<CartProduct> CartedProducts { get; set; }

        [BindNever]
        // use virtual for lazy loading
        public Order Order { get; set; }

        public ShoppingCart(User user)
        {
            CartId = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
            UserId = user.Id;
            IsCompleted = false;
            CartedProducts = new List<CartProduct>();
        }

       
        public ShoppingCart()
        {

        }
    }

}
