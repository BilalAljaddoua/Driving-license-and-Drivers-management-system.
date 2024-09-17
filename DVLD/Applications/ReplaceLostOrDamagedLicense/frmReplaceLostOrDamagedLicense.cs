using DVLD.Classes;
using DVLD.Licenses.International_License;
using DVLD.Licenses.Local_Licenses;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Buisness.clsApplication;
using static DVLD_Buisness.clsLicense;

namespace DVLD.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicense : Form
    {
        public frmReplaceLostOrDamagedLicense()
        {
            InitializeComponent();
        }    
         private  enApplicationType _GetApplicationType()
        {
            if (rbDamage.Checked)
            {
                return clsApplication.enApplicationType.ReplacementforDamaged;
            }
            else
            {
                return clsApplication.enApplicationType.ReplacementforLost;

            }
        }
        private enIssueReason issueReason()
        {
            if (rbDamage.Checked)
            {
                return enIssueReason.ReplacementforDamaged;
            }
            else
            {
                return enIssueReason.ReplacementforLost;
            }
        }

        private int _LicenseID = -1;

        private void rbDamage_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replace Damaged License";
            lblApplicationFees.Text=clsApplicationType.FindApplicationType(clsApplication.enApplicationType.ReplacementforDamaged).Fees.ToString();
        }
         private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replace Lost License";
            lblApplicationFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.ReplacementforLost).Fees.ToString();
 


        }
        private void frmReplaceLostOrDamagedLicense_Activated(object sender, EventArgs e)
        {
            rbDamage.Checked = true;
            ctrIDriverLicenseWithFilter1.FilterFocus();
        }
        private void frmReplaceLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text=clsGlobal.CurrentUser.UserName;

        }
        private void ctrIDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int OldLicenseID = obj;
            lblOldLicenseID.Text = OldLicenseID.ToString();
            llShowHistory.Enabled = (OldLicenseID != -1);
            _LicenseID = OldLicenseID;

            if (OldLicenseID == -1)
            {        
                btnRrplaceLicense.Enabled = false;
                 return;
            }
            clsLicense license = clsLicense.FindLicensesByLicenseID(OldLicenseID);
            if(!license.IsActive)
            {
                MessageBox.Show("The licese the selected is not active , please chose another one. ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            btnRrplaceLicense.Enabled = true;




        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         private void btnReplaceLicense_Click(object sender, EventArgs e)
        {
          clsLicense ReplaceLicense=  ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.ReplaceForDamagedOrLost(issueReason(),clsGlobal.CurrentUser.UserID);
            if (ReplaceLicense == null)
            {
                MessageBox.Show("Replacement prosses faild","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _LicenseID = ReplaceLicense.LicenseID;
            MessageBox.Show("Replacement prosses Successfully , the new License ID : "+ReplaceLicense.LicenseID, "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblNewlicenseID.Text = ReplaceLicense.LicenseID.ToString();
            lblReNewLicenseApplication.Text = ReplaceLicense.ApplicationID.ToString();
            llShowInfo.Enabled = true;
            ctrIDriverLicenseWithFilter1.FilterEnabled = false;
            gbChoises.Enabled = false;
            btnRrplaceLicense.Enabled = false;
        }
        private void llShowHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsLicense.FindLicensesByLicenseID(_LicenseID).PersonInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
        private void llShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }
    }
}
