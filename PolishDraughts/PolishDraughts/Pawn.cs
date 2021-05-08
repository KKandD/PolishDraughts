using System;
using System.Collections.Generic;
using System.Text;

namespace PolishDraughts
{
    public class Pawn
    {
        public bool IsWhite { get; private set; }

        public (int x, int y) Coordinates { get; set; }

        public Pawn(bool isWhite, int x, int y)
        {
            IsWhite = isWhite;
            Coordinates = (x, y);
        }
    }
}
