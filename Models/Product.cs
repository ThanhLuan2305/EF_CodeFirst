using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_CodeFirst.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product name can not empty!")]
        // Cho phép nhập kí tự 
        [RegularExpression(@"^[A-Za-z 0-9]*$", ErrorMessage = "Cannot use special characters in Product Name.")]
        [MinLength(2, ErrorMessage = "Product name >= 2 char")]
        public string ProductName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required()]
        //[Range(0, 100000, ErrorMessage = "Price should be in between 0 - 10000")]
        //[DivisibleBy100(ErrorMessage = "Price should in multiples 100")]
        public Nullable<decimal> Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfPurchase { get; set; }
        public string AvailabilityStatus { get; set; }
        [Required()]
        public Nullable<long> CategoryID { get; set; }
        [Required()]
        public Nullable<long> BrandID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> Quantity { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}