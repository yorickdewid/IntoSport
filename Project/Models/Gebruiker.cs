using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Project.Validation;


namespace Project.Models
{
    public class Gebruiker
    {
        public int ID { get; set; }
        private string _Postcode;


        [Required(ErrorMessage="Dit is een verplicht veld.")]
        [StringLength(50, ErrorMessage="Dit veld mag niet meer dan 50 tekens bevatten.")]
        [NaamAttribute(ErrorMessage = "Cijfers zijn niet toegestaan. Alleen deze leestekens zijn toegestaan: ` ' -")]
        public String Naam { get; set; }

        [Required(ErrorMessage="Dit is een verplicht veld.")]
        [DataType(DataType.Password)]
        public String Wachtwoord { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld.")]
        [StringLength(50, ErrorMessage = "Dit veld mag niet meer dan 50 tekens bevatten.")]
        public String Adres { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld.")]
        [Postcode(ErrorMessage = "Dit is geen geldige postcode.")]
        public String Postcode { get { return this._Postcode; } set { _Postcode = value.Replace(" ", null); } }

        [Required(ErrorMessage = "Dit is een verplicht veld.")]
        [StringLength(50, ErrorMessage = "Dit veld mag niet meer dan 50 tekens bevatten.")]
        [NaamAttribute(ErrorMessage = "Cijfers zijn niet toegestaan. Alleen deze leestekens zijn toegestaan: ` ' -")]
        public String Woonplaats { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld.")]
        public int Telefoonnummer { get; set; }

        
        [Required(ErrorMessage = "Dit is een verplicht veld.")]
        [StringLength(80, ErrorMessage = "Dit veld mag niet meer dan 80 tekens bevatten.")]
        [Email(ErrorMessage = "Dit is geen geldig email adres.")]
        [EmailExists(ErrorMessage = "Dit email adres is al gebruikt")]
        public String Email { get; set; }

        public String Type { get; set; }

        public bool Actief { get; set; }

        public int Aantal { get; set; }

        public Boolean Goldmembership { get; set; }
    }
}