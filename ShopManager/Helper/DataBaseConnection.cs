using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShopManager.Model;

namespace ShopManager.Helper
{
    public class DataBaseConnection
    {
        private MySqlConnection connection;
        private string connectionString;
        private string server = "localhost";
        private string userId = "root";
        private string password = "";
        private string dataBaseName = "stockandorders";

        public DataBaseConnection()
        {
            connectionString = string.Format(@"server={0};userid={1};password={2};database={3}", 
                server, userId, password, dataBaseName);
        }

        public List<Customer> SelectAllCustomers()
        {
            string sql = "SELECT * FROM customer";
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
                                Address address = addressesDict[rdr.GetInt32(3)];
                                customersFromDB.Add(new Customer(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), address));
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
            string sql = "SELECT * FROM address";
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
                                addressesFromDB.Add(rdr.GetInt32(0), 
                                    new Address(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), 
                                    rdr.GetString(3), rdr.GetString(4)));
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
