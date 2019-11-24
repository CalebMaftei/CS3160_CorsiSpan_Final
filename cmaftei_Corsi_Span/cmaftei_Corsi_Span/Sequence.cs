using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Sequence
    {
        private List<int> systemSequence = new List<int>();
        private List<int> userSequence = new List<int>();
        private Color activeBlockColor = Color.White;
        private string gameMode;

        //getters
        public List<int> GetSystemSequence()
        {
            return this.systemSequence;
        }

        public List<int> GetUserSequence()
        {
            return this.userSequence;
        }

        public Color GetActiveBlockColor()
        {
            return this.activeBlockColor;
        }

        public string GetGameMode()
        {
            if(gameModeRandomChange())
            {
                gameMode = "reverse";
            }
            else
            {
                gameMode = "normal";
            }
            return gameMode;
        }

        //generates sequence then sets block order with that sequence.
        public void GenerateSequence(int difficulty)
        {
            Random rand = new Random();
            int n;
            //Block number order.
            for (int i = 0; i < difficulty; i++)
            {
                n = rand.Next(9);
                systemSequence.Add(n);
            }
        }

        //When user ends up adding to their sequence, this will allow mainUI to alter the list in this class.
        public void addToUserSequence(int blockID)
        {
            userSequence.Add(blockID - 1);
        }

        //Returns true if both lists are identical in sequence, returns false, otherwise.
        public bool sequenceCheck()
        {
            //If the two sequences do not have the same number of blocks => Wrong User Sequence
            if (systemSequence.Count != userSequence.Count)
            {
                return false;
            }

            //declaring two arrays
            int[] system = new int[systemSequence.Count];
            int[] user = new int[userSequence.Count];

            //Convert the set into a sequence
            system = systemSequence.ToArray();
            //Turns them into arrays for easier comparing
            user = userSequence.ToArray();

            //Iterate through system sequence. If at any point user sequence is different, wrong sequence.
            for (int i = 0; i < system.Length; i++)
            {
                if (system[i] != user[i])
                {
                    return false;
                }
            }
            return true;
        }

        //Reverse Check
        public bool ReverseSequenceCheck()
        {
            //if sequence lengths are not the same, return false
            if(systemSequence.Count != userSequence.Count)
            {
                return false;
            }
            else //sequences are the same length
            {
                //convert sequences to arrays for fine comparisons
                systemSequence.Reverse();
                int[] systemArray = systemSequence.ToArray();
                int[] userArray = userSequence.ToArray();

                //Checks if user sequence matches the system sequence.
                for(int i = 0; i < systemArray.Count(); i++)
                {
                    if(systemArray[i] != userArray[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Resets sequence for new rounds
        public void ResetSequences()
        {
            this.systemSequence.Clear();
            this.userSequence.Clear();
        }

        //Used internally to determine if the sequence must be answered in reverse or not.
        private bool gameModeRandomChange()
        {
            Random rand = new Random();
            int i;
            i = rand.Next(0, 999);
            if(i % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
