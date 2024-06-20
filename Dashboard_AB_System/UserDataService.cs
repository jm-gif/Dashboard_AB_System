using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Dashboard_AB_System.Services
{
    public class UserDataService
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Vren Estriber Sante\\source\\repos\\Dashboard_AB_System\\Dashboard_AB_System\\ArchiSpiresDB.mdf\";Integrated Security=True";

        public (string FirstName, string LastName) GetUserByUsername(string username)
        {
            string firstName = null;
            string lastName = null;

            string query = "SELECT FirstName, LastName FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        firstName = reader["FirstName"]?.ToString();
                        lastName = reader["LastName"]?.ToString();
                        Console.WriteLine($"User found: {username}, FirstName: {firstName}, LastName: {lastName}");
                    }
                    else
                    {
                        Console.WriteLine($"User not found for username: {username}");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving user: {ex.Message}");
                }
            }

            return (firstName, lastName);
        }


        public List<string> GetAllUsernames()
        {
            List<string> usernames = new List<string>();

            string query = "SELECT Username FROM Users";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = reader["Username"]?.ToString();
                        usernames.Add(username);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving usernames: {ex.Message}");
                }
            }

            return usernames;
        }
    }
}
