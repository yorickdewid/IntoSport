using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.DBControllers
{
    public class MerkDBController : DatabaseController
    {
        public void InsertMerk(Merk merk)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"insert into tbl_merk(Create_date, Merk) values(@Create_date, @Merk)";
                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter datumParam = new MySqlParameter("@Create_date", MySqlDbType.DateTime);
                MySqlParameter merkParam = new MySqlParameter("@Merk", MySqlDbType.VarChar);

                datumParam.Value = DateTime.Now;
                merkParam.Value = merk.Naam;


                cmd.Parameters.Add(datumParam);
                cmd.Parameters.Add(merkParam);


                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("CategorieDBController InsertCategorie()" + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteMerk(int merkID)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string delete = @"UPDATE tbl_merk SET Actief='N' WHERE merk_ID = @merkID";
                MySqlCommand cmd = new MySqlCommand(delete, conn);

                MySqlParameter merkIDParam = new MySqlParameter("@merkID", MySqlDbType.VarChar);

                merkIDParam.Value = merkID;

                cmd.Parameters.Add(merkIDParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();

            }
            catch (Exception e)
            {
                Console.WriteLine("CategorieDBController DeleteCategorie() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public Merk GetMerk(int merkid)
        {
            Merk merk = null;
            try
            {
                conn.Open();

                string select = @"select * from tbl_merk where merk_id = @merkid";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter merkidParam = new MySqlParameter("@merkid", MySqlDbType.Int16);
                merkidParam.Value = merkid;
                cmd.Parameters.Add(merkidParam);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    int id = datareader.GetInt16("merk_id");
                    string naam = datareader.GetString("merk");

                    merk = new Merk { ID = id, Naam = naam };
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return merk;
        }

        public List<Merk> GetAllMerken(bool nonactive=false)
        {
            List<Merk> catMerk = new List<Merk>();
            try
            {
                conn.Open();
                String act = null;
                bool actief = true;

                if (!nonactive)
                {
                    act = " AND Actief='Y'";
                }

                string select = @"SELECT * FROM tbl_merk WHERE 1=1 "+act+" ORDER BY Merk";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int merkID = datareader.GetInt16("Merk_ID");
                    string merkNaam = datareader.GetString("Merk");

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

                    Merk merk = new Merk { ID = merkID, Naam = merkNaam, Actief = actief };

                    catMerk.Add(merk);
                }
                return catMerk;
            }
            catch (Exception e)
            {
                Console.WriteLine("CategorieDBController GetAllCategories() " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateMerk(Merk merk)        
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string update = @"update tbl_merk set merk = @naam where merk_ID = @merkID";
                MySqlCommand cmd = new MySqlCommand(update, conn);

                MySqlParameter merkParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter merkIDParam = new MySqlParameter("@merkID", MySqlDbType.Int16);


                merkParam.Value = merk.Naam;
                merkIDParam.Value = merk.ID;
              
                cmd.Parameters.Add(merkParam);
                cmd.Parameters.Add(merkIDParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("MerkDBController Updatemerk()" + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }        
    }
}
