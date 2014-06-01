using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;
using Project.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Project.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Dit is een verplicht veld")]
        public String Naam { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public String Beschrijving { get; set; }
        
        [Required(ErrorMessage = "Dit is een verplicht veld")]
		[Range(0, Double.MaxValue, ErrorMessage="De prijs moet een positief getal zijn")]
        [Prijs(ErrorMessage = "Noteer de prijs met een komma, niet een punt. 0 is niet toegestaan")]
        public double Prijs { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld")]
        [Range(0, Double.MaxValue, ErrorMessage = "De voorraad moet een positief getal zijn")]
        public int Voorraad { get; set; }

        public bool Actief { get; set; }

        public Merk merk { get; set; }

        public Categorie cat { get; set; }
        
        public String Image { get; set; }

        public String StrVoorraad
        {
            get
            {
                String rs = "";

                if (Voorraad >= 10)
                {
                    rs = "Voldoende";
                }
                else if((Voorraad > 0)&&(Voorraad <= 9))
                {
                   rs = "Beperkt";
                }
                else if (Voorraad <= 0)
                {
                    rs = "Bij leverancier";
                }

                return rs;
            }
        }
    }
}