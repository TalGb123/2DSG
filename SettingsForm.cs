using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DSG
{
    // i tried making colors in one function but got tired, maybe later

    public partial class SettingsForm : Form
    {
        GameSettings gameSettings;
        private const string MapFilePath = "Assets/Data/Maps.txt";
        private List<Map> allMaps;
        private Map selectedMap;

        public SettingsForm()
        {
            InitializeComponent();
            this.Text = "2D Shooter Game - Settings";

            allMaps = Map.LoadAll(MapFilePath);
            gameSettings = new GameSettings(Color.Green, Color.DarkBlue, "Player 1", "Player 2", allMaps[0]);

            double startXPercent = 0.3;
            double startYPercent = 0.2;
            double endXPercent = 0.8;
            double endYPercent = 0.6;

            int availableWidth = this.ClientSize.Width;
            int availableHeight = this.ClientSize.Height;

            int startX = (int)(availableWidth * startXPercent);
            int startY = (int)(availableHeight * startYPercent);
            int endX = (int)(availableWidth * endXPercent);
            int endY = (int)(availableHeight * endYPercent);

            int mapWidth = (int)((endX - startX) / 4.0);  
            int mapHeight = (int)((endY - startY) / 4.0); 
            int spacingX = 10;
            int spacingY = 10;

            int currentX = startX;
            int currentY = startY;

            foreach (var map in allMaps)
            {
                PictureBox mapBox = CreateMapPreview(map, currentX, currentY, mapWidth, mapHeight);
                this.Controls.Add(mapBox);

                currentX += mapWidth + spacingX;

                if (currentX + mapWidth > endX)
                {
                    currentX = startX;
                    currentY += mapHeight + spacingY;
                }
            }

        }

        private void skinGreen1BTN_Click(object sender, EventArgs e)
        {
            playerSkin1PB.BackColor = Color.Green;
            gameSettings.PlayerColor1 = Color.Green;
        }

        private void skinTeal1BTN_Click(object sender, EventArgs e)
        {
            playerSkin1PB.BackColor = Color.Teal;
            gameSettings.PlayerColor1 = Color.Teal;
        }

        private void skinDarkViolet1BTN_Click(object sender, EventArgs e)
        {
            playerSkin1PB.BackColor = Color.DarkViolet;
            gameSettings.PlayerColor1 = Color.DarkViolet;
        }

        private void skinCrimson1BTN_Click(object sender, EventArgs e)
        {
            playerSkin1PB.BackColor = Color.Crimson;
            gameSettings.PlayerColor1 = Color.Crimson;
        }

        private void skinDarkBlue1BTN_Click(object sender, EventArgs e)
        {
            playerSkin1PB.BackColor = Color.DarkBlue;
            gameSettings.PlayerColor1 = Color.DarkBlue;
        }

        private void skinGreen2BTN_Click(object sender, EventArgs e)
        {
            playerSkin2PB.BackColor = Color.Green;
            gameSettings.PlayerColor2 = Color.Green;
        }

        private void skinTeal2BTN_Click(object sender, EventArgs e)
        {
            playerSkin2PB.BackColor = Color.Teal;
            gameSettings.PlayerColor2 = Color.Teal;
        }

        private void skinDarkViolet2BTN_Click(object sender, EventArgs e)
        {
            playerSkin2PB.BackColor = Color.DarkViolet;
            gameSettings.PlayerColor2 = Color.DarkViolet;
        }

        private void skinCrimson2BTN_Click(object sender, EventArgs e)
        {
            playerSkin2PB.BackColor = Color.Crimson;
            gameSettings.PlayerColor2 = Color.Crimson;
        }

        private void skinDarkBlue2BTN_Click(object sender, EventArgs e)
        {
            playerSkin2PB.BackColor = Color.DarkBlue;
            gameSettings.PlayerColor2 = Color.DarkBlue;
        }

        private void startGameBTN_Click(object sender, EventArgs e)
        {
            if (name1TB.Text.Trim() == "")
            {
                gameSettings.PlayerName1 = "Player 1";
            }
            else
            {
                gameSettings.PlayerName1 = name1TB.Text;
            }
            if (name2TB.Text == "")
            {
                gameSettings.PlayerName2 = "Player 2";
            }
            else
            {
                gameSettings.PlayerName2 = name2TB.Text;
            }

            GameForm gameForm = new GameForm(gameSettings);
            gameForm.ShowDialog();
        }

        private PictureBox CreateMapPreview(Map map, int x, int y, int width, int height)
        {
            PictureBox previewBox = new PictureBox();
            previewBox.Width = width;
            previewBox.Height = height;
            previewBox.Left = x;
            previewBox.Top = y;
            previewBox.BorderStyle = BorderStyle.FixedSingle;
            previewBox.Tag = map;
            previewBox.SizeMode = PictureBoxSizeMode.StretchImage;
            previewBox.BackColor = Color.White;

            foreach (var p in map.Platforms)
            {
                PictureBox plat = new PictureBox();
                plat.BackColor = Color.Gray;
                plat.Left = (int)(p.x * width);
                plat.Top = (int)(p.y * height);
                plat.Width = (int)(p.w * width);
                plat.Height = (int)(p.h * height);
                previewBox.Controls.Add(plat);
            }

            foreach (var s in map.PlayerStarts)
            {
                PictureBox spawn = new PictureBox();
                spawn.BackColor = Color.Red;
                spawn.Left = (int)(s.X * width) - 1;
                spawn.Top = (int)(s.Y * height) - 1;
                spawn.Width = 3;
                spawn.Height = 3;
                previewBox.Controls.Add(spawn);
            }

            previewBox.Click += (s, e) =>
            {
                gameSettings.SelectedMap = map;
                MessageBox.Show($"Selected map: {map.Name}");
            };

            return previewBox;
        }

    }
}
