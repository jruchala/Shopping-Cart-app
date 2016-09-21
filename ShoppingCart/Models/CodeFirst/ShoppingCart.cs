using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        // foreign key
        public int ItemId { get; set;}

        // foreign key
        public string CustomerId { get; set; }

        [Display(Name = "Quantity")]
        [Range (0, 999)]
        public int Count { get; set; }

    
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        // navigation property --connection to Item table
        public virtual Item Item { get; set; }

        // navigation poperty -- connection to Customer table
        public virtual ApplicationUser Customer { get; set; }

        
    }
}