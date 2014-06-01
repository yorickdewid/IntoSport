using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Project.DBControllers
{
    public abstract class DatabaseController
    {
        protected MySqlConnection conn;

        public DatabaseController()
        {
            conn = new MySqlConnection("Server=localhost;Database=db_intosport;Uid=root;Pwd=usbw;");
        }    
    }
}