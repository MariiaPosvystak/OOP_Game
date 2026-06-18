using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Game.Models
{
    public class GameTheme
    {
        public static List<Theme> Themes =>
        [
            new Theme(
            "Light",
            Colors.White,
            Colors.Black,
            Colors.LightGray,
            "OpenSansRegular"),

        new Theme(
            "Dark",
            Colors.DarkGray,
            Colors.DimGray,
            Colors.White,
            "OpenSansRegular"),

        new Theme(
            "Neon",
            Color.FromHex("#fe59c2"),
            Color.FromHex("#bc13fe"),
            Color.FromHex("#fe019a"),
            "OpenSansRegular")
        ];
    }
}
