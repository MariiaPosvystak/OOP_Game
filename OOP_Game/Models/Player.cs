using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Game.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Moves { get; set; }
        public TimeSpan PlayTime { get; set; }
        public Player(string name)
        {
            Name = name;
            Moves = 0;
            PlayTime = TimeSpan.Zero;
        }
    }
}
