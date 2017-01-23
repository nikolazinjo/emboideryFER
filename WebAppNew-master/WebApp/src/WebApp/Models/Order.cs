using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace WebApp.Models
{
    public class Order
    {

        public Order()
        {
            OrderId = Guid.NewGuid();
           
        }
        public Order(Guid cartId, DateTime orderDate, string firstName, string lastName, string adress, string city, string postalCode, string country, string phoneNumber, string emailAdress, Decimal total)
        {
            OrderId = Guid.NewGuid();
            ShoppingCartId = cartId;
            OrderDate = orderDate;
            FirstName = firstName;
            LastName = lastName;
            Address = adress;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Phone = phoneNumber;
            Email = emailAdress;
            Total = total;
        }

        public Guid OrderId { get; set; }


        public Guid ShoppingCartId { get; set; }

        [BindNever]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [StringLength(40)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(10, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(40)]
        public string Country { get; set; }

        [Required]
        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        //    ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        
    }
}
