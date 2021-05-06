using Market.Models;
using Market.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Market.Controllers
{
    public class OrdersController : Controller
    {
        MarketContext db = new MarketContext();
        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView(); //el cliente con su lista de productos
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            //almacenar en sesion
            Session["orderView"] = orderView;// as OrderView; // cdo llegue a add prod valido si puedo recuperar el prod

            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un cliente]" });
          //  list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list,"CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar

            return View(orderView);
        }

        // POST: Orders
        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView; //recupero de la session
            var customerID = int.Parse(Request["CustomerID"]);

            if (customerID == 0)
            {
                var list = db.Customers.ToList();
                list = list.OrderBy(c => c.FullName).ToList();
                list.Add(new Customer { CustomerID = 0, FirstName = "[Seleccione un cliente]" });
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar
                ViewBag.Error = "Cliente no existe";
                return View(orderView);
            }

            var customer = db.Customers.Find(customerID); //en la colec de customer buscamos el customerid 

            if (customer == null)
            {
                var list = db.Customers.ToList();
                list = list.OrderBy(c => c.FirstName).ToList();
                list.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un cliente]" });
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(orderView);
            }
            if (orderView.Products.Count==0) //si no han seleccionado productos
            {
                var list = db.Customers.ToList();
                list = list.OrderBy(c => c.FirstName).ToList();
                list.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un producto]" });
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar
                ViewBag.Error = "Debe ingresar detalle";
                return View(orderView);
            }
            var orderID = 0;
            using (var transaction = db.Database.BeginTransaction()) {
                try
                {
                    var order = new Order
                    { //atributos
                        CustomerID = customerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max(); //lista de ordenes
                                                                               //ciclo para grabar el detalle
                    foreach (var item in orderView.Products) //cada iteracion del ciclo va a haber cada prod adicionado en la orden
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductID = item.ID,
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            OrderID = orderID

                        };
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                    }

                    transaction.Commit();
                }//si no falla
                catch (Exception ex) //si falla
                {
                    transaction.Rollback(); //devolvemos para q no se guarde nada
                    ViewBag.Error = "ERROR: " + ex.Message;

                    var list = db.Customers.ToList();
                    list = list.OrderBy(c => c.FirstName).ToList();
                    list.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un producto]" });
                    ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar


                    return View(orderView);
                }   

           


          
           }//using
            ViewBag.Message = string.Format("La orden: {0}, grabada ok", orderID);

            var listC = db.Customers.ToList();
            listC = listC.OrderBy(c => c.FirstName).ToList();
            listC.Add(new Customer { CustomerID = 0, FullName = "[Seleccione un producto]" });
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar



            orderView = new OrderView(); //el cliente con su lista de productos
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            //almacenar en sesion
            Session["orderView"] = orderView;// as OrderView; // cdo llegue a add prod valido si puedo recuperar el prod

            return View(orderView);//RedirectToAction("NewOrder");
        }


        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
              list.Add(new Product {ID = 0, Description = "[Seleccione un producto]" });
            list = list.OrderBy(c => c.Description).ToList();
            ViewBag.ID = new SelectList(list, "ID", "Description"); //origen de datos, campo de la clave, y lo q voy a mostrar
            
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder) //recibe todo lo q tiene el formulario
        {
            
            var orderView = Session["orderView"] as OrderView; //recupero de la session
            var productID = int.Parse(Request["ID"]);//lo que el usuario me selecciono en el combo
            if (productID==0)
            {
                var list = db.Products.ToList();
                list.Add(new Product { ID = 0, Description = "[Seleccione un producto...]" });
                list = list.OrderBy(c => c.Description).ToList();
                ViewBag.ID = new SelectList(list, "ID", "Description"); //origen de datos, campo de la clave, y lo q voy a mostrar
                ViewBag.Error = "Debe seleccionar un producto";
                return View(productOrder);
            }

            var product = db.Products.Find(productID);

            if (product == null)
            {
                var list = db.Products.ToList();
                list = list.OrderBy(c => c.Description).ToList();
                list.Add(new Product { ID = 0, Description = "[Seleccione un producto]" });
                ViewBag.ID = new SelectList(list, "ID", "Description"); //origen de datos, campo de la clave, y lo q voy a mostrar
                ViewBag.Error = "El producto no existe";
                return View(productOrder);
            }

            productOrder = orderView.Products.Find(p => p.ID == productID);
            if (productOrder == null)
            {
                productOrder = new ProductOrder //crear obj en memoria
                {
                    Description = product.Description,
                    Price = product.Price,
                    ID = product.ID,
                    Quantity = int.Parse(Request["Quantity"]) //lo q recibo del formulario
                };
                orderView.Products.Add(productOrder); //order en su colec de prod le adicionamos el prod q acabamos de crear q es el prodorder

            }
            else
            {
                productOrder.Quantity += int.Parse(Request["Quantity"]);
            }

            var listC = db.Customers.ToList();
            listC = listC.OrderBy(c => c.FullName).ToList();
            listC.Add(new Customer { CustomerID = 0, FirstName = "[Seleccione un cliente]" });
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName"); //origen de datos, campo de la clave, y lo q voy a mostrar



            return View("NewOrder", orderView); ///retorno a la otra vista
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}