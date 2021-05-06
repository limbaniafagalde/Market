using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

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

        [Display(Name = "Document")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (8))]
        [Required(ErrorMessage = "You must enter {0}")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeID { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } set { } } 

        //Prop de navegacion
        //lado varios 1 tipo doc varios clientes
        public virtual DocumentType DocumentType { get; set; }


    }
}