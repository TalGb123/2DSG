using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace _2DSG
{
    public partial class PauseForm : Form
    {
        private GameSettings gameSettings;
        private Form parentGameForm;
        private Label reasonLabel;
        public PauseForm(GameSettings settings, Form parent, string reason)
        {
            InitializeComponent();
            this.gameSettings = settings;
            this.parentGameForm = parent;
            this.reasonLabel = new Label();
            PauseReasonLBL.Text = reason;
            if (reason == "Game Paused")
            {
                ResumeBTN.Visible = true;
            }
            PauseReasonLBL.MaximumSize = new Size(this.ClientSize.Width - 40, 0); 
            PauseReasonLBL.AutoSize = true;
            PauseReasonLBL.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void ResumeBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainMenuBTN_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            this.Hide();
            menuForm.ShowDialog();
            this.Close();
        }

        private void SettingsBTN_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            this.Hide();
            settingsForm.ShowDialog();
            this.Close();
        }

        private void RestartBTN_Click(object sender, EventArgs e)
        {
            parentGameForm.Hide();
            parentGameForm.Close();
            GameForm gameForm = new GameForm(gameSettings);
            this.Hide();
            gameForm.ShowDialog();
            this.Close();
        }
    }
}
