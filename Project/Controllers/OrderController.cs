using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private OrderDBController orderdbcontroler = new OrderDBController();
        private GebruikerDBController gebruikerdbcontroller = new GebruikerDBController();
        private MainView mvvm = new MainView();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult MijnOrders()
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);
                mvvm.ListOrder = orderdbcontroler.GetAllOrders(mvvm.gebruiker.ID, -1, true);

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index" + e);
                return View();
            }
        }

        [Authorize]
        public ActionResult Detail(int oid)
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);
                mvvm.ListOrder = orderdbcontroler.GetAllOrders(mvvm.gebruiker.ID);
                mvvm.ListOrderRegel = orderdbcontroler.GetAllOrderRegels(oid);

                if (!orderdbcontroler.CheckOrderRegelID(oid, mvvm.gebruiker.ID))
                {
                    return RedirectToAction("MijnOrders", "Order");
                }

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index" + e);
                return View();
            }
        }

        [Authorize]
        public ActionResult Cancel(int oid)
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                if (!orderdbcontroler.CheckOrderRegelID(oid, mvvm.gebruiker.ID))
                {
                    return RedirectToAction("MijnOrders", "Order");
                }

                orderdbcontroler.CancelOrder(oid);

                return RedirectToAction("MijnOrders", "Order");
            }
            catch (Exception e)
            {
                return RedirectToAction("MijnOrders", "Order");
            }
        }
    }
}
