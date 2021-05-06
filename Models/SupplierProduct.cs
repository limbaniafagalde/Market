﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class SupplierProduct
    {
        [Key]
        public int SupplierProductID { get; set; }

        //claves primarias de las tablas a relacionar
        public int SupplierID { get; set; }
        public int ProductID { get; set; }

        //listas 
        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }


    }
}