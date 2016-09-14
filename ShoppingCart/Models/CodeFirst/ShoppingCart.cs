using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ItemId { get; set;}

        public string CustomerId { get; set; }
        public int Count { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public virtual Item Item { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        
    }
}