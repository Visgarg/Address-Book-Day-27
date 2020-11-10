using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBook_ADO.NET
{
    class DBConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            //making connection string
            string conn = @"Data Source=DESKTOP-ERFDFCL\SQLEXPRESS01;Initial Catalog=AddressBookServiceDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //making sql connection
            SqlConnection connection = new SqlConnection(conn);
            return connection;
        }
    }
}
