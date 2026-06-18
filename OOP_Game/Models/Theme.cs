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

        public Theme(string name, Color bg, Color text, Color tile, string font)
        {
            Name = name;
            BackgroundColor = bg;
            TextColor = text;
            TileColor = tile;
            FontFamily = font;
        }
        public void Apply(Page page)
        {
            page.BackgroundColor = BackgroundColor;
            page.Resources["AppFont"] = FontFamily;
        }
    }
}
