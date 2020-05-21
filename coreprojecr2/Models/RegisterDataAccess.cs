using coreprojecr2.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace coreprojecr2.Models
{
    public class RegisterDataAccess
    {
        string connectionstring = ConnectionString.CName;

        public IEnumerable<Register> GetAllUsers()
        {
            List<Register> users = new List<Register>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string sqlQuery = "Select * from Users";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Register user = new Register();
                    user.Email = rdr["Email"].ToString();
                    user.Password = rdr["Password"].ToString();

                    users.Add(user);
                }

            }
            return users;
        }

        public void AddUser(Register user)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string sqlQuery = "Insert into Users (UserName, Email, Password) Values(@Email, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                cmd.Parameters.AddWithValue("@UserName", user.Email);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        internal void AddUser(IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
