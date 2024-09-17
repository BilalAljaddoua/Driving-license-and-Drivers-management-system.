namespace DVLD
{
    partial class frmTraying
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
            this.ctrIDriverLicenseWithFilter1 = new DVLD.Applications.International_License.Controls.ctrIDriverLicenseWithFilter();
            this.SuspendLayout();
            // 
            // ctrIDriverLicenseWithFilter1
            // 
            this.ctrIDriverLicenseWithFilter1.Location = new System.Drawing.Point(12, 12);
            this.ctrIDriverLicenseWithFilter1.Name = "ctrIDriverLicenseWithFilter1";
            this.ctrIDriverLicenseWithFilter1.Size = new System.Drawing.Size(960, 497);
            this.ctrIDriverLicenseWithFilter1.TabIndex = 0;
            // 
            // frmTraying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 809);
            this.Controls.Add(this.ctrIDriverLicenseWithFilter1);
            this.Name = "frmTraying";
            this.Text = "Traying";
            this.Load += new System.EventHandler(this.Traying_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Applications.International_License.Controls.ctrIDriverLicenseWithFilter ctrIDriverLicenseWithFilter1;
    }
}