using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.DBControllers
{
    public class GebruikerDBController : DatabaseController
    {
        public void InsertGebruiker(Gebruiker gebruiker)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"insert into tbl_gebruiker(Create_date, type_id, gebruikersnaam, wachtwoord, adres, postcode, woonplaats, telefoonnr, email, actief) values (@Create_date, @type_id, @gebruikersnaam, @wachtwoord, @adres, @postcode, @woonplaats, @telefoonnr, @email, @actief)";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter dateParam = new MySqlParameter("@Create_date", MySqlDbType.DateTime);
                MySqlParameter typeParam = new MySqlParameter("@type_id", MySqlDbType.Int16);
                MySqlParameter naamParam = new MySqlParameter("@gebruikersnaam", MySqlDbType.VarChar);
                MySqlParameter wachtwoordParam = new MySqlParameter("@wachtwoord", MySqlDbType.VarChar);
                MySqlParameter adresParam = new MySqlParameter("@adres", MySqlDbType.VarChar);
                MySqlParameter postcodeParam = new MySqlParameter("@postcode", MySqlDbType.VarChar);
                MySqlParameter plaatsParam = new MySqlParameter("@woonplaats", MySqlDbType.VarChar);
                MySqlParameter nummerParam = new MySqlParameter("@telefoonnr", MySqlDbType.Int16);
                MySqlParameter mailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter actiefParam = new MySqlParameter("@actief", MySqlDbType.VarChar);

                dateParam.Value = DateTime.Now;
                typeParam.Value = 3;
                naamParam.Value = gebruiker.Naam;
                wachtwoordParam.Value = gebruiker.Wachtwoord;
                adresParam.Value = gebruiker.Adres;
                postcodeParam.Value = gebruiker.Postcode;
                plaatsParam.Value = gebruiker.Woonplaats;
                nummerParam.Value = gebruiker.Telefoonnummer;
                mailParam.Value = gebruiker.Email;
                actiefParam.Value = "Y";

                cmd.Parameters.Add(dateParam);
                cmd.Parameters.Add(typeParam);
                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(wachtwoordParam);
                cmd.Parameters.Add(adresParam);
                cmd.Parameters.Add(postcodeParam);
                cmd.Parameters.Add(plaatsParam);
                cmd.Parameters.Add(nummerParam);
                cmd.Parameters.Add(mailParam);
                cmd.Parameters.Add(actiefParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();

            }
            catch (Exception e)
            {
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertBeheerder(Gebruiker gebruiker)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"insert into tbl_gebruiker(Create_date, type_id, gebruikersnaam, wachtwoord, adres, postcode, woonplaats, telefoonnr, email, actief) values (@Create_date, @type_id, @gebruikersnaam, @wachtwoord, @adres, @postcode, @woonplaats, @telefoonnr, @email, @actief)";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter dateParam = new MySqlParameter("@Create_date", MySqlDbType.DateTime);
                MySqlParameter typeParam = new MySqlParameter("@type_id", MySqlDbType.Int16);
                MySqlParameter naamParam = new MySqlParameter("@gebruikersnaam", MySqlDbType.VarChar);
                MySqlParameter wachtwoordParam = new MySqlParameter("@wachtwoord", MySqlDbType.VarChar);
                MySqlParameter adresParam = new MySqlParameter("@adres", MySqlDbType.VarChar);
                MySqlParameter postcodeParam = new MySqlParameter("@postcode", MySqlDbType.VarChar);
                MySqlParameter plaatsParam = new MySqlParameter("@woonplaats", MySqlDbType.VarChar);
                MySqlParameter nummerParam = new MySqlParameter("@telefoonnr", MySqlDbType.Int16);
                MySqlParameter mailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter actiefParam = new MySqlParameter("@actief", MySqlDbType.VarChar);

                dateParam.Value = DateTime.Now;
                typeParam.Value = 1;
                naamParam.Value = gebruiker.Naam;
                wachtwoordParam.Value = gebruiker.Wachtwoord;
                adresParam.Value = gebruiker.Adres;
                postcodeParam.Value = gebruiker.Postcode;
                plaatsParam.Value = gebruiker.Woonplaats;
                nummerParam.Value = gebruiker.Telefoonnummer;
                mailParam.Value = gebruiker.Email;
                actiefParam.Value = "Y";

                cmd.Parameters.Add(dateParam);
                cmd.Parameters.Add(typeParam);
                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(wachtwoordParam);
                cmd.Parameters.Add(adresParam);
                cmd.Parameters.Add(postcodeParam);
                cmd.Parameters.Add(plaatsParam);
                cmd.Parameters.Add(nummerParam);
                cmd.Parameters.Add(mailParam);
                cmd.Parameters.Add(actiefParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();

            }
            catch (Exception e)
            {
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                string update = @"update tbl_gebruiker set gebruikersnaam = @naam, adres = @adres, postcode = @postcode, wachtwoord = @wachtwoord, woonplaats = @woonplaats, telefoonnr = @nummer, email = @email, goldmembership=@goldmembership where gebruiker_ID = @id";
                MySqlCommand cmd = new MySqlCommand(update, conn);

                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter adresParam = new MySqlParameter("@adres", MySqlDbType.VarChar);
                MySqlParameter postcodeParam = new MySqlParameter("@postcode", MySqlDbType.VarChar);
                MySqlParameter plaatsParam = new MySqlParameter("@woonplaats", MySqlDbType.VarChar);
                MySqlParameter nummerParam = new MySqlParameter("@nummer", MySqlDbType.Int32);
                MySqlParameter mailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter IDParam = new MySqlParameter("@id", MySqlDbType.Int32);
                MySqlParameter goldParam = new MySqlParameter("@goldmembership", MySqlDbType.Int32);
                MySqlParameter wachtwoord = new MySqlParameter("@wachtwoord", MySqlDbType.VarChar);

                naamParam.Value = gebruiker.Naam;
                adresParam.Value = gebruiker.Adres;
                postcodeParam.Value = gebruiker.Postcode;
                plaatsParam.Value = gebruiker.Woonplaats;
                nummerParam.Value = gebruiker.Telefoonnummer;
                mailParam.Value = gebruiker.Email;
                IDParam.Value = gebruiker.ID;
                wachtwoord.Value = gebruiker.Wachtwoord;

                if (gebruiker.Goldmembership == true)
                {
                    goldParam.Value = 1;
                }
                else
                {
                    goldParam.Value = 0;
                }

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(adresParam);
                cmd.Parameters.Add(postcodeParam);
                cmd.Parameters.Add(plaatsParam);
                cmd.Parameters.Add(nummerParam);
                cmd.Parameters.Add(mailParam);
                cmd.Parameters.Add(IDParam);
                cmd.Parameters.Add(goldParam);
                cmd.Parameters.Add(wachtwoord);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Updategebruiker(gebruiker)" + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public Gebruiker GetGebruiker(String email)
        {
            try
            {
                Gebruiker gebruiker = null;

                conn.Open();

                string get = "Select * from tbl_gebruiker where email = @email";
                MySqlCommand cmd = new MySqlCommand(get, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                emailParam.Value = email;

                cmd.Parameters.Add(emailParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int id = datareader.GetInt16("gebruiker_ID");
                    string naam = datareader.GetString("gebruikersnaam");
                    string adres = datareader.GetString("adres");
                    string postcode = datareader.GetString("postcode");
                    string mail = datareader.GetString("email");
                    string woonplaats = datareader.GetString("woonplaats");
                    int nummer = datareader.GetInt32("telefoonnr");
                    Boolean goldmembership;
                    if (datareader.GetInt16("Goldmembership") == 0)
                    {
                        goldmembership = false;
                    }
                    else
                    {
                        goldmembership = true;
                    }

                    gebruiker = new Gebruiker { ID = id, Naam = naam, Adres = adres, Postcode = postcode, Email = mail, Woonplaats = woonplaats, Telefoonnummer = nummer, Goldmembership = goldmembership };
                }
                datareader.Close();
                return gebruiker;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Getgebruiker(email)" + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public Gebruiker GetGebruiker(int klantid)
        {
            try
            {
                Gebruiker gebruiker = null;

                conn.Open();

                string get = "Select * from tbl_gebruiker where gebruiker_id = @id";
                MySqlCommand cmd = new MySqlCommand(get, conn);

                MySqlParameter IDParam = new MySqlParameter("@id", MySqlDbType.VarChar);
                IDParam.Value = klantid;

                cmd.Parameters.Add(IDParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int id = datareader.GetInt16("gebruiker_ID");
                    string naam = datareader.GetString("gebruikersnaam");
                    string adres = datareader.GetString("adres");
                    string postcode = datareader.GetString("postcode");
                    string mail = datareader.GetString("email");
                    string woonplaats = datareader.GetString("woonplaats");
                    int nummer = datareader.GetInt32("telefoonnr");
                    Boolean goldmembership;
                    
                    if (datareader.GetInt16("Goldmembership") == 0)
                    {
                        goldmembership = false;
                    }
                    else
                    {
                        goldmembership = true;
                    }
                    if ( HttpContext.Current.User.IsInRole("Beheerder"))
                    {
                         String wachtwoord=datareader.GetString("wachtwoord");
                         gebruiker = new Gebruiker { ID = id, Naam = naam, Adres = adres, Postcode = postcode, Email = mail, Woonplaats = woonplaats, Telefoonnummer = nummer, Goldmembership = goldmembership, Wachtwoord=wachtwoord };
                    }
                    else{
                         gebruiker = new Gebruiker { ID = id, Naam = naam, Adres = adres, Postcode = postcode, Email = mail, Woonplaats = woonplaats, Telefoonnummer = nummer, Goldmembership = goldmembership};
                    }

                   
                }
                return gebruiker;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Getgebruiker(email)" + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteGebruiker(int gebruikerID)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                string delete = @"UPDATE tbl_gebruiker SET Actief='N' WHERE Gebruiker_ID = @gebruikerID";
                MySqlCommand cmd = new MySqlCommand(delete, conn);

                MySqlParameter gebruikerIDParam = new MySqlParameter("@gebruikerID", MySqlDbType.Int16);

                gebruikerIDParam.Value = gebruikerID;

                cmd.Parameters.Add(gebruikerIDParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("GebruikerDBController DeleteGebruiker() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteAllGebruikers()
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string delete = @"delete from gebruiker";
                MySqlCommand cmd = new MySqlCommand(delete, conn);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("GebruikerDBController DeleteAllGebruikers() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Gebruiker> GetAllGebruikers(bool nonactive = false)
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            try
            {
                conn.Open();


                String act = null;
                bool actief = true;

                if (!nonactive)
                {
                    act = "WHERE Actief='Y'";
                }

                string select = @"select * from tbl_gebruiker g join tbl_gebruiker_type t on g.type_id = t.type_id " + act;
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int id = datareader.GetInt32("gebruiker_id");
                    string naam = datareader.GetString("gebruikersnaam");
                    string adres = datareader.GetString("adres");
                    string postcode = datareader.GetString("postcode");
                    string woonplaats = datareader.GetString("woonplaats");
                    int telefoonnummer = datareader.GetInt32("telefoonnr");
                    string email = datareader.GetString("email");
                    string type = datareader.GetString("type");

                    if (nonactive)
                    {
                        string tmp = datareader.GetString("Actief");
                        if (tmp.Equals("Y"))
                        {
                            actief = true;
                        }
                        else if (tmp.Equals("N"))
                        {
                            actief = false;
                        }
                    }

                    Gebruiker gebruiker = new Gebruiker { ID = id, Naam = naam, Adres = adres, Postcode = postcode, Woonplaats = woonplaats, Telefoonnummer = telefoonnummer, Email = email, Type = type, Actief = actief };
                    gebruikers.Add(gebruiker);
                }
                return gebruikers;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Gebruiker> GetAllBestelGebruikers()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            try
            {
                conn.Open();

                string select = @"select *, count(o.order_id) as aantal from tbl_gebruiker g join tbl_order o on g.gebruiker_id = o.gebruiker_id group by g.gebruiker_id";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {

                    int aantal = datareader.GetInt16("aantal");
                    int id = datareader.GetInt32("gebruiker_id");
                    string naam = datareader.GetString("gebruikersnaam");
                    string adres = datareader.GetString("adres");
                    string postcode = datareader.GetString("postcode");
                    string woonplaats = datareader.GetString("woonplaats");
                    int telefoonnummer = datareader.GetInt32("telefoonnr");
                    string email = datareader.GetString("email");

                    Gebruiker gebruiker = new Gebruiker { ID = id, Naam = naam, Adres = adres, Postcode = postcode, Woonplaats = woonplaats, Telefoonnummer = telefoonnummer, Email = email, Aantal = aantal };
                    gebruikers.Add(gebruiker);
                }
                return gebruikers;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Gebruiker> GetAllBeheerders()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            try
            {
                conn.Open();

                string select = @"select * from tbl_gebruiker where type_id = 1";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {


                    int id = datareader.GetInt32("gebruiker_id");
                    string naam = datareader.GetString("gebruikersnaam");
                    string adres = datareader.GetString("adres");
                    string postcode = datareader.GetString("postcode");
                    string woonplaats = datareader.GetString("woonplaats");
                    int telefoonnummer = datareader.GetInt32("telefoonnr");
                    string email = datareader.GetString("email");

                    Gebruiker gebruiker = new Gebruiker { ID = id, Naam = naam, Adres = adres, Postcode = postcode, Woonplaats = woonplaats, Telefoonnummer = telefoonnummer, Email = email };
                    gebruikers.Add(gebruiker);
                }
                return gebruikers;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public Boolean DoesExist(int gebruiker_ID)
        {
            try
            {
                if (gebruiker_ID > 0)
                {
                    conn.Open();

                    string get = "select Email from tbl_gebruiker where gebruiker_ID =" + gebruiker_ID + "";
                    MySqlCommand cmd = new MySqlCommand(get, conn);

                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read())
                    {

                        string mail = datareader.GetString("Email");
                        if (mail != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Getgebruiker(email)" + e);
                return true;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}