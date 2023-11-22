using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace Taxi.Models
{
    public class TaxiDB
    {
        public void UpdateAll()
        {
            try
            {
                GetUsers();
                GetDrivers();
                GetOperators();
                GetClients();
                GetStatusList();
                GetRequests();
                GetDrives();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public string connectionString =
            @"Data Source=DESKTOP-OE4PBCI\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";

        public List<Request> Requests;
        public List<Client> Clients;
        public List<Driver> Drivers;
        public List<Operator> Operators;
        public List<Drive> Drives;
        public List<Status> StatusList;
        public List<User> Users;


        private void GetUsers()
        {
            Users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [User]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User item = new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Login = reader["Login"].ToString(),
                            Password = reader["Password"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                        };
                        Users.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void GetDrivers()
        {
            Drivers = new List<Driver>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [Driver]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Driver item = new Driver()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Experience = Convert.ToInt32(reader["Experience"]),
                            Rating = Convert.ToDecimal(reader["Rating"]),
                            User = Users.Find(c => c.Id == Convert.ToInt32(reader["Id"]))
                        };
                        Drivers.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void GetClients()
        {
            Clients = new List<Client>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [Client]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Client item = new Client()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            User = Users.Find(c => c.Id == Convert.ToInt32(reader["Id"]))
                        };
                        Clients.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void GetOperators()
        {
            Operators = new List<Operator>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [Operator]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Operator item = new Operator()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            User = Users.Find(c => c.Id == Convert.ToInt32(reader["Id"]))
                        };
                        Operators.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void GetRequests()
        {
            Requests = new List<Request>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [Request]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Request item = new Request();
                        item.Id = Convert.ToInt32(reader["Id"]);
                        item.AddressFrom = reader["AddressFrom"].ToString()!;
                        item.AddressWhere = reader["AddressWhere"].ToString()!;
                        item.ClientId = Convert.ToInt32(reader["ClientId"]);
                        item.Client = Users.Find(c => c.Id == Convert.ToInt32(reader["ClientId"]))!;
                        if (reader["OperatorId"] != DBNull.Value)
                        {
                            item.OperatorId = Convert.ToInt32(reader["OperatorId"]);
                            item.Operator = Users.Find(c => c.Id == Convert.ToInt32(reader["OperatorId"]));
                        }

                        item.Date = Convert.ToDateTime(reader["Date"]);
                        Requests.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void GetDrives()
        {
            Drives = new List<Drive>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [Drive]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Drive item = new Drive();
                        item.Id = Convert.ToInt32(reader["Id"]);
                        item.DriverId = Convert.ToInt32(reader["DriverId"]);
                        item.RequestId = Convert.ToInt32(reader["RequestId"]);
                        item.StatusId = Convert.ToInt32(reader["StatusId"]);
                        item.Driver = Drivers.Find(c => c.Id == Convert.ToInt32(reader["DriverId"]));
                        item.Request = Requests.Find(c => c.Id == Convert.ToInt32(reader["RequestId"]));
                        item.Status = StatusList.Find(c => c.Id == Convert.ToInt32(reader["StatusId"]));

                        Drives.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }

        private void GetStatusList()
        {
            StatusList = new List<Status>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [Status]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Status item = new Status()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        };
                        StatusList.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }
    }
}