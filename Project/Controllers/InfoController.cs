using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    public class InfoController : Controller
    {
        //
        // GET: /Info/

        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private MainView mvvm = new MainView();
		private Boolean contactSucces = false;

        public ActionResult Over()
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
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index" + e);
                return View();
            }
        }

        public ActionResult Contact()
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
                mvvm.email = new Email();

                if (contactSucces == true)
                {
                    ViewBag.ContactSucces = true;
                }

                return View(mvvm);
            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index "+e);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Contact(MainView viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.email.SendEmailToWebshop();
                contactSucces = true;
                return Contact();
            }
            else
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
        }
		
		        public ActionResult Verzenden()
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
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index" + e);
                return View();
            }
        }

        public ActionResult Sitemap()
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
                System.Diagnostics.Debug.WriteLine("Exception @Home/Index" + e);
                return View();
            }
        }

    }
}
