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

        //locations for saved data
        private string loginInfoDestination = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/user_loginInfo/login_Info.txt";
        private string aboutInfoDestination = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/user_aboutInfo/about_Info.txt";
        private string scoreInfoDestination = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/user_scoreInfo/score_Info.txt";

        //0 param constructor... potentially do not need this.
        public Player()
        {
            //No param constructor
        }

        //All Param Constructor
        public Player(string newUserName, string newPassword, DateTime newDOB, string newCity, string newState, 
            string newCounty, string newDiagnosis)
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

        //Takes the user's current information and saves the information in the appropriate txt file.
        public void SaveUserData()
        {
            //Saves these values within the appropriate text files.
            string loginInfo = String.Format("{0},{1}", this.userName, this.password);
            string scoreInfo = String.Format("{0},{1}", this.userName.ToLower(), this.bestScore);
            string aboutInfo = 
                String.Format("{0},{1},{2},{3},{4},{5}", this.userName.ToLower(), this.dateOfBirth, this.city.ToLower(), 
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());

            //Find way to update appropriate info when scores are changed
            //Create logic in case files get deleted to create appropriate files

            //Load apporpriate data to the correct files
            using (StreamWriter sw = File.AppendText(loginInfoDestination))
            {
                sw.WriteLine(loginInfo);
            }
            using (StreamWriter sw = File.AppendText(aboutInfoDestination))
            {
                sw.WriteLine(aboutInfo);
            }
            using (StreamWriter sw = File.AppendText(scoreInfoDestination))
            {
                sw.WriteLine(scoreInfo);
            }
        }

        //MOVE TO UI CLASS. MIGHT MAKE OWN CLASS. Checks if user already exists. If so, goes back to UI to send message 
        //saying can't add existing user.
        public bool CheckForRedundnantUser()
        {
            using (StreamReader sr = File.OpenText(loginInfoDestination))
            {
                string line;
                string user = "";

                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char letter in line.ToArray())
                    {
                        if (letter == ',')
                            break;
                        user += letter;
                    }
                    //Need username to be checked.
                    if (user == this.userName)
                    { 
                        return true;
                    }
                    user = "";
                }
            }
            return false;
        }
    }
}
