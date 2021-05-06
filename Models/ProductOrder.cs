using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class ProductOrder : Product
    {
        [Display(Name = "Quantity")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter {0}")]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Display(Name = "Value")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Range(1, 1000)]
        public decimal Value { get { return Price * Quantity; } }
    }
}