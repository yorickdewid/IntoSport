using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.DBControllers
{
    public class ManagementDBController : DatabaseController
    {
        //
        // GET: /ManagementDB/
        public List<Manager> VerkrijgOmzetPerWeek()
        {
            List<Manager> managerList = new List<Manager>();
            try
            {
                conn.Open();

                string select = @"SELECT Sum(Totaal)AS totaal, DATE_FORMAT(Create_date,'%v')AS Datum FROM tbl_order ord GROUP BY Datum";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int week = datareader.GetInt16("Datum");
                    double totaal = datareader.GetDouble("totaal");

                    Manager manager = new Manager { week = week, totaal = totaal };

                    managerList.Add(manager);
                }
                return managerList;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ManagementDBController " + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //----------------

        public List<Verkoop> ProductenVerkoop()
        {
            List<Verkoop> verkoopList = new List<Verkoop>();
            try
            {
                conn.Open();

                string select = @"SELECT prod.Product_ID , prod.Productnaam , prod.Prijs , SUM(ordregel.Aantal)AS Aantal ," +
                    " SUM(ordregel.Subtotaal) AS Totaal FROM tbl_product prod " +
                    " LEFT JOIN tbl_orderregel ordregel ON prod.Product_ID=ordregel.Product_ID " +
                    " GROUP BY prod.Product_ID ORDER BY Aantal DESC";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int id = datareader.GetInt32("Product_ID");
                    string temp;
                    //---null values
                    double totaal;
                    if (!datareader.IsDBNull(4))
                    {
                        totaal = datareader.GetDouble("Totaal");
                    }
                    else
                    {
                        totaal = 0;
                    }
                    double prijs = datareader.GetDouble("Prijs");
                    //---null values
                    int aantal;
                    if (!datareader.IsDBNull(3))
                    {
                        aantal = datareader.GetInt32("Aantal");
                    }
                    else
                    {
                        aantal = 0;
                    }
                    string productnaam = datareader.GetString("Productnaam");

                    Verkoop verkoop = new Verkoop { productID = id, Totaal = totaal, Prijs = prijs, Aantal = aantal, ProductNaam = productnaam };

                    verkoopList.Add(verkoop);
                }
                return verkoopList;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ManagementDBController Productenverkoop" + e);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
