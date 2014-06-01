using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.ViewModels;
using Project.DBControllers;
using Project.Models;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private AccountDBController accountdbcontroller = new AccountDBController();
        private GebruikerDBController gebruikerdbcontroller = new GebruikerDBController();
        private OrderDBController orderdbcontroller = new OrderDBController();
        private MainView mvvm = new MainView();

        public ViewResult LogOn()
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
                ViewBag.Foutmelding = "er ging wat fout " + e;
                return View();
            }

        }

        [HttpPost]
        public ActionResult LogOn(MainView viewmodel, String returnURL)
        {
            if (ModelState.IsValid)
            {
                AccountDBController accountdbcontroller = new AccountDBController();
                bool geldig = accountdbcontroller.IsGeldig(viewmodel.LogOnViewModel.Email, viewmodel.LogOnViewModel.Wachtwoord);
                if (geldig)
                {
                    FormsAuthentication.SetAuthCookie(viewmodel.LogOnViewModel.Email, false);

                    Gebruiker gebruiker = gebruikerdbcontroller.GetGebruiker(viewmodel.LogOnViewModel.Email);//gebruiker ophalen
                    orderdbcontroller.Goldmembership(gebruiker);//kijken of klant in aanmerking komt voor goldmembership

                    return Redirect(returnURL ?? Url.Action("Index", "Account"));
                }
                else
                {
                    ModelState.AddModelError("", "Email en wachtwoord komen niet overeen.");
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Klant"))
            {
                return RedirectToAction("MijnAccount", "Account");
            }
            else if (User.IsInRole("Beheerder"))
            {
                return RedirectToAction("Index", "Beheer");
            }
            else if (User.IsInRole("Manager"))
            {
                return RedirectToAction("Management", "Management");
            }
            return View();
        }

        public ActionResult Registreren()
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
                ViewBag.foutmelding = "er ging wat fout " + e;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Registreren(MainView view)
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

            try
            {
                if (ModelState.IsValid)
                {

                    gebruikerdbcontroller.InsertGebruiker(view.gebruiker);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(mvvm);
                }
            }
            catch (Exception e)
            {
                return View(mvvm);
            }
        }

        [Authorize]
        public ActionResult MijnAccount(String email)
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
            mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

            return View(mvvm);
        }

        [Authorize]
        public ActionResult WijzigAccount()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            if (Session["userID"] == null)
            {
                Session["userID"] = gebruikerdbcontroller.GetGebruiker(User.Identity.Name).ID;
            }

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListMand = qwkm.GetAllwinkelmand();
            mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

            return View(mvvm);
        }

        [HttpPost]
        public ActionResult WijzigAccount(MainView main)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gebruikerdbcontroller.UpdateGebruiker(main.gebruiker);
                    return LogOut();
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
                    mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                    return View(mvvm);
                }
            }
            catch (Exception e)
            {
                return View(main);
            }
        }
    }
}