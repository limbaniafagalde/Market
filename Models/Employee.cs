using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name ="First Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30,ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength =(3))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {1} and {2} characters", MinimumLength = (3))]
        public string LastName { get; set; }

        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Salary { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter {0}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "You must enter {0}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [DataType(DataType.Url)]
        public string URL { get; set; }

        [Display(Name = "Document")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(15), MinLength(8)]
        public string DocumentNumber { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeID { get; set; }

        //prop de nav
        //lado varios 1 tipo doc varios empleados
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<Order> Orders { get; set; } //1 cliente * ordenes

    }
}