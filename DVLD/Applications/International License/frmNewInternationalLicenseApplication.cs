using DVLD.Classes;
using DVLD.Licenses.International_License;
using DVLD.Licenses.International_Licenses;
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

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }
        clsLicense _LocalLicense;
        clsInternationalLicense _InternationalLicense;
        int _InternationalID;
        private void frmInternationalLicenseApplication_Load(object sender, EventArgs e)
        {

            _SetDefaultValues();  
        }

        private void ctrIDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int LocalLicenseID = obj;

            lblLocalLiceneID.Text = LocalLicenseID.ToString();
            _InternationalLicense=clsInternationalLicense.FindByLocalLicenseID(LocalLicenseID);
            llHistroy.Enabled = (LocalLicenseID != -1);


            if(LocalLicenseID==-1)
            {
                // here the person selected doesn't has international license
                lblLocalLiceneID.Text = "[????]";
                return;
            }

            //here we check if the person has ordinary license 
            //this local license is required
            if(ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID!=3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
           
            int ActiveInternaionalLicenseID=clsInternationalLicense.FindByLocalLicenseID(LocalLicenseID).InternationalLicenseID;
            if(ActiveInternaionalLicenseID!=-1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llInfo.Enabled = true;
                _InternationalID = ActiveInternaionalLicenseID;

                btnIssue.Enabled = false;
                return; 
            }

            btnIssue.Enabled = true;
             
        }

        private void _SetDefaultValues()
        { 

            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblLocalLiceneID.Text ="[????]";
           // lblExDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
         //   btnSave.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

             clsInternationalLicense InternationalLicense= new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.FindApplicationType( clsApplication.enApplicationType.NewInternationalLicense).Fees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrIDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

 

 

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Save Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 

            }
            else
            {

                MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnIssue.Enabled = false;
                llInfo.Enabled = true;
                ctrIDriverLicenseWithFilter1.FilterEnabled = false;

                _InternationalLicense = InternationalLicense;
                lblInterApplication.Text = InternationalLicense.ApplicationID.ToString();
                _InternationalID = InternationalLicense.InternationalLicenseID;
            }

        }

        private void llHistroy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
 
            frmShowPersonLicenseHistory frm =new frmShowPersonLicenseHistory(_InternationalLicense.PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void llInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseinfo frm = new frmShowInternationalLicenseinfo(_InternationalID);
            frm.ShowDialog();
        }

        private void ctrIDriverLicenseWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrIDriverLicenseWithFilter1.FilterFocus();
        }
    }
}
