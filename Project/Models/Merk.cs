using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Merk
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Dit is een verplicht veld")]
        [StringLength(30, ErrorMessage = "Dit veld mag maximaal 30 tekens bevatten.")]
        public String Naam { get; set; }

        public bool Actief { get; set; }
    }
}