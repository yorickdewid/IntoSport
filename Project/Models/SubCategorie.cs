using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;

namespace Project.Models
{
    public class SubCategorie
    {
        public int ID { get; set; }
        public String Naam { get; set; }
        public Categorie categorie { get; set; }
    }
}