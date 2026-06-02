using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Game.Models
{
    public class Theme
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color TileColor { get; set; }
        public string FontFamily { get; set; }
        public Theme(
            string name,
            Color backgroundColor,
            Color textColor,
            Color tileColor,
            string fontFamily)
        {
            Name = name;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            TileColor = tileColor;
            FontFamily = fontFamily;
        }
    }
}
