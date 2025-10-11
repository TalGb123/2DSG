namespace _2DSG
{
    partial class PauseForm
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
            this.PauseReasonLBL = new System.Windows.Forms.Label();
            this.SettingsBTN = new System.Windows.Forms.Button();
            this.ResumeBTN = new System.Windows.Forms.Button();
            this.RestartBTN = new System.Windows.Forms.Button();
            this.MainMenuBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PauseReasonLBL
            // 
            this.PauseReasonLBL.AutoSize = true;
            this.PauseReasonLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseReasonLBL.Location = new System.Drawing.Point(357, 61);
            this.PauseReasonLBL.Name = "PauseReasonLBL";
            this.PauseReasonLBL.Size = new System.Drawing.Size(97, 33);
            this.PauseReasonLBL.TabIndex = 0;
            this.PauseReasonLBL.Text = "Pause";
            this.PauseReasonLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsBTN
            // 
            this.SettingsBTN.Location = new System.Drawing.Point(493, 241);
            this.SettingsBTN.Name = "SettingsBTN";
            this.SettingsBTN.Size = new System.Drawing.Size(76, 68);
            this.SettingsBTN.TabIndex = 1;
            this.SettingsBTN.Text = "Settings";
            this.SettingsBTN.UseVisualStyleBackColor = true;
            this.SettingsBTN.Click += new System.EventHandler(this.SettingsBTN_Click);
            // 
            // ResumeBTN
            // 
            this.ResumeBTN.Location = new System.Drawing.Point(378, 326);
            this.ResumeBTN.Name = "ResumeBTN";
            this.ResumeBTN.Size = new System.Drawing.Size(76, 68);
            this.ResumeBTN.TabIndex = 1;
            this.ResumeBTN.Text = "Resume";
            this.ResumeBTN.UseVisualStyleBackColor = true;
            this.ResumeBTN.Visible = false;
            this.ResumeBTN.Click += new System.EventHandler(this.ResumeBTN_Click);
            // 
            // RestartBTN
            // 
            this.RestartBTN.Location = new System.Drawing.Point(261, 241);
            this.RestartBTN.Name = "RestartBTN";
            this.RestartBTN.Size = new System.Drawing.Size(76, 68);
            this.RestartBTN.TabIndex = 1;
            this.RestartBTN.Text = "Restart";
            this.RestartBTN.UseVisualStyleBackColor = true;
            this.RestartBTN.Click += new System.EventHandler(this.RestartBTN_Click);
            // 
            // MainMenuBTN
            // 
            this.MainMenuBTN.Location = new System.Drawing.Point(378, 241);
            this.MainMenuBTN.Name = "MainMenuBTN";
            this.MainMenuBTN.Size = new System.Drawing.Size(76, 68);
            this.MainMenuBTN.TabIndex = 1;
            this.MainMenuBTN.Text = "Back To\r\nMain Menu";
            this.MainMenuBTN.UseVisualStyleBackColor = true;
            this.MainMenuBTN.Click += new System.EventHandler(this.MainMenuBTN_Click);
            // 
            // PauseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResumeBTN);
            this.Controls.Add(this.RestartBTN);
            this.Controls.Add(this.MainMenuBTN);
            this.Controls.Add(this.SettingsBTN);
            this.Controls.Add(this.PauseReasonLBL);
            this.Name = "PauseForm";
            this.Text = "PauseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PauseReasonLBL;
        private System.Windows.Forms.Button SettingsBTN;
        private System.Windows.Forms.Button ResumeBTN;
        private System.Windows.Forms.Button RestartBTN;
        private System.Windows.Forms.Button MainMenuBTN;
    }
}