using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private List<int> scoreHistory;
        private string saveInfo;
        private string trackerLog;
        private XOR_Encryption xorEncrypt = new XOR_Encryption();

        //location for saved data
        private readonly string loadInfoDestination = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/loadPlayers.txt";

        //0 param constructor... potentially do not need this.
        public Player()
        {
            //No param constructor
        }

        //Constructor meant for making a brand new player
        public Player(
            string newUserName, string newPassword, int newBestScore, DateTime newDOB,
            string newCity, string newState, string newCounty, string newDiagnosis
            )
        {
            this.userName = newUserName;
            this.password = newPassword;
            this.adminValue = false;
            this.bestScore = newBestScore;
            this.scoreHistory = new List<int>{0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            this.dateOfBirth = newDOB;
            this.city = newCity;
            this.state = newState;
            this.county = newCounty;
            this.diagnosis = newDiagnosis;
            this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}",
                this.userName.ToLower(), this.password, this.bestScore, this.scoreHistory[0], this.scoreHistory[1],
                this.scoreHistory[2], this.scoreHistory[3], this.scoreHistory[4], this.scoreHistory[5], this.scoreHistory[6],
                this.scoreHistory[7], this.scoreHistory[8], this.scoreHistory[9], this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());
            this.trackerLog = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/trackerLogs/" + this.userName + ".txt";
            File.CreateText(trackerLog);
        }

        //All Param Constructor
        public Player(
            string newUserName, string newPassword, int newBestScore, List<int> scoreHistory, DateTime newDOB, 
            string newCity, string newState, string newCounty, string newDiagnosis
            )
        {
            this.userName = newUserName;
            this.password = newPassword;
            this.adminValue = false;
            this.bestScore = newBestScore;
            this.scoreHistory = scoreHistory;
            this.dateOfBirth = newDOB;
            this.city = newCity;
            this.state = newState;
            this.county = newCounty;
            this.diagnosis = newDiagnosis;
            this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}",
                this.userName.ToLower(), this.password, this.bestScore, this.scoreHistory[0], this.scoreHistory[1],
                this.scoreHistory[2], this.scoreHistory[3], this. scoreHistory[4], this.scoreHistory[5], this.scoreHistory[6],
                this.scoreHistory[7], this.scoreHistory[8], this.scoreHistory[9], this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());
            this.trackerLog = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/trackerLogs/" + this.userName + ".txt";
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

        public List<int> GetScoreHistory()
        {
            return this.scoreHistory;
        }

        public string GetTrackerLog()
        {
            return this.trackerLog;
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
            if(this.bestScore < bestScore)
            {
                this.bestScore = bestScore;
                UpdatePlayerInfo();
            }
        }

        public void SetScoreHistory(List<int> scoreHistory)
        {
            this.scoreHistory = scoreHistory;
        }

        //Resets the saveInfo of the active player
        public void ResetSaveInfo()
        {
            this.saveInfo = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}",
                this.userName.ToLower(), this.password, this.bestScore, this.scoreHistory[0], this.scoreHistory[1],
                this.scoreHistory[2], this.scoreHistory[3], this.scoreHistory[4], this.scoreHistory[5], this.scoreHistory[6],
                this.scoreHistory[7], this.scoreHistory[8], this.scoreHistory[9], this.dateOfBirth.Date, this.city.ToLower(),
                this.state.ToLower(), this.county.ToLower(), this.diagnosis.ToLower());
        }

        //Takes the user's current information and saves the information in the appropriate txt file.
        public void SaveUserData()
        {
            ResetSaveInfo();
            //Find way to update appropriate info when scores are changed
            //Create logic in case files get deleted to create appropriate files

            try
            {
                using (StreamWriter sw = File.AppendText(loadInfoDestination))
                {
                    sw.WriteLine(xorEncrypt.EncryptDecrypt(this.saveInfo, 307));
                }
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("User was not saved into DB, as DB could not be found.");
            }
        }

        //NOT USED AS ADMIN FUNCTIONALITY HAS NOT BEEN GIVEN THIS PERMISSION YET.
        public void DeletePlayer()
        {
            string assignmentsLocation = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/loadPlayers.txt";
            string tempFile = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/tempFile.txt";

            ResetSaveInfo();

            using (var sr = new StreamReader(assignmentsLocation))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != this.saveInfo)
                        sw.WriteLine(line);
                }
            }

            File.Delete(assignmentsLocation);
            File.Move(tempFile, assignmentsLocation);
        }

        //Updates player information whenever contents change.
        public void UpdatePlayerInfo()
        {
            //File.Create(AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/tempFile.txt");
            string loadPlayerLocation = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/loadPlayers.txt";
            string tempFile = AppDomain.CurrentDomain.BaseDirectory + @"playerInfo/tempFile.txt";

            //For this to be correct with the database, we consistently need to update the DB as we make changes.
            //Might want to to make the update function a private function, and place it into each setter

            try
            {
                using (StreamReader sr = new StreamReader(loadPlayerLocation))
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    string line;
                    while ((line = xorEncrypt.EncryptDecrypt(sr.ReadLine(), 307)) != null)
                    {
                        if (line != this.saveInfo)
                        {
                            //If line isn't the one that needs to be updated, then no revision needed
                            sw.WriteLine(xorEncrypt.EncryptDecrypt(line, 307));
                        }
                        else
                        {
                            //If line is the one that needs to be updated, then create revision
                            ResetSaveInfo();
                            sw.WriteLine(xorEncrypt.EncryptDecrypt(this.saveInfo, 307));
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
            catch (FileNotFoundException)
            {
                MessageBox.Show("DB could not be found. Values will not persist.");
            }
        }

        //MOVE TO UI CLASS. MIGHT MAKE OWN CLASS. Checks if user already exists. If so, goes back to UI to send message 
        //saying can't add existing user.
        public bool CheckForRedundnantUser()
        {
            try
            {
                using (StreamReader sr = File.OpenText(loadInfoDestination))
                {
                    string line;
                    string[] playerInfo;

                    while ((line = xorEncrypt.EncryptDecrypt(sr.ReadLine(), 307)) != null)
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
            catch
            {
                MessageBox.Show("No DB could be found. Allow Player to go through.\n NOTE: Any progress made will not be saved.");
                return true;
            }
        }
    }
}
