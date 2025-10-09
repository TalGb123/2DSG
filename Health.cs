using _2DSG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Health
{
    public int Max { get; private set; }
    public int Current { get; private set; }
    public int DamagePerHit { get; set; } = 10;
    public PictureBox Sprite { get; set; }

    public Health(int max)
    {
        Max = max;
        Current = max;
        Sprite = new PictureBox(); 
        Sprite.Size = new Size(60, 8);
        Sprite.BackColor = Color.Green;
        Sprite.Left = 10;
        Sprite.Top = 10;
    }

    public void TakeDamage()
    {
        Current = Math.Max(0, Current - DamagePerHit);
    }

    public bool IsDead => Current <= 0;
}