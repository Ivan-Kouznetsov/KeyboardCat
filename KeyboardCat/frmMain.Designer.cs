﻿namespace KeyboardCat
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.rdoCatOn = new System.Windows.Forms.RadioButton();
            this.rdoCatOff = new System.Windows.Forms.RadioButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerNoKeyPress = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // rdoCatOn
            // 
            this.rdoCatOn.AutoSize = true;
            this.rdoCatOn.Location = new System.Drawing.Point(73, 11);
            this.rdoCatOn.Name = "rdoCatOn";
            this.rdoCatOn.Size = new System.Drawing.Size(39, 17);
            this.rdoCatOn.TabIndex = 0;
            this.rdoCatOn.Text = "On";
            this.rdoCatOn.UseVisualStyleBackColor = true;
            // 
            // rdoCatOff
            // 
            this.rdoCatOff.AutoSize = true;
            this.rdoCatOff.Checked = true;
            this.rdoCatOff.Location = new System.Drawing.Point(118, 11);
            this.rdoCatOff.Name = "rdoCatOff";
            this.rdoCatOff.Size = new System.Drawing.Size(39, 17);
            this.rdoCatOff.TabIndex = 2;
            this.rdoCatOff.TabStop = true;
            this.rdoCatOff.Text = "Off";
            this.rdoCatOff.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Cat Mode:";
            // 
            // timerNoKeyPress
            // 
            this.timerNoKeyPress.Tick += new System.EventHandler(this.TimerNoKeyPress_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 45);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.rdoCatOff);
            this.Controls.Add(this.rdoCatOn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Keyboard Cat";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoCatOn;
        private System.Windows.Forms.RadioButton rdoCatOff;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerNoKeyPress;
    }
}

