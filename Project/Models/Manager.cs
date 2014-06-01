using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Models
{
    public class Verkoop
    {
        public int productID { get; set; }
        public String ProductNaam { get; set; }
        public int Aantal { get; set; }
        public double Prijs { get; set; }
        public double Totaal { get; set; }
    }
    public class Manager
    {
        public int week { get; set; }
        public double totaal { get; set; }
    }
}
