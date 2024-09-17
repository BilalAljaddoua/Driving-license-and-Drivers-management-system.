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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Rlease_Detained_License
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicense(int licenseID)
        {
            InitializeComponent();
            this._LicenseID = licenseID;
            ctrIDriverLicenseWithFilter1.FilterEnabled = false;
        }

        private  int _LicenseID = -1;
        private clsLicense _License;
        private void ctrIDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            llShowLicenseHistory.Enabled = (_LicenseID != -1);
            if(_LicenseID==-1)
            {

                return;
            }
            _License = clsLicense.FindLicensesByLicenseID(_LicenseID);

            if(!_License.IsActive)
            {
                MessageBox.Show("This License it is not Active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_License.IsDetained)
            {
                MessageBox.Show("This License isn't Detained","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             
                 lblDetainID.Text= _License.DetainInfo.DetainID.ToString();
                lblLicenseID.Text= _License.DetainInfo.LicenseID.ToString();
                lblFineFees.Text= _License.DetainInfo.FineFees.ToString();
                lblTotalFees.Text=((Convert.ToSingle(lblFineFees.Text))+(Convert.ToSingle(lblApplicationFees.Text))).ToString();
                lblApplicationID.Text = ""; 
       



            btnRelease.Enabled = true;

        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
           lblCreatedByUser.Text=clsGlobal.CurrentUser.UserName;
            lblLicenseID.Text=_LicenseID.ToString();

        
        
        
        
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm =new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm=new frmShowPersonLicenseHistory(clsLicense.FindLicensesByLicenseID(_LicenseID).PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
                if ( (MessageBox.Show("Do you eant to  release this license with ID: "+_LicenseID,"Conferm",MessageBoxButtons.OKCancel,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1)==DialogResult.Cancel))
                 return;
            int ApplicationID = -1;
               
            bool IsRelease = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.ReleseLicense(Convert.ToSingle(lblTotalFees.Text), clsGlobal.CurrentUser.UserID,ref ApplicationID);
            if(IsRelease)
            { 
                llShowLicenseInfo.Enabled = true;
                btnRelease.Enabled = false;
                ctrIDriverLicenseWithFilter1.FilterEnabled = false;
                lblApplicationID.Text= ApplicationID.ToString();
                MessageBox.Show("License released successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("License released Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
