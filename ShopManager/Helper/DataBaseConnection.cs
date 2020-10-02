using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShopManager.Model;
using ShopManager.Properties;

namespace ShopManager.Helper
{
    public class DataBaseConnection
    {
        private MySqlConnection connection;
        private string connectionString;
        private string server;
        private string userId;
        private string secret;
        private string databaseName;

        public DataBaseConnection()
        {
            server = (string)Settings.Default["DatabaseServer"];
            userId = (string)Settings.Default["DatabaseUserId"];
            secret = (string)Settings.Default["DatabaseSecret"];
            databaseName = (string)Settings.Default["DatabaseName"];
            connectionString = string.Format(@"server={0};userid={1};password={2};database={3}", 
                server, userId, secret, databaseName);
        }

        public List<Customer> SelectAllCustomers()
        {
            string sql = "SELECT * FROM customers";
            List<Customer> customersFromDB = new List<Customer>();

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
        
                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            Dictionary<int, Address> addressesDict = SelectAllAddresses();
                            while (rdr.Read())
                            {
                                Address address = addressesDict[rdr.GetInt32("address")];
                                customersFromDB.Add(new Customer(rdr.GetInt32("id"), rdr.GetString("firstname"), rdr.GetString("surname"), address));
                            }
                        }
                    }
                }
                catch (MySqlException)
                {
                    Console.WriteLine("Database connection could not be established, is the server running?");
                }
            }
            return customersFromDB;
        }

        public Dictionary<int, Address> SelectAllAddresses()
        {
            string sql = "SELECT * FROM addresses";
            Dictionary<int, Address> addressesFromDB = new Dictionary<int, Address>();

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                addressesFromDB.Add(rdr.GetInt32("id"), 
                                    new Address(rdr.GetInt32("id"), rdr.GetString("street"), rdr.GetString("housenumber"), 
                                    rdr.GetString("postalcode"), rdr.GetString("city")));
                            }
                        }
                    }
                }
                catch (MySqlException)
                {
                    Console.WriteLine("Database connection could not be established, is the server running?");
                }
            }
            return addressesFromDB;
        }
    }
}
