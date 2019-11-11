using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Sequence
    {
        private List<int> systemSequence = new List<int>();
        private List<int> userSequence = new List<int>();

        //generates sequence then sets block order with that sequence.
        public List<int> GenerateSequence(int difficulty, List<Block> blockOrder)
        {
            //Block number order.
            for (int i = 0; i < difficulty; i++)
            {
                Random rand = new Random();
                systemSequence.Add(rand.Next(9));
            }
            return systemSequence;
        }
    }
}
