using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Display(Name = "Date Order")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter {0}")]
        public DateTime DateOrder { get; set; }

        [Display(Name = "Customer")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int CustomerID { get; set; }

        [Display(Name = "Order Status")]
        [Required(ErrorMessage = "You must enter {0}")]
        public OrderStatus OrderStatus { get; set; }

        //prop de nav 1 orden 1 cliente
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}