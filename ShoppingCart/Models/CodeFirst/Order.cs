using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }
        public string Country { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        
    }
}