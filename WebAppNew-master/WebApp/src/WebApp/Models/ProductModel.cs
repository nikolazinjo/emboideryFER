using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace WebApp.Models
{
    public class ProductModel
    {

        public ProductModel()
        {
            IsUnlimited = false;
        }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }


        [Required]
        [MaxLength(40)]
        public string Category { get; set; }


        [MaxLength(10000)]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "Unit price")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        //[DataType(DataType.Currency)]
        [Range(0.00, 10000.00)]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }


        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Product is unlimited")]
        public bool IsUnlimited { get; set; }
        
        [Required]
        [Display(Name = "Product priority")]
        [Range(0,10)]
        public int PriorityView { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
