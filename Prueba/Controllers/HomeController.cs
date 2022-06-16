using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            PaisesModel listPaises = new PaisesModel();
            return View(listPaises.getPaises());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Companies(string pais)
        {
            CustomersModel customers = new CustomersModel();

            return Json(customers.getCompanies(pais), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Orders(string clienteID)
        {
            OrderModel order = new OrderModel();

            return Json(order.getOrders(clienteID), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Create(string id, float freight, string orderDate, string requiredDate, string shipCity)
        {
            OrderModel order = new OrderModel();

            if (order.createOrder(id, freight, orderDate, requiredDate, shipCity))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(string[] data)
        {
            OrderModel order = new OrderModel();

            if (data.Length > 0)
            {
                bool r = false;
                foreach (var id in data)
                {
                    if (order.deleteOrder(id))
                    {
                        r = true;
                    }
                    else
                    {
                        r = false;
                    }
                }

                if (r)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }     
        }
        [HttpPost]
        public JsonResult Update(string id, float freight)
        {
            OrderModel order = new OrderModel(); 

            if(order.updateOrder(id, freight))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);         
        }
    }
}