using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using WebApp.Logic;

namespace WebApp.Models
{
    public class ShoppingCart
    {
        
        public Guid CartId { get; set; }

        [BindNever]
        public User User { get; set; }

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
            User = user;
            IsCompleted = false;
            CartedProducts = new List<CartProduct>();
        }

       
        public ShoppingCart()
        {

        }
    }

}
