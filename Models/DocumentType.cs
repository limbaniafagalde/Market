using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Document Id")]
        public int DocumentTypeID { get; set; }

        [Display(Name = "Document Type")]
        public string Description { get; set; }

        //prop nav - 1 doc * empleados
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}