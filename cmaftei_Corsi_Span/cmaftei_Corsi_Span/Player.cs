using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Player : User
    {
        private DateTime dateOfBirth;
        private string city;
        private string state;
        private string county;
        private string diagnosis;
        private int bestScore;
        
        public void player()
        {
            //No param constructor
        }

        //All Param Constructor
        public void player(string newUserName, string newPassword, DateTime newDOB, string newCity, string newState, string newCounty, string newDiagnosis)
        {
            this.userName = newUserName;
            this.password = newPassword;
            this.adminValue = false;
            this.bestScore = 0;
            this.dateOfBirth = newDOB;
            this.city = newCity;
            this.state = newState;
            this.county = newCounty;
            this.diagnosis = newDiagnosis;
        }

        //getters
        public string GetUserName()
        {
            return this.userName;
        }
        public string GetPassword()
        {
            return this.password;
        }
        public DateTime GetDOB()
        {
            return this.dateOfBirth;
        }
        public string GetCity()
        {
            return this.city;
        }
        public string GetState()
        {
            return this.state;
        }
        public string GetCounty()
        {
            return this.county;
        }
        public string GetDiagnosis()
        {
            return this.diagnosis;
        }
        public int GetBestScore()
        {
            return this.bestScore;
        }

        //setters
        public void GetUserName(string newUserName)
        {
            this.userName = newUserName;
        }
        public void GetPassword(string newPassword)
        {
            this.password = newPassword;
        }
        public void GetDOB(DateTime DOB)
        {
            this.dateOfBirth = DOB;
        }
        public void GetCity(string city)
        {
            this.city = city;
        }
        public void GetState( string state)
        {
            this.state = state;
        }
        public void GetCounty( string county)
        {
            this.county = county;
        }
        public void GetDiagnosis(string diagnosis)
        {
            this.diagnosis = diagnosis;
        }
        public void GetBestScore(int bestScore)
        {
            this.bestScore = bestScore;
        }

        public void SaveUserData()
        {
            //Saves these values within the appropriate text files.
        }
    }
}
