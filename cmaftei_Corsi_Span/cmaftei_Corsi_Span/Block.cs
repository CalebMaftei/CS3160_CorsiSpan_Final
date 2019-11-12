using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Block
    {
        private Color blockColor;
        private int xLocation;
        private int yLocation;
        private int blockID;
        private Random rand = new Random();

        //3-Element Constructor
        public Block(Color BlockColor, int BlockID, int randomValue)
        {
            this.blockColor = BlockColor;
            this.blockID = BlockID;
            shuffleBlockLocation(randomValue);
        }

        //Getters
        public Color GetBlockColor()
        {
            return blockColor;
        }

        public int GetXLocation()
        {
            return xLocation;
        }

        public int GetYLocation()
        {
            return yLocation;
        }

        public int GetBlockID()
        {
            return blockID;
        }

        //Setters
        public void SetBlockColor(Color BlockColor)
        {
            this.blockColor = BlockColor;
        }

        public void SetXLocation(int XLocation)
        {
            this.xLocation = XLocation;
        }

        public void SetYLocation(int YLocation)
        {
            this.yLocation = YLocation;
        }

        public void SetBlockID(int BlockID)
        {
            this.blockID = BlockID;
        }

        //Shuffle Location
        private void shuffleBlockLocation(int i)
        {
            this.xLocation = 65 * (this.blockID-1) + 10;
            this.yLocation = i;
        }
    }
}
