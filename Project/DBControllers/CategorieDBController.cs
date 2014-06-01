using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.DBControllers
{
    public class CategorieDBController : DatabaseController
    {
        public void InsertCategorie(Categorie categorie)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"insert into tbl_categorie(Create_date,Categorie) values(@Create_date, @Categorie)";
                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter datumParam = new MySqlParameter("@Create_date", MySqlDbType.DateTime);
                MySqlParameter categorieParam = new MySqlParameter("@Categorie", MySqlDbType.VarChar);

                datumParam.Value = DateTime.Now;
                categorieParam.Value = categorie.Naam;
         

                cmd.Parameters.Add(datumParam);
                cmd.Parameters.Add(categorieParam);
    

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

        public void UpdateCategorie(Categorie categorie)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string update = @"update tbl_categorie set Categorie = @naam where Categorie_ID = @catID";
                MySqlCommand cmd = new MySqlCommand(update, conn);

                MySqlParameter catParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter catIDParam = new MySqlParameter("@catID", MySqlDbType.Int16);


                catParam.Value = categorie.Naam;
                catIDParam.Value = categorie.ID;
              
                cmd.Parameters.Add(catParam);
                cmd.Parameters.Add(catIDParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("CategorieDBController UpdateCategorie()" + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteCategorie(int catID)
        {
            MySqlTransaction trans = null;
            try
            {                
                conn.Open();

                trans = conn.BeginTransaction();

                string delete = @"UPDATE tbl_categorie SET Actief='N' WHERE categorie_ID = @catID";
                MySqlCommand cmd = new MySqlCommand(delete, conn);

                MySqlParameter catIDParam = new MySqlParameter("@catID", MySqlDbType.VarChar);

                catIDParam.Value = catID;

                cmd.Parameters.Add(catIDParam);

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

        public void DeleteAllCategories()
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
                Console.WriteLine("CategorieDBController DeleteAllCategories() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Categorie> GetAllCategories(bool nonactive = false)
        {
            List<Categorie> catList = new List<Categorie>();
            try
            {
                conn.Open();

                String act = null;
                bool actief = true;

                if (!nonactive)
                {
                    act = " WHERE Actief='Y'";
                }

                string select = @"SELECT * FROM tbl_categorie"+act;
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();
                
                while (datareader.Read())
                {
                    int categorieID = datareader.GetInt16("Categorie_ID");
                    string categorieNaam = datareader.GetString("Categorie");

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


                    Categorie categorien = new Categorie { ID = categorieID, Naam = categorieNaam, Actief = actief };

                    catList.Add(categorien);
                }
                return catList;
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

		public Categorie GetCategorie(int cid)
        {
            Categorie cat = null;
            try
            {
                conn.Open();

                string select = @"select * from tbl_categorie where categorie_id = @catid";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter idParam = new MySqlParameter("@catid", conn);
                idParam.Value = cid;
                cmd.Parameters.Add(idParam);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    int id = datareader.GetInt16("categorie_id");
                    string naam = datareader.GetString("categorie");

                    cat = new Categorie { ID = id, Naam = naam };
                }
                return cat;
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

    }
}