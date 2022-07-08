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
            this.SuspendLayout();
            // 
            // Boardpanel
            // 
            this.Boardpanel.BackColor = System.Drawing.Color.Gray;
            this.Boardpanel.Location = new System.Drawing.Point(8, 8);
            this.Boardpanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Boardpanel.Name = "Boardpanel";
            this.Boardpanel.Size = new System.Drawing.Size(484, 484);
            this.Boardpanel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 529);
            this.Controls.Add(this.Boardpanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Boardpanel;
    }
}
