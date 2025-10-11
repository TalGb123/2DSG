namespace _2DSG
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.startGameBTN = new System.Windows.Forms.Button();
            this.exitGameBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "2D Shooting Game";
            // 
            // startGameBTN
            // 
            this.startGameBTN.Location = new System.Drawing.Point(330, 189);
            this.startGameBTN.Name = "startGameBTN";
            this.startGameBTN.Size = new System.Drawing.Size(128, 53);
            this.startGameBTN.TabIndex = 1;
            this.startGameBTN.Text = "Start Game";
            this.startGameBTN.UseVisualStyleBackColor = true;
            this.startGameBTN.Click += new System.EventHandler(this.startGameBTN_Click);
            // 
            // exitGameBTN
            // 
            this.exitGameBTN.Location = new System.Drawing.Point(330, 265);
            this.exitGameBTN.Name = "exitGameBTN";
            this.exitGameBTN.Size = new System.Drawing.Size(128, 53);
            this.exitGameBTN.TabIndex = 2;
            this.exitGameBTN.Text = "Exit Game";
            this.exitGameBTN.UseVisualStyleBackColor = true;
            this.exitGameBTN.Click += new System.EventHandler(this.exitGameBTN_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.exitGameBTN);
            this.Controls.Add(this.startGameBTN);
            this.Controls.Add(this.label1);
            this.Name = "MenuForm";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startGameBTN;
        private System.Windows.Forms.Button exitGameBTN;
    }
}