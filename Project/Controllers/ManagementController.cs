using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Manager/
        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private GebruikerDBController gebruikerdbcontroller = new GebruikerDBController();
        private OrderDBController orderdbcontroller = new OrderDBController();
        private ManagementDBController managedbcontroller = new ManagementDBController();
        MainView mvvm = new MainView();

        public ActionResult Management()
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception @Management/Management" + e);
                return View();
            }
        }
        public ActionResult OmzetWeek()
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.ListManager = managedbcontroller.VerkrijgOmzetPerWeek();

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception @Management/Management" + e);
                return View();
            }
        }

        public ActionResult ProductVerkoop()
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.ListVerkoop = managedbcontroller.ProductenVerkoop();

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception @Management/Management" + e);
                return View();
            }
        }
    }
}

