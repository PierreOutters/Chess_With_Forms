namespace Chess_With_Forms
{
    partial class Form1
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
            this.Boardpanel = new System.Windows.Forms.Panel();
            this.InactiveTimer = new System.Windows.Forms.TextBox();
            this.ActiveTimer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Boardpanel
            // 
            this.Boardpanel.BackColor = System.Drawing.Color.Gray;
            this.Boardpanel.Location = new System.Drawing.Point(25, 53);
            this.Boardpanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Boardpanel.Name = "Boardpanel";
            this.Boardpanel.Size = new System.Drawing.Size(645, 596);
            this.Boardpanel.TabIndex = 0;
            // 
            // InactiveTimer
            // 
            this.InactiveTimer.BackColor = System.Drawing.SystemColors.Control;
            this.InactiveTimer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InactiveTimer.Cursor = System.Windows.Forms.Cursors.Default;
            this.InactiveTimer.Enabled = false;
            this.InactiveTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InactiveTimer.Location = new System.Drawing.Point(548, 19);
            this.InactiveTimer.Name = "InactiveTimer";
            this.InactiveTimer.Size = new System.Drawing.Size(122, 29);
            this.InactiveTimer.TabIndex = 1;
            this.InactiveTimer.Text = "10:00";
            this.InactiveTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ActiveTimer
            // 
            this.ActiveTimer.BackColor = System.Drawing.SystemColors.Control;
            this.ActiveTimer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ActiveTimer.Cursor = System.Windows.Forms.Cursors.Default;
            this.ActiveTimer.Enabled = false;
            this.ActiveTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveTimer.Location = new System.Drawing.Point(548, 654);
            this.ActiveTimer.Name = "ActiveTimer";
            this.ActiveTimer.Size = new System.Drawing.Size(122, 29);
            this.ActiveTimer.TabIndex = 2;
            this.ActiveTimer.Text = "10:00";
            this.ActiveTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 762);
            this.Controls.Add(this.ActiveTimer);
            this.Controls.Add(this.InactiveTimer);
            this.Controls.Add(this.Boardpanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Boardpanel;
        private System.Windows.Forms.TextBox InactiveTimer;
        private System.Windows.Forms.TextBox ActiveTimer;
    }
}
