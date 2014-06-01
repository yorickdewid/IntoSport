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
    public class Email
    {
        [Required(ErrorMessage = "Dit is een verplicht veld.")]
        [Email(ErrorMessage = "Dit is geen geldig email adres.")]
        public String berichtVan { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld")]
        [StringLength(30, ErrorMessage = "Het onderwerp mag maximaal 30 karakers bevatten.")]
        public String Onderwerp { get; set; }

        [Required(ErrorMessage = "Dit is een verplicht veld")]
        public String Bericht { get; set; }
        
        public void SendEmailToWebshop()
        {
            
            //Om te testen moet je een valide emailadresVan invoeren met bijbehorend password.
                                        
            string emailAdresVan = "intosportgroningen@gmail.com";
            string password = "geheim1@";

            Bericht ="Dit bericht is afkomstig van " + berichtVan + "\n\n" +  Bericht;

            //MailMessage object aanmaken.
            MailMessage msg = new MailMessage();
            //Properties van het zojuist aangemaakte MailMessage zetten
            msg.From = new MailAddress(emailAdresVan);
            msg.To.Add(new MailAddress("intosportgroningen@gmail.com"));
            msg.Subject = Onderwerp;
            msg.Body = Bericht;
            msg.IsBodyHtml = true;

            //Als SmtpClient ga ik in dit voorbeeld uit van gmail. Je bent natuurlijk vrij om een ander smtpclient te kiezen. 
            //SmtpClient aanmaken "smtp.gmail.com" (= smtp server gmail) 587 (= Port 587 is voor gebruikers om emails over te versturen.)
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //NetworkCredential object aanmaken. Emailadres en password zijn de constructorparameters
            NetworkCredential loginInfo = new NetworkCredential(emailAdresVan, password);

            //Properties van het zojuist aangemaakte SmtpClient zetten
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = loginInfo;
            //Bericht daadwerkelijk versturen
            smtpClient.Send(msg); 
            
        }

        public void SendEmail()
        {
            try
            {

                //Om te testen moet je een valide emailadresVan invoeren met bijbehorend password.
                MainView mvvm = new MainView();
                string emailAdresVan = "intosportgroningen@gmail.com";
                string password = "geheim1@";
                string berichtnaar = berichtVan;

                //MailMessage object aanmaken.
                MailMessage msg = new MailMessage();
                //Properties van het zojuist aangemaakte MailMessage zetten
                msg.From = new MailAddress(emailAdresVan);
                msg.To.Add(new MailAddress(berichtnaar));
                msg.Subject = Onderwerp;
                msg.Body = Bericht;
                msg.IsBodyHtml = true;

                //Als SmtpClient ga ik in dit voorbeeld uit van gmail. Je bent natuurlijk vrij om een ander smtpclient te kiezen. 
                //SmtpClient aanmaken "smtp.gmail.com" (= smtp server gmail) 587 (= Port 587 is voor gebruikers om emails over te versturen.)
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //NetworkCredential object aanmaken. Emailadres en password zijn de constructorparameters
                NetworkCredential loginInfo = new NetworkCredential(emailAdresVan, password);

                //Properties van het zojuist aangemaakte SmtpClient zetten
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = loginInfo;
                //Bericht daadwerkelijk versturen
                smtpClient.Send(msg);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Mail versturen goldmember error: " + e);
            }

        }
    }
}
