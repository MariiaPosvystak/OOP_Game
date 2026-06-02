using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Game.Models
{
    public class Tile
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsEmpty => Value == 0;
        public Tile(int value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
        }
    }
}
