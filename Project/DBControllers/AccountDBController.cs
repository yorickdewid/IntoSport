using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Project.DBControllers
{
    public class AccountDBController : DatabaseController
    {
        public string[] GetRollen(string email)
        {
            try
            {
                conn.Open();

                string select = @"select * from tbl_gebruiker g join tbl_gebruiker_type t on g.type_ID = t.type_ID where email = @email";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                
                emailParam.Value = email;

                cmd.Parameters.Add(emailParam);

                List<string> rollen = new List<string>();

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    string rol = datareader.GetString("type");
                    rollen.Add(rol);
                }
                return rollen.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("AccountDBController GetRollen() " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool IsGeldig(string email, string wachtwoord)
        {
            try
            {
                conn.Open();

                string select = @"SELECT * FROM tbl_gebruiker WHERE email = @email AND wachtwoord = @wachtwoord AND Actief='Y'";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.VarChar);
                MySqlParameter wachtwoordParam = new MySqlParameter("@wachtwoord", MySqlDbType.VarChar);

                emailParam.Value = email;
                wachtwoordParam.Value = wachtwoord;

                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(wachtwoordParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                return datareader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine("AccountDBController IsGeldig() " + e);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}