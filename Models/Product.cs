using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Product")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (2))]
        public string Description { get; set; }


        [Display(Name = "Price")]
        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must enter {0}")]
        public decimal Price { get; set; }

        [Display(Name = "Last Buy")]
       // [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter {0}")]
        public DateTime LastBuy { get; set; }

        [Display(Name = "Stock")]
       // [DataType(DataType.Currency)]
       // [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must enter {0}")]
        [Range(1, 1000)]
        public int Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        //relac * a * - 1 prod * prov
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}