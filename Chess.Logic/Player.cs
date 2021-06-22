using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Logic
{
    public class Player
    {
        public Side PlayerSide { get; }

        public int NumberOfFigures { get; private set; }

        public Player(Side side)
        {
            PlayerSide = side;

            NumberOfFigures = 16;
        }

        public void MinusFigure()
        {
            NumberOfFigures--;
        }

        public bool CheckLoss()
        {
            return NumberOfFigures == 0;
        }
    }
}
