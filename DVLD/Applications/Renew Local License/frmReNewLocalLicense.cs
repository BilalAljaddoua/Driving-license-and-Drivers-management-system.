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

namespace DVLD.Applications.Renew_Local_License
{
    public partial class frmReNewLocalLicense : Form
    {
        public frmReNewLocalLicense()
        {
            InitializeComponent();
        }
        private int _NewLicenseID = -1;
        clsLicense License;

        private void btnIssue_Click(object sender, EventArgs e)
        {
                
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.RenewLicense(txtNots.Text, clsGlobal.CurrentUser.UserID);

            if(NewLicense==null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblReNewLicenseApplication.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblNewlicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrIDriverLicenseWithFilter1.FilterEnabled = false;
            llShowInfo.Enabled = true;


        }

        private void ctrIDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();

            llShowHistory.Enabled = (SelectedLicenseID != -1);

                if(SelectedLicenseID== -1) 
                {
                   return;
                }
            _NewLicenseID = SelectedLicenseID;

            int DefaultValidityLength = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNots.Text = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.Notes;

            //check the license is not Expired.
            if (!ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            //check the license is not Expired.
            if (!ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }



            btnRenewLicense.Enabled = true;
        }


        private void frmReNewLocalLicense_Activated(object sender, EventArgs e)
        {
            ctrIDriverLicenseWithFilter1.FilterFocus();
        }
        private void llShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(License.LicenseID);
            frm.ShowDialog();
        }
        private void llShowHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsLicense.FindLicensesByLicenseID(_NewLicenseID).PersonInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();

        }

        private void frmReNewLocalLicense_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblAppDate.Text;

            lblExDate.Text = "???";
            lblApplicationFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
