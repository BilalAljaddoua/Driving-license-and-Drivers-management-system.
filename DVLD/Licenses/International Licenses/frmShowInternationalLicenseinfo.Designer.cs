namespace DVLD.Licenses.International_Licenses
{
    partial class frmShowInternationalLicenseinfo
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
            this.ctrlShowInernationalLicenseinfo1 = new DVLD.Licenses.International_Licenses.Controls.ctrlShowInernationalLicenseinfo();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlShowInernationalLicenseinfo1
            // 
            this.ctrlShowInernationalLicenseinfo1.Location = new System.Drawing.Point(12, 167);
            this.ctrlShowInernationalLicenseinfo1.Name = "ctrlShowInernationalLicenseinfo1";
            this.ctrlShowInernationalLicenseinfo1.Size = new System.Drawing.Size(950, 330);
            this.ctrlShowInernationalLicenseinfo1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(198, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Show International License Info";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(831, 503);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmShowInternationalLicenseinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 556);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlShowInernationalLicenseinfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowInternationalLicenseinfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowInternationalLicenseinfo";
            this.Load += new System.EventHandler(this.frmShowInternationalLicenseinfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlShowInernationalLicenseinfo ctrlShowInernationalLicenseinfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}