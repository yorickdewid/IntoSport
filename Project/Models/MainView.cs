using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.ViewModels;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class MainView
    {
        public List<Categorie> ListCategorie { get; set; }
        public List<Product> ListProduct { get; set; }
        public List<Merk> ListMerk { get; set; }
        public List<Gebruiker> ListGebruiker { get; set; }
        public List<Order> ListOrder { get; set; }
        public List<OrderRegel> ListOrderRegel { get; set; }
        public Product DetailProduct { get; set; }
        public List<Mand> ListMand { get; set; }
        public LogOnViewModel LogOnViewModel { get; set; }
        public SearchViewModel SearchViewModel { get; set; }
        public Gebruiker gebruiker { get; set; }
        public Email email { get; set; }
		public Categorie categorie { get; set; }
        public SelectList Merken { get; set; }
        public int SelectedMerkID { get; set; }
        public SelectList Categorieen { get; set; }
        public int SelectedCategorieID { get; set; }

        public List<Verkoop> ListVerkoop { get; set; }
        public List<Manager> ListManager { get; set; }
        public List<OrderViewModel> ListOrderViewModel { get; set; }
        public SelectList Statussen { get; set; }
        public int SelectedStatusID { get; set; }
        public Status status { get; set; }
        public Order order { get; set; }
        public Merk Merk { get; set; }
    }
}