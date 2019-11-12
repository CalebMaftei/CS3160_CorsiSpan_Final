using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Scoreboard
    {
        private Dictionary<string, int> playerScores = new Dictionary<string, int>();

        public void LoadScores(List<Player> players)
        {
            foreach(Player p in players)
            {
                if(!playerScores.ContainsKey(p.GetUserName()))
                {
                    playerScores.Add(p.GetUserName(), p.GetBestScore());
                }                
            }
        }

        public override string ToString()
        {
            string returnString = "";
            int count = 0;
            foreach(KeyValuePair<string, int> pair in playerScores.OrderByDescending(i => i.Value))
            {
                returnString += String.Format("{0}:\t\t{1}\n", pair.Key, pair.Value);
                count++;
                if (count == 10)
                    break;
            }
            return returnString;
        }
    }
}
