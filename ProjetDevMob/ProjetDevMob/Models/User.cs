using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMob.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
    
    public class UserProfil
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }

        public UserProfil(string email, string firstName, string lastName, string password)
        {
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            this.password = password;
        }
    }
}
