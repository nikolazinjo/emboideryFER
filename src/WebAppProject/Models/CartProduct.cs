using System;

namespace WebAppProject.Models
{
    [Serializable]
    public class CartProduct
    {
        public CartProduct()
        {
        }

        public CartProduct(Guid shoppingCartId, Product product, int numberOfIems)
        {
            if (product == null || numberOfIems < 1)
            {
                throw new ArgumentException();
            }

            Id = Guid.NewGuid();
            ShoppingCartId = shoppingCartId;
            Product = product;
            Quantity = numberOfIems;
        }



        public Guid Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
