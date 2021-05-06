using Market.Models;
using Market.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Market.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); //conexion a la bd q contiene la info de usuarios

        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList(); //devuelve una lista de usuarios//var pq no se q devuelve
            var usersView = new List<UserView>();//los q manipulo en la vista
            foreach (var user in users) // x cada usuario en la collec de users (no los mio)
            {
                var userView = new UserView {
                    Email = user.Email,
                    Name = user.UserName,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }
            return View(usersView);//mandar una lista de usuarios (userView)
        }

        public ActionResult Roles(string userID) //paso el cod de usuario
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); //var para manipular los users
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db)); //var para manipular los roles
            var users = userManager.Users.ToList(); //devuelve una lista de usuarios//var pq no se q devuelve
            var user = users.Find(u => u.Id == userID);//encontrar un solo usuario
                                                       //ver ctos roles tiene el user

            if (user == null)
            {
                return HttpNotFound();
            }

            var roles = roleManager.Roles.ToList(); //lista de todos los roles
            var rolesView = new List<RoleView>();
            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View(userView); //recibe userView
        }

        //get
        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); //var para manipular los users
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db)); //var para manipular los roles
            var users = userManager.Users.ToList(); //devuelve una lista de usuarios//var pq no se q devuelve
            var user = users.Find(u => u.Id == userID);//encontrar un solo usuario

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db)); //var para manipular los roles
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Select a ROLE...]" });
            list = list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name"); //origen de datos, campo de la clave, y lo q voy a mostrar

            return View(userView); //recibe userView
        }

        //get
        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            //verificar q haya seleccionado role
            var roleID = Request["RoleID"];
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db)); //var para manipular los roles
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); //var para manipular los users
            var users = userManager.Users.ToList(); //devuelve una lista de usuarios//var pq no se q devuelve
            var user = users.Find(u => u.Id == userID);//encontrar un solo usuario
            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = "You must select a role";

                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Select a ROLE...]" });
                list = list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name"); //origen de datos, campo de la clave, y lo q voy a mostrar

                return View(userView); //recibe userView
            }
            var roles = roleManager.Roles.ToList(); //busco UN rol
            var role = roles.Find(r => r.Id == roleID); //busco UN rol

            if (!userManager.IsInRole(userID, role.Name))//verificar q no tenga el rol
            {
                userManager.AddToRole(userID, role.Name);
            }


            var rolesView = new List<RoleView>();
            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);
        }

        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //delete
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db)); //var para manipular los roles
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); //var para manipular los users
            var user = userManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleID);
            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            //devolver a la vista
            
            var users = userManager.Users.ToList(); //devuelve una lista de usuarios//var pq no se q devuelve
            var roles = roleManager.Roles.ToList(); //busco UN rol
            var rolesView = new List<RoleView>();

           
            //ver ctos roles tiene el user
           
            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);

        }


        protected override void Dispose(bool disposing) //cierra la conex a la bd
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}