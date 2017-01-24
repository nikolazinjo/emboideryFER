using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
  
    [Serializable]
    public class Product : IEquatable<Product>
    {
        #region Properties

        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        public string Category { get; set; }

        [MaxLength(10000)]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "Unit price")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        [DataType(DataType.Currency)]
        [Range(0.00, 10000.00)]
        public decimal ProductPrice { get; set; }

        public string ImagePath { get; set; }

        public string ThumbPath { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 10)]
        public int PriorityView { get; set; }

        [Required]
        [Display(Name = "Product is unlimited")]
        public bool IsUnlimited { get; set; }
        

       
        #endregion


        #region Implementation

        public Product()
        {
        }


        public Product(ProductModel model)
        {
            ProductId = Guid.NewGuid();
            Quantity = model.Quantity;
            ProductName = model.ProductName;
            ProductPrice = model.ProductPrice;
            IsUnlimited = model.IsUnlimited;
            ProductDescription = model.ProductDescription;
            Category = model.Category;
            PriorityView = model.PriorityView;
        }

        public Product(Product product)
        {
            ProductId = product.ProductId;
            Quantity = product.Quantity;
            ProductName = product.ProductName;
            ProductPrice = product.ProductPrice;
            IsUnlimited = product.IsUnlimited;
            ProductDescription = product.ProductDescription;
            Category = product.Category;
            PriorityView = product.PriorityView;
        }

        public Product(string name, string description, decimal price, string imagePath, string thumbPath, int quantity, bool isUnlimited, string category, int priorityView)
        {
            ProductId = Guid.NewGuid();
            Quantity = quantity;
            ImagePath = imagePath;
            ThumbPath = thumbPath;
            ProductName = name;
            ProductPrice = price;
            IsUnlimited = isUnlimited;
            ProductDescription = description;
            Category = category;
            PriorityView = priorityView;

        }

       

        #endregion


        #region IEqutable implementation

        public bool Equals(Product other)
        {
            if (other != null)
            {
                return this.ProductId == other.ProductId;
            }
            return false;
        }

        #endregion
    }
}
