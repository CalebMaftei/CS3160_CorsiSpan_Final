using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    abstract class User
    {
        public string userName; //Name of user
        public string password; //Associated Password
        public bool adminValue; //defines if user goes to admin screen or player screen
    }
}
