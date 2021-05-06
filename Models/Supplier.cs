using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (3))]
        public string Name { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (3))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (3))]
        public string LastName { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (3))]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (3))]
        public string Address { get; set; }


        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }


        // 1 prov * prod
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }


    }
}