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
                Colors.Black,
                Colors.White,
                Colors.DimGray,
                "OpenSansRegular"),

            new Theme(
                "Neon",
                Color.FromArgb("#081120"),
                Colors.Lime,
                Color.FromArgb("#152642"),
                "OpenSansRegular")
        ];
    }
}
