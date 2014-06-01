using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.DBControllers
{
    public class ProductDBController : DatabaseController
    {
        public enum OrderAllProducten
        {
            RANDOM,
            ALLES,
        }

        public void InsertProduct(Product product)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"insert into tbl_product(create_date, productnaam, merk_ID, categorie_id, beschrijving, prijs, voorraad, Image) values(@datum, @naam, @merk, @categorie, @beschrijving, @prijs, @voorraad, @image)";
                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter datumParam = new MySqlParameter("@datum", MySqlDbType.DateTime);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter merkParam = new MySqlParameter("@merk", MySqlDbType.Int32);
                MySqlParameter categorieParam = new MySqlParameter("@categorie", MySqlDbType.Int32);
                MySqlParameter beschrijvingParam = new MySqlParameter("@beschrijving", MySqlDbType.VarChar);
                MySqlParameter prijsParam = new MySqlParameter("@prijs", MySqlDbType.Float);
                MySqlParameter voorraadParam = new MySqlParameter("@voorraad", MySqlDbType.Int16);
                MySqlParameter imageParam = new MySqlParameter("@image", MySqlDbType.VarChar);

                datumParam.Value = DateTime.Now;
                naamParam.Value = product.Naam;
                merkParam.Value = product.merk.ID;
                categorieParam.Value = product.cat.ID;
                beschrijvingParam.Value = product.Beschrijving;
                prijsParam.Value = product.Prijs;
                voorraadParam.Value = product.Voorraad;
                imageParam.Value = product.Image;

                cmd.Parameters.Add(datumParam);
                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(merkParam);
                cmd.Parameters.Add(categorieParam);
                cmd.Parameters.Add(beschrijvingParam);
                cmd.Parameters.Add(prijsParam);
                cmd.Parameters.Add(voorraadParam);
                cmd.Parameters.Add(imageParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductenDBController InsertProduct()" + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateProduct(Product product)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string update = @"update tbl_product set merk_id = @merkid, categorie_id = @catid, productnaam = @naam, prijs = @prijs, voorraad = @voorraad, beschrijving = @beschrijving where product_ID = @productID";
                MySqlCommand cmd = new MySqlCommand(update, conn);

                MySqlParameter merkParam = new MySqlParameter("@merkid", MySqlDbType.Int16);
                MySqlParameter catParam = new MySqlParameter("@catid", MySqlDbType.Int16);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter beschrijvingParam = new MySqlParameter("@beschrijving", MySqlDbType.VarChar);
                MySqlParameter prijsParam = new MySqlParameter("@prijs", MySqlDbType.Float);
                MySqlParameter voorraadParam = new MySqlParameter("@voorraad", MySqlDbType.Int16);
                MySqlParameter idParam = new MySqlParameter("@productID", MySqlDbType.Int16);

                merkParam.Value = product.merk.ID;
                catParam.Value = product.cat.ID;
                naamParam.Value = product.Naam;
                beschrijvingParam.Value = product.Beschrijving;
                prijsParam.Value = product.Prijs;
                voorraadParam.Value = product.Voorraad;
                idParam.Value = product.ID;

                cmd.Parameters.Add(merkParam);
                cmd.Parameters.Add(catParam);
                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(beschrijvingParam);
                cmd.Parameters.Add(prijsParam);
                cmd.Parameters.Add(voorraadParam);
                cmd.Parameters.Add(idParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductenDBController UpdateProduct()" + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateProductAfbeelding(int pid, string filename)
        {
            MySqlTransaction trans = null;
            try
            {
                if (pid == null || filename == null)
                {
                    return;
                }

                conn.Open();

                trans = conn.BeginTransaction();

                string update = @"UPDATE tbl_product SET Image='" + filename + "' WHERE product_ID=" + pid;
                MySqlCommand cmd = new MySqlCommand(update, conn);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductenDBController UpdateProduct()" + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public Product GetProduct(int productID)
        {
            conn.Open();
            try
            {
                Product product = null;

                string select = @"SELECT * FROM tbl_product p join tbl_merk m on p.merk_id = m.merk_id join tbl_categorie c on p.categorie_id = c.categorie_id WHERE Product_ID = @pID LIMIT 15";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter productIDParam = new MySqlParameter("@pID", MySqlDbType.VarChar);

                productIDParam.Value = productID;

                cmd.Parameters.Add(productIDParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int merkid = datareader.GetInt16("merk_id");
                    string merknaam = datareader.GetString("merk");
                    Merk merkk = new Merk { ID = merkid, Naam = merknaam };

                    int catid = datareader.GetInt16("categorie_id");
                    string catnaam = datareader.GetString("categorie");
                    Categorie cate = new Categorie { ID = catid, Naam = catnaam };

                    int productid = datareader.GetInt16("Product_ID");
                    string productnaam = datareader.GetString("Productnaam");
                    string beschrijving = datareader.GetString("Beschrijving");
                    double prijs = datareader.GetDouble("Prijs");
                    int voorraad = datareader.GetInt16("Voorraad");
                    // -------Nullable values
                    string image;
                    if (datareader.IsDBNull(10))
                    {
                        image = "no_image.png";
                    }
                    else
                    {
                        image = datareader.GetString("Image");
                    }
                    product = new Product { ID = productid, Naam = productnaam, Beschrijving = beschrijving, Prijs = prijs, Voorraad = voorraad, Image = image, cat = cate, merk = merkk };
                }
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductDBController GetProduct() " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Product> GetAllProducten(OrderAllProducten oap = OrderAllProducten.ALLES, int cat = -1, int merk = -1, int limiet = 6, bool nonactive = false)
        {
            List<Product> producten = new List<Product>();
            try
            {
                conn.Open();

                String order = null;
                String con = null;
                String act = null;
                bool actief = true;

                if (oap == OrderAllProducten.RANDOM)
                {
                    order = "RAND()";
                }
                else if (oap == OrderAllProducten.ALLES)
                {
                    order = "1";
                }

                if (merk > 0)
                {
                    con += " AND Merk_ID=" + merk;
                }

                if (cat > 0)
                {
                    con += " AND Categorie_ID=" + cat;
                }
                if (!nonactive)
                {
                    act = " AND Actief='Y'";
                }

                string select = @"SELECT * FROM tbl_product WHERE 1=1 " + con + act + " ORDER BY " + order + " LIMIT " + limiet;
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int productID = datareader.GetInt16("Product_ID");
                    string productnaam = datareader.GetString("Productnaam");
                    string beschrijving = datareader.GetString("Beschrijving");
                    double prijs = datareader.GetDouble("Prijs");
                    int voorraad = datareader.GetInt16("Voorraad");

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

                    string image;
                    if (datareader.IsDBNull(10))
                    {
                        image = "no_image.png";
                    }
                    else
                    {
                        image = datareader.GetString("Image");
                    }

                    Product product = new Product { ID = productID, Naam = productnaam, Beschrijving = beschrijving, Prijs = prijs, Voorraad = voorraad, Image = image, Actief = actief };

                    producten.Add(product);
                }
                return producten;
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductDBController GetAllProducten() " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Product> ZoekAllProducten(string query, int cat = -1, int merk = -1, int limiet = 6, bool nonactive = false)
        {
            List<Product> producten = new List<Product>();
            try
            {
                if (query == null)
                {
                    return producten;
                }

                conn.Open();

                String order = null;
                String con = null;
                String act = null;
                bool actief = true;

                if (merk > 0)
                {
                    con += " AND Merk_ID=" + merk;
                }

                if (cat > 0)
                {
                    con += " AND Categorie_ID=" + cat;
                }
                if (!nonactive)
                {
                    act = " AND Actief='Y'";
                }

                string select = @"SELECT * FROM tbl_product WHERE (Productnaam LIKE '%" + MySqlHelper.EscapeString(query) + "%' OR Productnaam LIKE '%" + MySqlHelper.EscapeString(query) + "%') " + con + act + " LIMIT " + limiet;
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int productID = datareader.GetInt16("Product_ID");
                    string productnaam = datareader.GetString("Productnaam");
                    string beschrijving = datareader.GetString("Beschrijving");
                    double prijs = datareader.GetDouble("Prijs");
                    int voorraad = datareader.GetInt16("Voorraad");
                    // -------Nullable values
                    string image;
                    if (datareader.IsDBNull(10))
                    {
                        image = "no_image.png";
                    }
                    else
                    {
                        image = datareader.GetString("Image");
                    }
                    Product product = new Product { ID = productID, Naam = productnaam, Beschrijving = beschrijving, Prijs = prijs, Voorraad = voorraad, Image = image };

                    producten.Add(product);
                }
                return producten;
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductDBController GetAllProducten() " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Product> GetProductenPerCategorie(int catID)
        {
            List<Product> producten = new List<Product>();
            try
            {
                conn.Open();

                string select = @"select * from tbl_product p join categorie c on s.catID = c.catID where catID = @catID";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter catIDParam = new MySqlParameter("@catID", MySqlDbType.Int16);

                catIDParam.Value = catID;

                cmd.Parameters.Add(catIDParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {

                    string catnaam = datareader.GetString("c.naam");
                    Categorie cat = new Categorie { ID = catID, Naam = catnaam };

                    int productID = datareader.GetInt16("productID");
                    string productnaam = datareader.GetString("p.naam");
                    string beschrijving = datareader.GetString("beschrijving");
                    float prijs = datareader.GetFloat("prijs");
                    int voorraad = datareader.GetInt16("voorraad");
                    Product product = new Product { ID = productID, Naam = productnaam, Beschrijving = beschrijving, Prijs = prijs, Voorraad = voorraad };

                    producten.Add(product);
                }
                return producten;
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductenDBController GetProductenPerCategorie() " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void NonActiefProduct(int productID)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string delete = @"UPDATE tbl_product SET Actief='N' WHERE product_ID = @productID";
                MySqlCommand cmd = new MySqlCommand(delete, conn);

                MySqlParameter productIDParam = new MySqlParameter("@productID", MySqlDbType.VarChar);

                productIDParam.Value = productID;

                cmd.Parameters.Add(productIDParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductDBController DeleteProduct() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void ActiefProduct(int productID)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string delete = @"Update tbl_product set actief = 1 where product_ID = @productID";
                MySqlCommand cmd = new MySqlCommand(delete, conn);

                MySqlParameter productIDParam = new MySqlParameter("@productID", MySqlDbType.VarChar);

                productIDParam.Value = productID;

                cmd.Parameters.Add(productIDParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductDBController DeleteProduct() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteAllProducten()
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
                Console.WriteLine("ProductDBController DeleteAllProducten() " + e);
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}