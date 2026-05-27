using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM2
{
    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Login()
        {
            if (Username == "admin" && Password == "1234")
                return true;
            else
                return false;
        }
    }
    
}
