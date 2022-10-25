using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FashionBangla.Models
{
    public class CustomerVM
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        [Required, StringLength(500)]
        public string Address { get; set; }
        [Required, CreditCard]
        public string CCNumber { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? CCExpire { get; set; }
        
    }
}