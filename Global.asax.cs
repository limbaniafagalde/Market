using Market.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Market
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.MarketContext,
            Migrations.Configuration>());
            ApplicationDbContext db = new ApplicationDbContext(); //conexion a la bd q contiene la info de usuarios
            CreateRoles(db);
            CreateSuperuser(db);
            AddPermisionsToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermisionsToSuperUser(ApplicationDbContext db)
        {//dar permisos x codigo
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //encontremos el user
            var user = userManager.FindByName("limbaniafagalde@gmail.com");
            if (!userManager.IsInRole(user.Id, "View")) ;// usuario no esta en el rol  rol view?
            {
                userManager.AddToRole(user.Id, "View"); //agrego al usuario rol de view
            }

            if (!userManager.IsInRole(user.Id, "Edit")) ;// usuario no esta en el rol  rol view?
            {
                userManager.AddToRole(user.Id, "Edit"); //agrego al usuario rol de view
            }

            if (!userManager.IsInRole(user.Id, "Create")) ;// usuario no esta en el rol  rol view?
            {
                userManager.AddToRole(user.Id, "Create"); //agrego al usuario rol de view
            }

            if (!userManager.IsInRole(user.Id, "Delete")) ;// usuario no esta en el rol  rol view?
            {
                userManager.AddToRole(user.Id, "Delete"); //agrego al usuario rol de view
            } 


        }

        private void CreateSuperuser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("limbaniafagalde@gmail.com");
            if (user == null) //si no existe en la bd
            {
                //lo creamos
                user = new ApplicationUser
                {
                    UserName = "limbaniafagalde@gmail.com",
                    Email = "limbaniafagalde@gmail.com"
                };//creamos el obj
            userManager.Create(user, "Limbu123*"); //inserto un usuario x codigo
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //el rolManager me va a permitir manipular los roles
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }

        }
    }
}
