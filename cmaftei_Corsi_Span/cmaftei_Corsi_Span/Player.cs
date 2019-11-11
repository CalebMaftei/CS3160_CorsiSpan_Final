using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Player : User
    {
        //Properties
        private DateTime dateOfBirth;
        private string city;
        private string state;
        private string county;
        private string diagnosis;
        private int bestScore;

        //location for saved data
        private string loadInfoDestination = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/loadPlayers.txt";

        //0 param constructor... potentially do not need this.
        public Player()
        {
            //No param constructor
        }

        //All Param Constructor
        public Player(
            string newUserName, string newPassword, int newBestScore, DateTime newDOB, 
            string newCity, string newState, string newCounty, string newDiagnosis
            )
        {
            this.userName = newUserName;
            this.password = newPassword;
            this.adminValue = false;
            this.bestScore = newBestScore;
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
        public void SetUserName(string newUserName)
        {
            this.userName = newUserName;
        }

        public void SetPassword(string newPassword)
        {
            this.password = newPassword;
        }

        public void SetDOB(DateTime DOB)
        {
            this.dateOfBirth = DOB;
        }

        public void SetCity(string city)
        {
            this.city = city;
        }

        public void SetState( string state)
        {
            this.state = state;
        }

        public void SetCounty( string county)
        {
            this.county = county;
        }

        public void SetDiagnosis(string diagnosis)
        {
            this.diagnosis = diagnosis;
        }

        public void SetBestScore(int bestScore)
        {
            this.bestScore = bestScore;
        }

        //Takes the user's current information and saves the information in the appropriate txt file.
        public void SaveUserData()
        {
            string loadInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                this.userName.ToLower(), this.password, this.bestScore, this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());

            //Find way to update appropriate info when scores are changed
            //Create logic in case files get deleted to create appropriate files

            using (StreamWriter sw = File.AppendText(loadInfoDestination))
            {
                sw.WriteLine(loadInfo);
            }
        }

        //MOVE TO UI CLASS. MIGHT MAKE OWN CLASS. Checks if user already exists. If so, goes back to UI to send message 
        //saying can't add existing user.
        public bool CheckForRedundnantUser()
        {
            using (StreamReader sr = File.OpenText(loadInfoDestination))
            {
                string line;
                string[] playerInfo;

                while ((line = sr.ReadLine()) != null)
                {
                    playerInfo = line.Split(',');

                    //Need username to be checked.
                    if (playerInfo[0] == this.userName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
