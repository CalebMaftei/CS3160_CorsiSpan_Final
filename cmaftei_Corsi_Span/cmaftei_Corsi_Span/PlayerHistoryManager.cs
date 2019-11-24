using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class PlayerHistoryManager
    {
        private readonly string  userName;
        private string historyBoard;

        //Constructor for when updating User's History
        public PlayerHistoryManager()
        {
            //No parameters needed.
        }

        //Used for displaying UserHistory
        public PlayerHistoryManager(string userName)
        {
            this.userName = userName;
            this.historyBoard = String.Format(
                "===========================================\n" +
                "||\t\t{0}'s HISTORY (TOP 10 SCORES) : \t\t||\n" +
                "||\t\t\t\t\t\t\t||\n", userName.ToUpper());
        }

        //Creates String Summary of User's History
        public string ProduceUserHistory(List<int> scores)
        {
            bool bestScorePrinted = false;
            foreach(int score in scores)
            {
                if(!bestScorePrinted)
                {
                    this.historyBoard += String.Format(
                        "||\t\t\tHIGH SCORE : {0}\t\t\t||\n", score.ToString());
                    bestScorePrinted = true;
                }
                else
                {
                    this.historyBoard += String.Format(
                        "||\t\t\t\t{0}\t\t\t||\n", score.ToString());
                }

            }
            this.historyBoard +=
                "||\t\t\t\t\t\t\t||\n" +
                "===========================================\n";
            return this.historyBoard;
        }

        //Looks through entire list of scores, and if the newScore is greater than some value within scoreHistory, replace it.
        public List<int> UserHistoryUpdate(int newScore, List<int> scores)
        {
            List<int> tempScores = new List<int>();

            //Allows only 1 rewrite
            bool updated = false;
            bool openSlotRoute = false;
            foreach (int score in scores)
            {
                if (score == 0 && !updated)
                {
                    tempScores.Add(newScore);
                    updated = true;
                    openSlotRoute = true;
                }
                else
                {
                    tempScores.Add(score);
                }               
            }

            if (!openSlotRoute)
            {
                tempScores.Clear();
                foreach (int score in scores)
                {
                    if (newScore > score && !updated)
                    {
                        tempScores.Add(newScore);
                        updated = true;
                    }
                    else
                    {
                        tempScores.Add(score);
                    }
                }
            }

            tempScores.Sort();
            tempScores.Reverse();
            return tempScores;
        }
    }
}
