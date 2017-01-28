using System.Data.Entity;
using WebAppProject.Logic;
using WebAppProject.Models;

namespace WebAppProject.Data
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<ShoppingCart> ShoppingCarts { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<CartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(s => s.ProductId);
            modelBuilder.Entity<Product>().Property(s => s.ProductName).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.ProductDescription).IsOptional();
            modelBuilder.Entity<Product>().Property(s => s.ProductPrice).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.Category).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.ImagePath).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.ThumbPath).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.Quantity).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.IsUnlimited).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.PriorityView).IsRequired();

            modelBuilder.Entity<CartProduct>().HasKey(s => s.Id);
            modelBuilder.Entity<CartProduct>().HasRequired(s => s.Product);
            modelBuilder.Entity<CartProduct>().Property(s => s.ShoppingCartId).IsRequired();
            modelBuilder.Entity<CartProduct>().Property(s => s.Quantity).IsRequired();

            modelBuilder.Entity<ShoppingCart>().HasKey(s => s.CartId);
            modelBuilder.Entity<ShoppingCart>().Property(s => s.UserId).IsRequired();
            modelBuilder.Entity<ShoppingCart>().Property(s => s.DateCreated).IsRequired();
            modelBuilder.Entity<ShoppingCart>().Property(s => s.DateRequested).IsOptional();
            modelBuilder.Entity<ShoppingCart>().Property(s => s.IsCompleted).IsRequired();
            modelBuilder.Entity<ShoppingCart>().HasMany(s => s.CartedProducts).WithMany();
            modelBuilder.Entity<ShoppingCart>().HasOptional(s => s.Order).WithOptionalPrincipal();

            modelBuilder.Entity<User>().HasKey(s => s.Id);
            modelBuilder.Entity<User>().Property(s => s.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(s => s.Email).IsRequired();
  
            modelBuilder.Entity<Order>().HasKey(s => s.OrderId);
            modelBuilder.Entity<Order>().Property(s => s.ShoppingCartId).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.OrderDate).IsOptional();
            modelBuilder.Entity<Order>().Property(s => s.FirstName).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.LastName).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Address).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.City).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.PostalCode).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Country).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Phone).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Email).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Total).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.RequestCompleted).IsRequired();

        }

    }
}
