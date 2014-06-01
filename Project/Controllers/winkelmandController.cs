using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    public class winkelmandController : Controller
    {
        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private GebruikerDBController gebruikerdbcontroller = new GebruikerDBController();
        private OrderDBController orderdbcontroller = new OrderDBController();
        private MainView mvvm = new MainView();

        public enum Operator
        {
            PLUS,
            MIN
        }
        
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
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                return View(mvvm);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Bestelling()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (Session["SESwkm"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"]; 

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                return View(mvvm);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Betaling()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (Session["SESwkm"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                return View(mvvm);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Verwerken()
        {
            try
            {
                double totaalPrijs = 0;
                int poid = -1;

                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (Session["SESwkm"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                for (int q = 0; q < mvvm.ListMand.Count; q++)
                {
                    if (mvvm.gebruiker.Goldmembership == true)
                    {
                        totaalPrijs += ((mvvm.ListMand[q].Prijs * mvvm.ListMand[q].Aantal) * 0.96);
                    }
                    else
                    {
                        totaalPrijs += (mvvm.ListMand[q].Prijs * mvvm.ListMand[q].Aantal);
                    }
                }

                orderdbcontroller.InsertOrder(mvvm.gebruiker.ID, totaalPrijs);
                poid = orderdbcontroller.GetOrderID(mvvm.gebruiker.ID);

                for (int a = 0; a < mvvm.ListMand.Count; a++)
                {
                    if (mvvm.gebruiker.Goldmembership == true)
                    {
                        orderdbcontroller.InsertOrderRegel(mvvm.ListMand[a].ID, poid, mvvm.ListMand[a].Aantal, ((mvvm.ListMand[a].Prijs * mvvm.ListMand[a].Aantal) * 0.96));
                    }
                    else
                    {
                        orderdbcontroller.InsertOrderRegel(mvvm.ListMand[a].ID, poid, mvvm.ListMand[a].Aantal, (mvvm.ListMand[a].Prijs * mvvm.ListMand[a].Aantal));
                    }
                }

                qwkm.DeleteAllwinkelmand();

                return RedirectToAction("Voltooid", "Winkelmand", new { oid = poid });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Voltooid(int oid)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (Session["SESwkm"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
                mvvm.ListProduct = productdbcontroller.GetAllProducten();
                mvvm.ListMerk = merkdbcontroler.GetAllMerken();
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(User.Identity.Name);

                Email email = new Email();
                email.berichtVan = mvvm.gebruiker.Email;
                email.Bericht = "Beste klant,<br><br>Bedankt voor je bestelling! We zullen deze zo snel mogelijk naar je opsturen. Mocht je je bedenken, dan heb je 7 dagen de tijd om je bestelling te annuleren.<br><br><br>Met vriendelijke groet, <br>Het Intosport team";
                email.Onderwerp = "Orderbevestiging IntoSport";
                email.SendEmail();

                ViewBag.OrderID = oid;

                return View(mvvm);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Legen()
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    RedirectToAction("Index", "Winkelmand");
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];
                qwkm.DeleteAllwinkelmand();

                return RedirectToAction("Index", "Winkelmand");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Add(int pid)
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];
                qwkm.Addwinkelmand(pid);

                return RedirectToAction("Index", "Winkelmand");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Aantal(int pid, Operator opr)
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                if (opr == Operator.PLUS)
                {
                    qwkm.WinkelmandItemPlus(pid);
                }
                else if (opr == Operator.MIN)
                {
                    qwkm.WinkelmandItemMin(pid);
                }

                return RedirectToAction("Index", "Winkelmand");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

        public ActionResult Delete(int pid)
        {
            try
            {
                if (Session["SESwkm"] == null)
                {
                    RedirectToAction("Index", "Winkelmand");
                }

                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];
                qwkm.Deletewinkelmand(pid);

                return RedirectToAction("Index", "Winkelmand");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Winkelmand");
            }
        }

    }
}
