using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using Project.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Project.Validation;

namespace Project.Models
{
    public class Categorie
    {
        public int ID { get; set; }
		
        [Required(ErrorMessage="Dit is een verplicht veld")]
        [StringLength(30, ErrorMessage="Dit veld mag maximaal 30 tekens bevatten.")]
        public String Naam { get; set; }

        public bool Actief { get; set; }
    }
}