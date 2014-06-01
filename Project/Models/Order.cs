using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Gebruiker { get; set; }
        public int Status { get; set; }
        public string StrStatus { get; set; }
        public double Totaal { get; set; }
        public int aantalProducten { get; set; }
    }

    public class OrderRegel
    {
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public string ProductNaam { get; set; }
        public int Aantal { get; set; }
        public double Subtotaal { get; set; }
    }

    public class OrderViewModel
    {
        public Order Order { get; set; }
        public OrderRegel Orderregel { get; set; }
    }
}