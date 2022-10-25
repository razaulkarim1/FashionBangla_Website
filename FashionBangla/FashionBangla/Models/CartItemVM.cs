using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionBangla.Models
{
    public class CartItemVM
    {
        [Key]
        public int CartItemId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }

}