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

namespace DVLD.Applications.International_License
{
    public partial class frmInternationalLicenseApplication : Form
    {
        public frmInternationalLicenseApplication()
        {
            InitializeComponent();
        }
        clsLicense _LocalLicense;
        clsInternationalLicense _InternationalLicense;
        private void frmInternationalLicenseApplication_Load(object sender, EventArgs e)
        {

            _SetDefaultValues();
        }

        private void ctrIDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int LocalLicenseID = obj;
             _LocalLicense = clsLicense.FindLicensesByLicenseID(LocalLicenseID);
   
              _InternationalLicense = clsInternationalLicense.FindByLocalLicenseID(LocalLicenseID);
            _SetDefaultValues();
            if(ctrIDriverLicenseWithFilter1.IsLicenseFound)
                llHistroy.Enabled = true;
            if (_InternationalLicense == null)
            {

                if (_LocalLicense != null)
                {      
                    if (ctrIDriverLicenseWithFilter1.IsActive)
                    {
                        if (!ctrIDriverLicenseWithFilter1.IsDetained)
                        {
                            btnSave.Enabled = true;
                            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
                            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
                            lblFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
                            lblLocalLiceneID.Text = LocalLicenseID.ToString();
                            lblExDate.Text = clsFormat.DateToShort(_LocalLicense.ExpirationDate);
                            //  lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
                        }
                        else
                        {
                            MessageBox.Show("This License is Detained...Please Relese it and try again.", "Not Active ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _SetDefaultValues();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This License is not Active...So you cann't Issue Internationsl Licene.", "Not Active ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _SetDefaultValues();
                    }
                }
            }
            else
            {
                    llInfo.Enabled = true;
                lblInterLicensID.Text = _InternationalLicense.InternationalLicenseID.ToString();
                lblInterApplication.Text= _InternationalLicense.ApplicationID.ToString();
                lblLocalLiceneID.Text= _InternationalLicense.IssuedUsingLocalLicenseID.ToString();     
                MessageBox.Show("This Person allredy has an active international license with ID : "+ _InternationalLicense.InternationalLicenseID, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
            }
        }

        private void _SetDefaultValues()
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblLocalLiceneID.Text ="[????]";
            lblExDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            //  lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
            btnSave.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplication ApplicationFroInterNationalLicense = new clsApplication();
            clsInternationalLicense NewInternationalLicense= new clsInternationalLicense();

            ApplicationFroInterNationalLicense.ApplicantPersonID = _LocalLicense.PersonInfo.PersonID;
            ApplicationFroInterNationalLicense.ApplicationDate = DateTime.Now;
            ApplicationFroInterNationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.New;
            ApplicationFroInterNationalLicense.ApplicationTypeID = clsApplication.enApplicationType.NewInternationalLicense;
            ApplicationFroInterNationalLicense.CreatedByUserID = 1;//clsGlobal.CurrentUser.UserID;
            ApplicationFroInterNationalLicense.LastStatusDate= DateTime.Now;
            ApplicationFroInterNationalLicense.Save();
            lblInterApplication.Text = ApplicationFroInterNationalLicense.ApplicatoinID.ToString();

            NewInternationalLicense.IssueDate = DateTime.Now;
             NewInternationalLicense.CreatedByUserID = 1;//clsGlobal.CurrentUser.UserID;
            NewInternationalLicense.DriverID = _LocalLicense.DriverID;
            NewInternationalLicense.IsActive = true;
            NewInternationalLicense.ApplicationID = ApplicationFroInterNationalLicense.ApplicatoinID;
            NewInternationalLicense.IssuedUsingLocalLicenseID = _LocalLicense.LicenseID;
            NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);  //  i should return here 

            if (NewInternationalLicense.Save())
            {

                lblInterLicensID.Text = NewInternationalLicense.InternationalLicenseID.ToString();
                MessageBox.Show("Save Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                llInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Save Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void llHistroy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =new frmShowPersonLicenseHistory(_LocalLicense.PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void llInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LocalLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}
