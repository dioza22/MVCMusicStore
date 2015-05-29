using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCMusicStore.Models
{
    [Bind(Exclude = "OrderID")]
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        
        [Required(ErrorMessage="First name is required!")]
        [Display(Name="First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [Display(Name = "Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(80)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required!")]
        [StringLength(40)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required!")]
        [Display(Name = "Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required!")]
        [StringLength(24)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email address is required!")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Email is is not valid.")]
        public string Email { get; set; }
        

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        
        public List<OrderDetail> OrderDetails { get; set; }
    }
}