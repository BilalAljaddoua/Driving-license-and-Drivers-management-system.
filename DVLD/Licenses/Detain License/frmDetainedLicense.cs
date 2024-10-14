using DVLD.Classes;
using DVLD.Licenses.International_License;
using DVLD.Licenses.Local_Licenses;
using DVLD_Buisness;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Licenses.Detain_License
{
    public partial class frmDetainedLicense : Form
    {
        public frmDetainedLicense()
        {
            InitializeComponent();
        }
        private clsLicense _License;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrIDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;
            llShowLicenseHistory.Enabled = (LicenseID != -1);
            if(LicenseID==-1)
            {
                btnDetain.Enabled = false;
                return;
            } 

            _License=clsLicense.FindLicensesByLicenseID(LicenseID);
             lblLicenseID.Text = LicenseID.ToString();   
            if (!_License.IsActive  )
            {
                MessageBox.Show("This License it is not Active","Not Active",MessageBoxButtons.OK,MessageBoxIcon.Error);
               btnDetain.Enabled = false;

                return;
            }
            if (_License.IsDetained )
            {
                MessageBox.Show("This License it's Already Detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtFineFees.Focus();
            btnDetain.Enabled = true;



        }

        private void btnDetain_Click(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(txtFineFees.Text))
            {
                MessageBox.Show("Please enter fees", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txtFineFees.Focus();
                return;
            }
            
            if (MessageBox.Show("Do you want to detained this license ? ", "Conform", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) != DialogResult.OK) 
            {
                return;
            }
            int detainedLicenseID = _License.DetainedLicense(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
            if (detainedLicenseID==-1)
            {
                MessageBox.Show("License Detained Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text=detainedLicenseID.ToString(); 
            btnDetain.Enabled=false;
            ctrIDriverLicenseWithFilter1.FilterEnabled=false;
            llShowLicenseInfo.Enabled = true;
            txtFineFees.Enabled = false;
                MessageBox.Show("License Detained Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
               // e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "this filed is required");
                //   txtFineFees.Focus();
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
             
            }
            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }

        private void frmDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrIDriverLicenseWithFilter1.FilterFocus();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void frmDetainedLicense_Load(object sender, EventArgs e)
        { 
            lblDetainDate.Text = DateTime.Now.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void txtFineFees_Validated(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "this filed is required");
                //   txtFineFees.Focus();

            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm =new  frmShowLicenseInfo(_License.LicenseID);
            frm.ShowDialog();
        }
    }
}
