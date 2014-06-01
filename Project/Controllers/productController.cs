using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    public class productController : Controller
    {
        //
        // GET: /product/

        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private MainView mvvm = new MainView();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Producten");
        }

        public ActionResult Detail(int pid = -1)
        {
            try
            {
                //TODO: check if pid bestaat
                if (pid == -1)
                {
                    return RedirectToAction("Index", "Producten");
                }
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.DetailProduct = productdbcontroller.GetProduct(pid);
                mvvm.ListMand = qwkm.GetAllwinkelmand();

                return View(mvvm);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Producten");
            }
        }

    }
}
