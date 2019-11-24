using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class PlayerHistoryManager
    {
        private int[] tempScores;
        private readonly string  userName;
        private string historyBoard;

        public PlayerHistoryManager(string userName)
        {
            this.userName = userName;
            this.historyBoard = String.Format(
                "||\t\t{0}'s HISTORY (TOP 10 SCORES) : \t\t||\n" +
                "||\t\t\t\t\t\t\t||\n", userName.ToUpper());
        }

        public string ProduceUserHistory(List<int> scores)
        {
            foreach(int score in scores)
            {
                this.historyBoard += String.Format(
                    "||\t\t\t\t{0}\t\t\t||\n",score.ToString());
            }
            this.historyBoard +=
                "||\t\t\t\t\t\t\t||\n" +
                "===========================================\n";
            return this.historyBoard;
        }

        //Looks through entire list of scores, and if the newScore is greater than some value within scoreHistory, replace it.
        public int[] UserHistorySort(int newScore, List<int> scores)
        {
            int i = 0;
            tempScores = new int[10];
            bool updated = false;
            foreach (int score in scores)
            {
                if(newScore > score && !updated)
                {
                    tempScores[i] = newScore;
                    updated = true;
                }
                else
                {
                    tempScores[i] = score;
                }
                i++;
            }
            Array.Sort(tempScores);
            Array.Reverse(tempScores);
            return tempScores;
        }
    }
}
