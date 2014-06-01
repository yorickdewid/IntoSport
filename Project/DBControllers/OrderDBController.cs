using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Project.Models;

namespace Project.DBControllers
{
    public class OrderDBController : DatabaseController
    {
        private GebruikerDBController gebruikerdbcontroller = new GebruikerDBController();
        private ProductDBController productdbcontroller = new ProductDBController();

        public void InsertOrder(int uid, double totaal)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"INSERT INTO tbl_order(Create_date, Gebruiker_id, Order_status_ID, Totaal) values (NOW(), @gebruiker_id, 3, @totaal)";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter gebruikerParam = new MySqlParameter("@gebruiker_id", MySqlDbType.Int16);
                MySqlParameter totaalParam = new MySqlParameter("@totaal", MySqlDbType.Double);

                gebruikerParam.Value = uid;
                totaalParam.Value = totaal;

                cmd.Parameters.Add(gebruikerParam);
                cmd.Parameters.Add(totaalParam);

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

        public int GetOrderID(int uid)
        {
            try
            {
                conn.Open();
                int id = 0;

                string get = "SELECT Order_ID FROM tbl_order WHERE Gebruiker_ID=@gebruiker_id";
                MySqlCommand cmd = new MySqlCommand(get, conn);

                MySqlParameter gebruikerParam = new MySqlParameter("@gebruiker_id", MySqlDbType.Int16);
                gebruikerParam.Value = uid;

                cmd.Parameters.Add(gebruikerParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    id = datareader.GetInt16("Order_ID");
                }
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine("Getgebruiker(email) " + e);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertOrderRegel(int pid, int oid, int aantal, double totaal)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                string insert = @"INSERT INTO tbl_orderregel(Product_ID, Order_ID, Create_date, Aantal, Subtotaal) values (@product_id, @order_id, NOW(), @aantal, @subtotaal)";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                MySqlParameter productParam = new MySqlParameter("@product_id", MySqlDbType.Int16);
                MySqlParameter orderParam = new MySqlParameter("@order_id", MySqlDbType.Int16);
                MySqlParameter aantalParam = new MySqlParameter("@aantal", MySqlDbType.Int16);
                MySqlParameter subtotaalParam = new MySqlParameter("@subtotaal", MySqlDbType.Double);

                productParam.Value = pid;
                orderParam.Value = oid;
                aantalParam.Value = aantal;
                subtotaalParam.Value = totaal;

                cmd.Parameters.Add(productParam);
                cmd.Parameters.Add(orderParam);
                cmd.Parameters.Add(aantalParam);
                cmd.Parameters.Add(subtotaalParam);

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

        public List<Order> GetAllOrders(int uid, int status = -1, bool ondate = false)
        {
            List<Order> orderlist = new List<Order>();
            try
            {
                conn.Open();

                String con = null;

                if (status > 0)
                {
                    con += " AND o.Order_status_ID=" + uid;
                }

                if (ondate)
                {
                    con += " AND (o.Create_date > DATE_SUB(NOW(), INTERVAL 7 DAY))";
                }

                string select = @"SELECT * FROM tbl_order o JOIN tbl_order_status s ON s.Order_status_ID=o.Order_status_ID WHERE o.Gebruiker_ID='" + uid + "'" + con + " ORDER BY o.Order_ID DESC";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int orderID = datareader.GetInt16("Order_ID");
                    DateTime createdate = datareader.GetDateTime("Create_date");
                    int gebruikerID = datareader.GetInt16("Gebruiker_ID");
                    int statusID = datareader.GetInt16("Order_status_ID");
                    string Status = datareader.GetString("Status");
                    double totaal = datareader.GetDouble("Totaal");

                    Order order = new Order { ID = orderID, CreateDate = createdate, Gebruiker = gebruikerID, Status = statusID, StrStatus = Status, Totaal = totaal };

                    orderlist.Add(order);
                }
                return orderlist;
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

        public bool CheckOrderRegelID(int oid, int uid)
        {
            List<Order> orderlist = new List<Order>();
            try
            {
                conn.Open();

                string select = @"SELECT * FROM tbl_order WHERE Order_ID='" + oid + "' AND Gebruiker_ID='" + uid + "'";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                return datareader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine("ProductDBController GetAllProducten() " + e);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Goldmembership(Gebruiker gebruiker)
        {
            Email email = new Email();
            List<Order> orderlist = GetAllOrders(gebruiker.ID, -1);
            if (orderlist.Count > 0)
            {
                double goldmember = 0.00;
                for (int i = 0; i < orderlist.Count; i++)
                {
                    DateTime date = orderlist[i].CreateDate;
                    
                    if (date.Year == DateTime.Today.Year)
                    {
                        double totaal = orderlist[i].Totaal;
                        goldmember = goldmember + totaal;
                    }
                }
                System.Diagnostics.Debug.WriteLine(goldmember);
                if (goldmember >= 500.00 && gebruiker.Goldmembership == false)
                {
                    email.berichtVan = gebruiker.Email;
                    email.Bericht = "Gefeliciteerd,<br><br>U heeft nu een goldmembership, omdat u binnen één jaar 500 euro of meer besteed heeft.<br>Daarom krijg u binnen dit jaar 4% korting op elke order.<br><br>Het Intosport team";
                    email.Onderwerp = "Goldmembership geactiveerd";
                    email.SendEmail();
                    gebruiker.Goldmembership = true;
                    gebruikerdbcontroller.UpdateGebruiker(gebruiker);
                }
                else if (goldmember >= 500.00 && gebruiker.Goldmembership == true)
                {
                    
                }
                else if (goldmember < 500.00 && gebruiker.Goldmembership == true)
                {
                    gebruiker.Goldmembership = false;
                    gebruikerdbcontroller.UpdateGebruiker(gebruiker);
                }

            }

        }

        public List<OrderRegel> GetAllOrderRegels(int oid)
        {
            List<OrderRegel> orderregellist = new List<OrderRegel>();
            try
            {
                conn.Open();

                string select = @"SELECT * FROM tbl_orderregel WHERE Order_ID='" + oid + "'";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    Product productnaam = new Product();

                    int productID = datareader.GetInt16("Product_ID");
                    int aantal = datareader.GetInt16("Aantal");
                    double totaal = datareader.GetDouble("Subtotaal");

                    productnaam = productdbcontroller.GetProduct(productID);

                    OrderRegel orderregel = new OrderRegel { ProductID = productID, OrderID = oid, ProductNaam = productnaam.Naam, Aantal = aantal, Subtotaal = totaal };

                    orderregellist.Add(orderregel);
                }
                return orderregellist;
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

        public List<Order> GetAllOrdersPerKlant(int klantid)
        {
            List<Order> orders = new List<Order>();
            try
            {
                conn.Open();
                string select = @"Select o.order_id, o.totaal, os.status, Sum(orr.aantal) as aantal from tbl_order o join tbl_order_status os on o.order_status_id = os.order_status_id join tbl_orderregel orr on o.order_id = orr.order_id where gebruiker_id = @gebruikerid group by o.order_id";

                MySqlCommand cmd = new MySqlCommand(select, conn);
                MySqlParameter idParam = new MySqlParameter("@gebruikerid", MySqlDbType.Int16);
                idParam.Value = klantid;
                cmd.Parameters.Add(idParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int id = datareader.GetInt16("order_id");
                    double totaal = datareader.GetDouble("totaal");
                    string status = datareader.GetString("status");
                    int aantal = datareader.GetInt16("aantal");
                    Order order = new Order { ID = id, Totaal = totaal, StrStatus = status, aantalProducten = aantal };

                    orders.Add(order);
                }
                return orders;
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

        public List<OrderViewModel> GetProductenPerOrder(int oid)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            try
            {
                conn.Open();

                string select = @"select * from tbl_orderregel orr join tbl_product p on orr.product_id = p.product_id join tbl_order o on orr.order_id = o.order_id where o.order_id = @id";

                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int32);
                idParam.Value = oid;
                cmd.Parameters.Add(idParam);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    Product productnaam = new Product();

                    int id = datareader.GetInt16("order_id");
                    double totaal = datareader.GetDouble("totaal");
                    int gebruiker = datareader.GetInt16("gebruiker_id");

                    Order order = new Order { ID = id, Totaal = totaal, StrStatus = "..", aantalProducten = 0, Gebruiker = gebruiker };

                    int productID = datareader.GetInt16("Product_ID");
                    int aantal = datareader.GetInt16("Aantal");
                    double subtotaal = datareader.GetDouble("Subtotaal");

                    productnaam = productdbcontroller.GetProduct(productID);

                    OrderRegel orderregel = new OrderRegel { ProductID = productID, OrderID = oid, ProductNaam = productnaam.Naam, Aantal = aantal, Subtotaal = subtotaal };

                    OrderViewModel orderview = new OrderViewModel { Order = order, Orderregel = orderregel };
                    orders.Add(orderview);
                }
                return orders;
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

        public Status GetGekozenStatus(int sid)
        {
            {
                Status status = null;
                try
                {
                    conn.Open();

                    string select = @"select order_status_id, status from tbl_order_status  where order_status_id = @id";

                    MySqlCommand cmd = new MySqlCommand(select, conn);
                    MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int16);
                    idParam.Value = sid;

                    cmd.Parameters.Add(idParam);

                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read())
                    {
                        int statusid = datareader.GetInt16("order_status_id");
                        string naam = datareader.GetString("status");

                        status = new Status { ID = statusid, Naam = naam };
                    }
                    return status;
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

        public Status GetStatus(int oid)
        {
            Status status = null;
            try
            {
                conn.Open();

                string select = @"select s.order_status_id, status from tbl_order o join tbl_order_status s on o.order_status_id = s.order_status_id where order_id = @id";

                MySqlCommand cmd = new MySqlCommand(select, conn);
                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int16);
                idParam.Value = oid;

                cmd.Parameters.Add(idParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int statusid = datareader.GetInt16("order_status_id");
                    string naam = datareader.GetString("status");

                    status = new Status { ID = statusid, Naam = naam };
                }
                return status;
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

        public List<Status> GetAllStatussen()
        {
            List<Status> Statussen = new List<Status>();
            try
            {
                conn.Open();

                string select = @"select * from tbl_order_status";
                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    int id = datareader.GetInt16("order_status_id");
                    string naam = datareader.GetString("status");

                    Status status = new Status { ID = id, Naam = naam };
                    Statussen.Add(status);
                }
                return Statussen;
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

        public void UpdateStatus(Order order)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                string update = @"update tbl_order set order_status_id = @sid where order_id = @oid";
                MySqlCommand cmd = new MySqlCommand(update, conn);

                MySqlParameter oidParam = new MySqlParameter("@oid", MySqlDbType.Int16);
                MySqlParameter sidParam = new MySqlParameter("@sid", MySqlDbType.Int16);

                oidParam.Value = order.ID;
                sidParam.Value = order.Status;

                cmd.Parameters.Add(sidParam);
                cmd.Parameters.Add(oidParam);

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

        public Order GetOrder(int oid)
        {
            Order order = null;
            try
            {
                conn.Open();
                string select = @"select * from tbl_order where order_id = @id";

                MySqlCommand cmd = new MySqlCommand(select, conn);

                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int16);

                idParam.Value = oid;

                cmd.Parameters.Add(idParam);

                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    int id = datareader.GetInt16("order_id");
                    double totaal = datareader.GetDouble("totaal");
                    int gebruiker = datareader.GetInt16("gebruiker_id");

                    order = new Order { ID = id, Totaal = totaal, StrStatus = "..", aantalProducten = 0, Gebruiker = gebruiker };
                }
                return order;
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

        public void CancelOrder(int oid)
        {
            try
            {
                conn.Open();
                string del1 = @"DELETE FROM tbl_orderregel WHERE Order_ID ="+oid;
                string del2 = @"DELETE FROM tbl_order WHERE Order_ID =" + oid;

                MySqlCommand cmd1 = new MySqlCommand(del1, conn);
                cmd1.ExecuteReader();
                conn.Close();
                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand(del2, conn);
                cmd2.ExecuteReader();
                conn.Close();

                return;
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
