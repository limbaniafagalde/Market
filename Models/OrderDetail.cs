using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Display(Name = "Order")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int OrderID { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int ProductID { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (2))]
        public string Description { get; set; }


        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter {0}")]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter {0}")]
        public float Quantity { get; set; }

        public virtual Order order { get; set; }
        public virtual Product Product { get; set; }

    }
}