using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace _2DSG
{
    public partial class GameForm : Form
    {
        List<Player> players = new List<Player>();
        List<Bullet> bullets = new List<Bullet>();
        List<Platform> platforms = new List<Platform>();
        Timer gameTimer = new Timer();

        List<bool> pLeft = new List<bool>();
        List<bool> pRight = new List<bool>();
        List<bool> pJump = new List<bool>();
        List<bool> pFire = new List<bool>();
        GameSettings gameSettings;

        public GameForm(GameSettings gameSettings)
        {
            InitializeComponent();
            this.Name = "2D Shooter Game";
            this.ClientSize = new Size(1280, 720);

            this.gameSettings = gameSettings;

            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            string[] playerColors = { "Red", "Blue", "Green", "Yellow" }; 
            int i = 0;
            foreach (var start in this.gameSettings.SelectedMap.PlayerStarts)
            {
                int px = (int)(start.X * w);
                int py = (int)(start.Y * h);
                var player = new Player(px, py, 100, playerColors[i], "player");
                // lazy solution for player colors as there are only 2 atm, in final project i will make it more dynamic
                if (i == 0)
                {
                    player.Sprite.BackColor = this.gameSettings.PlayerColor1;
                    player.NameTag.Text = this.gameSettings.PlayerName1;
                }
                else
                {
                    player.Sprite.BackColor = this.gameSettings.PlayerColor2;
                    player.NameTag.Text = this.gameSettings.PlayerName2;
                }
                player.NameTag.AutoSize = true;
                players.Add(player);
                this.Controls.Add(player.Sprite);
                this.Controls.Add(player.Health.Sprite);
                this.Controls.Add(player.NameTag);
                
                i++;
                pLeft.Add(false);
                pRight.Add(false);
                pJump.Add(false);
                pFire.Add(false);
            }



            platforms = gameSettings.SelectedMap.CreatePlatforms(this.ClientSize.Width, this.ClientSize.Height);
            foreach (var platform in platforms)
            {
                this.Controls.Add(platform.Sprite);
            }

            // Put Image for background (very laggy in winforms within the limits i got)
            //if (!string.IsNullOrEmpty(selectedMap.BackgroundImage))
            //{
            //    this.BackgroundImage = Image.FromFile(selectedMap.BackgroundImage);
            //    this.BackgroundImageLayout = ImageLayout.Stretch;
            //}

            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (pLeft[i]) players[i].MoveLeft();
                if (pRight[i]) players[i].MoveRight();
                if (pJump[i] && !players[i].Jumping)
                {
                    players[i].Jumping = true;
                    players[i].JumpSpeed = -players[i].Force * 2;
                }
            }

            foreach (var player in players)
                player.ApplyGravity();

            foreach (var b in bullets)
                b.Move();

            for (int i = 0; i < players.Count; i++)
            {
                if (pFire[i] && players[i].CanFire())
                {
                    int direction = players[i].FacingRight ? 1 : -1;
                    var bullet = new Bullet(
                        players[i].FacingRight ? players[i].Sprite.Left + players[i].Sprite.Width : players[i].Sprite.Left - 10,
                        players[i].Sprite.Top + players[i].Sprite.Height / 2,
                        direction,
                        players[i]
                    );
                    bullets.Add(bullet);
                    this.Controls.Add(bullet.Sprite);
                    players[i].RegisterFire(); 
                }
            }

            foreach (var player in players)
                CheckCollisions(player);

            CheckBulletPlayerCollisions();

            int healthBarWidth = 60;
            int healthBarHeight = 8;

            foreach (var player in players)
            {
                player.Health.Sprite.Width = (int)(healthBarWidth * (double)player.Health.Current / player.Health.Max);
                player.Health.Sprite.Height = healthBarHeight;
                player.Health.Sprite.Left = player.Sprite.Left + (player.Sprite.Width - healthBarWidth) / 2;
                player.Health.Sprite.Top = player.Sprite.Top - healthBarHeight - 5;
                player.Health.Sprite.BackColor = player.Health.Current > 0 ? Color.Green : Color.Red;

                player.NameTag.Left = player.Sprite.Left + (player.Sprite.Width - player.NameTag.Width) / 2;
                player.NameTag.Top = player.Health.Sprite.Top - player.NameTag.Height - 5;
            }

            GameOver();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) pLeft[0] = true;
            if (e.KeyCode == Keys.D) pRight[0] = true;
            if (e.KeyCode == Keys.W) pJump[0] = true;
            if (e.KeyCode == Keys.X) pFire[0] = true;
            if (e.KeyCode == Keys.Escape) Application.Exit();
            if (e.KeyCode == Keys.P)
            {
                PauseForm pauseForm = new PauseForm(this.gameSettings, this, "Game Paused");
                pauseForm.ShowDialog();
            }

            if (players.Count > 1)
            {
                if (e.KeyCode == Keys.Left) pLeft[1] = true;
                if (e.KeyCode == Keys.Right) pRight[1] = true;
                if (e.KeyCode == Keys.Up) pJump[1] = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) pLeft[0] = false;
            if (e.KeyCode == Keys.D) pRight[0] = false;
            if (e.KeyCode == Keys.W) pJump[0] = false;
            if (e.KeyCode == Keys.X) pFire[0] = false;

            if (players.Count > 1)
            {
                if (e.KeyCode == Keys.Left) pLeft[1] = false;
                if (e.KeyCode == Keys.Right) pRight[1] = false;
                if (e.KeyCode == Keys.Up) pJump[1] = false;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && players.Count > 1)
            {
                pFire[1] = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && players.Count > 1)
            {
                pFire[1] = false;
            }
        }

        private void CheckCollisions(Player player)
        {
            bool landed = false;

            foreach (var platform in platforms)
            {
                Rectangle playerRect = player.Sprite.Bounds;
                Rectangle platRect = platform.Sprite.Bounds;

                bool isFalling = player.JumpSpeed >= 0;

                int nextBottom = playerRect.Bottom + player.JumpSpeed;

                bool willLandOnTop =
                    isFalling &&
                    playerRect.Bottom <= platRect.Top && 
                    nextBottom >= platRect.Top &&        
                    playerRect.Right > platRect.Left + 5 && 
                    playerRect.Left < platRect.Right - 5; 

                if (willLandOnTop)
                {
                    player.Jumping = false;
                    player.JumpSpeed = 0;
                    player.Sprite.Top = platRect.Top - player.Sprite.Height;
                    landed = true;
                    break;
                }
            }

            if (!landed)
            {
                player.Jumping = true;
            }
        }

        private void CheckBulletPlayerCollisions()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var b = bullets[i];

                for (int p = 0; p < players.Count; p++)
                {
                    if (b.Owner != players[p] && b.Sprite.Bounds.IntersectsWith(players[p].Sprite.Bounds))
                    {
                        players[p].Health.TakeDamage();
                        this.Controls.Remove(b.Sprite);
                        bullets.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void GameOver()
        {
            int aliveCount = 0;
            Player lastAlive = null;
            foreach (var player in players)
            {
                if (!player.Health.IsDead)
                {
                    aliveCount++;
                    lastAlive = player;
                }
                if (player.Sprite.Top > this.ClientSize.Height)
                {
                    gameTimer.Stop();
                    string message = $"Game Over!" + $"\n{player.NameTag.Text} fell from the map!";
                    PauseForm gameOverForm = new PauseForm(this.gameSettings, this, message);
                    gameOverForm.ShowDialog();
                    return;
                }
            }
            if (aliveCount <= 1)
            {
                gameTimer.Stop();
                string message = aliveCount == 1 ? "We have a winner!" : "It's a draw!";
                if (lastAlive != null)
                {
                    message += $"\nWinner is the {lastAlive.Sprite.Name}!";
                }
                PauseForm gameOverForm = new PauseForm(this.gameSettings, this, message);
                gameOverForm.ShowDialog();
                return;
            }
        }
    }
}