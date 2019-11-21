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
        private string saveInfo;

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
            this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                this.userName.ToLower(), this.password, this.bestScore, this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());
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
            //resetSaveInfo();
            updateUserScores();
        }

        public void resetSaveInfo()
        {
            this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                this.userName.ToLower(), this.password, this.bestScore, this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());
        }

        //Takes the user's current information and saves the information in the appropriate txt file.
        public void SaveUserData()
        {
            this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                this.userName.ToLower(), this.password, this.bestScore, this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());

            //Find way to update appropriate info when scores are changed
            //Create logic in case files get deleted to create appropriate files

            using (StreamWriter sw = File.AppendText(loadInfoDestination))
            {
                sw.WriteLine(this.saveInfo);
            }
        }

        //NEEDS TO BE TESTED
        public void deleteAssignment()
        {
            string assignmentsLocation = AppDomain.CurrentDomain.BaseDirectory + @"textFileBackups/assignment_backUps.txt";
            string tempFile = AppDomain.CurrentDomain.BaseDirectory + @"textFileBackups/tempFile.txt";

            //For this to be correct with the database, we consistently need to update the DB as we make changes.
            //Might want to to make the update function a private function, and place it into each setter.
            string SaveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                this.userName.ToLower(), this.password, this.bestScore, this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());

            using (var sr = new StreamReader(assignmentsLocation))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != SaveInfo)
                        sw.WriteLine(line);
                }
            }

            File.Delete("file.txt");
            File.Move(tempFile, "file.txt");
        }

        //NEEDS TO BE TESTED
        private void updateUserScores()
        {
            //File.Create(AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/tempFile.txt");
            string loadPlayerLocation = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/loadPlayers.txt";
            string tempFile = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/tempFile.txt";

            //For this to be correct with the database, we consistently need to update the DB as we make changes.
            //Might want to to make the update function a private function, and place it into each setter

            using (var sr = new StreamReader(loadPlayerLocation))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != this.saveInfo)
                    {
                        //If line isn't the one that needs to be updated, then no revision needed
                        sw.WriteLine(line);
                    }
                    else
                    {
                        //If line is the one that needs to be updated, then create revision
                        this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                            this.userName.ToLower(), this.password, this.bestScore, this.dateOfBirth.Date, this.city.ToLower(),
                            this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());
                        sw.WriteLine(saveInfo);
                    }
                }
            }
            //Removes content in Score Location
            File.Delete(loadPlayerLocation);
            //Transfers temp content into Score Location
            File.Copy(tempFile, loadPlayerLocation);
            //Removes content in tempFile
            File.WriteAllText(tempFile, string.Empty);
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
