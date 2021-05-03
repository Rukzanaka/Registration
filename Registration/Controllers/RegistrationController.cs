using Registration.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class RegistrationController : ApiController
    {
        string connectionString = "Server=DESKTOP-T7TL75O\\SQLEXPRESS;Database=StudentRegistration;Trusted_Connection=True;";
        [HttpPost]

        public string Registration(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("Registration", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("Name", student.Name);
            command.Parameters.AddWithValue("Age", student.Age);
            command.Parameters.AddWithValue("Email", student.Email);
            command.Parameters.AddWithValue("UserName", student.UserName);
            command.Parameters.AddWithValue("Password", student.Password);

            command.ExecuteNonQuery();
            connection.Close();

            return "Successfully Register";
        }
        [HttpPost]
        public string Login(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("Login", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("UserName", student.UserName);
            command.Parameters.AddWithValue("Password", student.Password);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            student.Name = (string)reader["Name"];
            student.Age = (int)reader["Age"];
            student.Email = (string)reader["Email"];
            student.UserName = (string)reader["UserName"];

            return "Welcome" + " "  + student.Name 
            + student.Age + " " +  student.Email + " " + student.UserName;
            
        }
    }
}
