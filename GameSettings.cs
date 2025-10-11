using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DSG
{
    public class GameSettings
    {
        public Color PlayerColor1 { get; set; } = Color.Green;
        public Color PlayerColor2 { get; set; } = Color.DarkBlue;
        public string PlayerName1 { get; set; } = "Player 1";
        public string PlayerName2 { get; set; } = "Player 2";
        public Map SelectedMap { get; set; } = null;

        public GameSettings(Color p1Color, Color p2Color, string p1Name, string p2Name, Map selectedMap)
        {
            Color PlayerColor1 = p1Color;
            Color PlayerColor2 = p2Color;
            string PlayerName1 = p1Name;
            string PlayerName2 = p2Name;
            Map SelectedMap = selectedMap;
        }
    }
}
