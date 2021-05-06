using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.ViewModels
{
    public class UserView //no para mandar a bd (como los demas), sino para manipular la vista 
    { //1 usuario * roles
        public string UserID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RoleView Role { get; set; } //para mostrar los titulos en la vista
        //almacenar temporalmente el rol q escoge
        public List<RoleView> Roles { get; set; }
    }
}