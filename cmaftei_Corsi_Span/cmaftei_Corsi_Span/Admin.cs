using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Admin : User
    {
        public Admin(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        //getters
        public string GetUsername()
        {
            return this.userName;
        }

        public string GetPassword()
        {
            return this.password;
        }
    }
}
