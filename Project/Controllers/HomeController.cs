using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private MainView mvvm = new MainView();

        public ActionResult Index()
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten(Project.DBControllers.ProductDBController.OrderAllProducten.RANDOM);
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                
                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index"+e);
                return View();
            }
        }

    }
}
