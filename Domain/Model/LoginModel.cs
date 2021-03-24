using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        //public string Email { get; set; }


        public LoginModel(string a, string b)

        {
            //Email = a;
            Password = b;

        }
        public LoginModel() { }
    }
}
