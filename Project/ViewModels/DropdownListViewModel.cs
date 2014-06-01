using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class DropdownListViewModel
    {
        public SelectList Merken { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "Het is verplicht om een merk te selecteren")]
        public int SelectedMerkID { get; set; }


        public SelectList Categorieen { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "Het is verplicht om een categorie te selecteren")]
        public int SelectedCategorieID { get; set; }
    }
}