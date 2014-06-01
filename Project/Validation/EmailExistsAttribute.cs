using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Project.DBControllers;
using Project.Models;

namespace Project.Validation
{
    public class EmailExistsAttribute : ValidationAttribute 
    {
        public override bool IsValid(Object value)
        {
            GebruikerDBController gbdbc = new GebruikerDBController();
            if (value != null)
            {
                String input = (String)value;
                Gebruiker gebruiker = gbdbc.GetGebruiker(input);
                if (gebruiker != null)
                {
                    int gebruiker_ID = 0;

                    //als er ingelogd is
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (HttpContext.Current.Session["userID"] != null)
                        {
                            gebruiker_ID = (Int32)HttpContext.Current.Session["userID"];
                        }
                    }

                    //als er daadwerkelijk gebruiker is
                    if (gebruiker_ID > 0)
                    {
                        Gebruiker gebruiker_temp;
                        gebruiker_temp = gbdbc.GetGebruiker(gebruiker_ID);
                        if (gbdbc.DoesExist(gebruiker_ID))
                        {
                            //als een beheerder een account wil wijzigen
                            if (HttpContext.Current.User.IsInRole("Beheerder"))
                            {
                                //in dit geval kan de email niet vergeleken worden met de ingelogde gebruiker
                                if (gebruiker.Email == gebruiker_temp.Email && gebruiker.ID == gebruiker_ID)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            
                            if (gebruiker.Email == HttpContext.Current.User.Identity.Name && gebruiker.ID == gebruiker_ID)
                            {
                                return true; //bij wijzigen kan hetzelfde mailadres worden gehouden
                            }
                            else if (gebruiker.Email == HttpContext.Current.User.Identity.Name && gebruiker.ID != gebruiker_ID)
                            {
                                return false; //als de email in de db bevind en het id niet klopt kan je niet wijzigen.
                            }
                        }
                        else if (gbdbc.DoesExist(gebruiker_ID)==false)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        //als de email voorkomt in de db
                        if (gbdbc.DoesExist(gebruiker.ID)==true)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false; //als gebruiker niet null is
                }
                else
                {
                    return true; //als null dan valid
                }
            }
            else
            {
                return false;//als de input leeg is
            }
        }
    }
}