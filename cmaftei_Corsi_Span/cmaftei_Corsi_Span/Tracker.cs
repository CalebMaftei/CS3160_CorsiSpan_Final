using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Tracker
    {
        //Keeps track of the time that an item is logged
        private DateTime logTIme;
        //Encrypts what is written at the bottom of the logs.
        private XOR_Encryption xorEncryption = new XOR_Encryption();
    }
}
