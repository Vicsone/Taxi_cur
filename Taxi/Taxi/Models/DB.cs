using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Taxi
{
    public class DB
    {
        public DB()
        {
            StatusList = GetStatusList();
            Users = GetUsers();
            Requests = GetRequests();
        }

        public static DB _dB = new DB();

        public string connectionString = @"Data Source=DESKTOP-OE4PBCI\SQLEXPRESS;Initial Catalog=SecurityCompany2;Integrated Security=True";

        public List<Request> Requests;
        public List<Client> Clients;
        public List<Driver> Drivers;
        public List<Operator> Operators;
        public List<Drive> Drives;
        public List<Status> StatusList;
        public List<User> Users;


        private List<User> GetUsers()
        {
            List<User> list = new List<User>();
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
                        list.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return list;
        }
        

        private List<Request> GetRequests()
        {
            List<Request> list = new List<Request>();
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
                        item.Address = reader["Address"].ToString()!;
                        item.StatusId = Convert.ToInt32(reader["StatusId"]);
                        item.ClientId = Convert.ToInt32(reader["ClientId"]);
                        if (reader["EmployeeId"] != DBNull.Value)
                        {
                            item.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                            item.Employee = Users.Find(c => c.Id == Convert.ToInt32(reader["EmployeeId"]));
                        }

                        item.Status = StatusList.Find(c => c.Id == Convert.ToInt32(reader["StatusId"]))!;
                        item.Client = Users.Find(c => c.Id == Convert.ToInt32(reader["ClientId"]))!;
                        list.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return list;
        }

        private List<RequestService> GetRequestServices()
        {
            List<RequestService> list = new List<RequestService>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from [RequestService]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        RequestService item = new RequestService()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RequestId = Convert.ToInt32(reader["RequestId"]),
                            ServiceId = Convert.ToInt32(reader["ServiceId"]),
                            Request = Requests.Find(c => c.Id == Convert.ToInt32(reader["RequestId"]))!,
                            Service = Services.Find(c => c.Id == Convert.ToInt32(reader["ServiceId"]))!
                        };
                        list.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return list;
        }

        private List<Status> GetStatusList()
        {
            List<Status> list = new List<Status>();
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
                            Name = reader["Name"].ToString()!
                        };
                        list.Add(item);
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return list;
        }
    }
}