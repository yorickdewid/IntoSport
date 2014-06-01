using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Project.Models;
using Project.DBControllers;

namespace Project.Controllers
{
    [Authorize(Roles = "Beheerder")]
    public class BeheerController : Controller
    {
        //
        // GET: /beheer/
        private CategorieDBController categoriedbcontroller = new CategorieDBController();
        private ProductDBController productdbcontroller = new ProductDBController();
        private MerkDBController merkdbcontroler = new MerkDBController();
        private GebruikerDBController gebruikerdbcontroller = new GebruikerDBController();
        private OrderDBController orderdbcontroller = new OrderDBController();
        MainView mvvm = new MainView();

        public ActionResult Index()
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

        public ActionResult Producten()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten(ProductDBController.OrderAllProducten.ALLES, -1, -1, 100, true);
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }

        public ActionResult Categorieen()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories(true);
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }


        public ActionResult GebruikerBeheer()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListGebruiker = gebruikerdbcontroller.GetAllGebruikers(true);
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }

        public ActionResult KlantenLijst()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListGebruiker = gebruikerdbcontroller.GetAllBestelGebruikers();
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }

        public ActionResult Bestellingen(int klantID)
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListOrder = orderdbcontroller.GetAllOrdersPerKlant(klantID);
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }

        public ActionResult Merken()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken(true);
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }

        public ActionResult Beheerders()
        {
            if (Session["SESwkm"] == null)
            {
                Session["SESwkm"] = new Winkelmand();
            }
            Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.ListGebruiker = gebruikerdbcontroller.GetAllBeheerders();
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
        }

        //------------------------------------Product 

        public ActionResult WijzigThumb(int pid)
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
                mvvm.DetailProduct = productdbcontroller.GetProduct(pid);
                mvvm.ListMand = qwkm.GetAllwinkelmand();

                List<Merk> merken = merkdbcontroler.GetAllMerken();

                mvvm.SelectedMerkID = mvvm.DetailProduct.merk.ID;

                mvvm.Merken = new SelectList(merken, "ID", "Naam");

                List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();

                mvvm.SelectedCategorieID = mvvm.DetailProduct.cat.ID;

                mvvm.Categorieen = new SelectList(categorieen, "ID", "Naam");

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Product Wijzigproduct()");
                return View(e);
            }
        }


        [HttpPost]
        public ActionResult WijzigThumb(MainView main, HttpPostedFileBase imageName)
        {
            try
            {
                if (imageName != null && imageName.ContentLength > 0 && (imageName.ContentType.Equals("image/png") || imageName.ContentType.Equals("image/gif") || imageName.ContentType.Equals("image/jpeg")))
                {
                        var fileName = Path.GetFileName(imageName.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
                        imageName.SaveAs(path);

                        productdbcontroller.UpdateProductAfbeelding(main.DetailProduct.ID, fileName);

                        return RedirectToAction("Producten", "beheer");
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

                    List<Merk> merken = merkdbcontroler.GetAllMerken();

                    mvvm.Merken = new SelectList(merken, "ID", "Naam");

                    List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();

                    mvvm.Categorieen = new SelectList(categorieen, "ID", "Naam");

                    return View(mvvm);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Product Wijzigproduct(Product product)");
                return View(e);
            }
        }

        public ActionResult WijzigProduct(int pid)
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
                mvvm.DetailProduct = productdbcontroller.GetProduct(pid);
                mvvm.ListMand = qwkm.GetAllwinkelmand();
                
                List<Merk> merken = merkdbcontroler.GetAllMerken();

                mvvm.SelectedMerkID = mvvm.DetailProduct.merk.ID;

                mvvm.Merken = new SelectList(merken, "ID", "Naam");

                List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();

                mvvm.SelectedCategorieID = mvvm.DetailProduct.cat.ID;

                mvvm.Categorieen = new SelectList(categorieen, "ID", "Naam");

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Product Wijzigproduct()");
                return View(e);
            }
        }

        [HttpPost]
        public ActionResult WijzigProduct(MainView main)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    main.DetailProduct.merk = merkdbcontroler.GetMerk(main.SelectedMerkID);
                    main.DetailProduct.cat = categoriedbcontroller.GetCategorie(main.SelectedCategorieID);
                    productdbcontroller.UpdateProduct(main.DetailProduct);

                    return RedirectToAction("Producten", "beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();


                    List<Merk> merken = merkdbcontroler.GetAllMerken();


                    main.Merken = new SelectList(merken, "ID", "Naam");

                    List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();


                    main.Categorieen = new SelectList(categorieen, "ID", "Naam");

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Product Wijzigproduct(Product product)");
                return View(e);
            }
        }

        public ActionResult ZetProductNonActief(int pid)
        {
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

            try
            {
                productdbcontroller.NonActiefProduct(pid);
                return RedirectToAction("Producten", "beheer");
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        public ActionResult ZetProductActief(int pid)
        {
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

            try
            {
                productdbcontroller.ActiefProduct(pid);
                return RedirectToAction("Producten", "beheer");
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        public ActionResult ToevoegenProduct()
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

                List<Merk> merken = merkdbcontroler.GetAllMerken();
                

                mvvm.Merken = new SelectList(merken, "ID", "Naam");

                List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();
                

                mvvm.Categorieen = new SelectList(categorieen, "ID", "Naam");
                return View(mvvm);
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        [HttpPost]
        public ActionResult ToevoegenProduct(MainView main)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    main.DetailProduct.merk = merkdbcontroler.GetMerk(main.SelectedMerkID);
                    main.DetailProduct.cat = categoriedbcontroller.GetCategorie(main.SelectedCategorieID);
                    main.DetailProduct.Image = "geenfoto.png";
                    productdbcontroller.InsertProduct(main.DetailProduct);

                    return RedirectToAction("Producten", "beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();


                    List<Merk> merken = merkdbcontroler.GetAllMerken();


                    main.Merken = new SelectList(merken, "ID", "Naam");

                    List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();


                    main.Categorieen = new SelectList(categorieen, "ID", "Naam");

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        private SelectList getSelectListCategorieen()
        {
            List<Categorie> categorieen = categoriedbcontroller.GetAllCategories();
            Categorie emptyCat = new Categorie();
            emptyCat.ID = -1;
            emptyCat.Naam = "";
            categorieen.Insert(0, emptyCat);

            return new SelectList(categorieen, "ID", "Naam");
        }

        private SelectList getSelectListMerken()
        {
            List<Merk> merken = merkdbcontroler.GetAllMerken();
            Merk emptyMerk = new Merk();
            emptyMerk.ID = -1;
            emptyMerk.Naam = "";
            merken.Insert(0, emptyMerk);

            return new SelectList(merken, "ID", "Naam");
        }


        //------------------------------------Categorie

        public ActionResult WijzigCategorie(int cid)
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
                mvvm.categorie = categoriedbcontroller.GetCategorie(cid);
                mvvm.ListMand = qwkm.GetAllwinkelmand();

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }


        [HttpPost]
        public ActionResult WijzigCategorie(MainView main)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoriedbcontroller.UpdateCategorie(main.categorie);

                    return RedirectToAction("Categorieen", "beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult ToevoegenCategorie()
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

        [HttpPost]
        public ActionResult ToevoegenCategorie(MainView main)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    categoriedbcontroller.InsertCategorie(main.categorie);
                    return RedirectToAction("Categorieen", "Beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult VerwijderCategorie(int cid)
        {
            try
            {
                categoriedbcontroller.DeleteCategorie(cid);

                return RedirectToAction("Categorieen", "Beheer");
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        //------------------------------------Klant

        public ActionResult WijzigGebruiker(int kid)
        {
                       
            try
            {
                if (Session["SESwkm"] == null)
                {
                    Session["SESwkm"] = new Winkelmand();
                }
                Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                if (Session["userID"] == null)
                {
                    Session["userID"] = kid;
                }

            mvvm.ListCategorie = categoriedbcontroller.GetAllCategories();
            mvvm.ListProduct = productdbcontroller.GetAllProducten();
            mvvm.ListMerk = merkdbcontroler.GetAllMerken();
            mvvm.gebruiker = gebruikerdbcontroller.GetGebruiker(kid);
            mvvm.ListMand = qwkm.GetAllwinkelmand();

            return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        [HttpPost]
        public ActionResult WijzigGebruiker(MainView main)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gebruikerdbcontroller.UpdateGebruiker(main.gebruiker);
                    Session["userID"]=null; //sessie weer null maken, voor evt volgende wijziging
                    return RedirectToAction("GebruikerBeheer", "Beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult VerwijderGebruiker(int kid)
        {
            try
            {
                gebruikerdbcontroller.DeleteGebruiker(kid);
                return RedirectToAction("GebruikerBeheer", "Beheer");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        //------------------------------------Bestelling

        public ActionResult BekijkProducten(int oid)
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
                mvvm.ListOrderViewModel = orderdbcontroller.GetProductenPerOrder(oid);

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult WijzigStatus(int oid)
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
                mvvm.status = orderdbcontroller.GetStatus(oid);
                mvvm.order = orderdbcontroller.GetOrder(oid);

                List<Status> statussen = orderdbcontroller.GetAllStatussen();

                mvvm.SelectedStatusID = mvvm.status.ID;

                mvvm.Statussen = new SelectList(statussen, "ID", "Naam");

                return View(mvvm);
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        [HttpPost]
        public ActionResult WijzigStatus(MainView main)
        {

            try
            {
                Status status = orderdbcontroller.GetGekozenStatus(main.SelectedStatusID);
                main.order.Status = status.ID;
                orderdbcontroller.UpdateStatus(main.order);
                return RedirectToAction("Bestellingen", "Beheer", new { klantID = main.order.Gebruiker });
            }
            catch (Exception e)
            {
               
                return View(mvvm);
            }
        }

        //------------------------------------Merken

        public ActionResult WijzigMerk(int merkID)
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
                mvvm.Merk = merkdbcontroler.GetMerk(merkID);
                mvvm.ListMand = qwkm.GetAllwinkelmand();

                return View(mvvm);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        [HttpPost]
        public ActionResult WijzigMerk(MainView main)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    merkdbcontroler.UpdateMerk(main.Merk);

                    return RedirectToAction("Merken", "beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult ToevoegenMerk()
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

        [HttpPost]
        public ActionResult ToevoegenMerk(MainView main)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    merkdbcontroler.InsertMerk(main.Merk);
                    return RedirectToAction("Merken", "Beheer");
                }
                else
                {
                    if (Session["SESwkm"] == null)
                    {
                        Session["SESwkm"] = new Winkelmand();
                    }
                    Winkelmand qwkm = (Winkelmand)Session["SESwkm"];

                    main.ListCategorie = categoriedbcontroller.GetAllCategories();
                    main.ListProduct = productdbcontroller.GetAllProducten();
                    main.ListMerk = merkdbcontroler.GetAllMerken();
                    main.ListMand = qwkm.GetAllwinkelmand();

                    return View(main);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult VerwijderMerk(int merkID)
        {
            try
            {
                merkdbcontroler.DeleteMerk(merkID);

                return RedirectToAction("Merken", "Beheer");
            }
            catch (Exception e)
            {
                return View(e);
            }
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

        //-------------------------------------Beheerder registreren

        [HttpPost]
        public ActionResult Registreren(MainView main)
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

                    gebruikerdbcontroller.InsertBeheerder(main.gebruiker);
                    return RedirectToAction("Beheerders", "Beheer");
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
    }
}
