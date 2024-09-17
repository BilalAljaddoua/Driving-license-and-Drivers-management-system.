using DVLD.Classes;
using DVLD.Properties;
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

namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlShowInernationalLicenseinfo : UserControl
    {
        public ctrlShowInernationalLicenseinfo()
        {
            InitializeComponent();
        }
        private int _InternationalLicenseID;
        private clsInternationalLicense _InternationalLicense;
        public void LoadData(int InternationalLicenseID)
        {
          //  _ResetDefaultValues();

            _InternationalLicenseID = InternationalLicenseID;
          _InternationalLicense=clsInternationalLicense.FindByInternationalLicenseID(InternationalLicenseID);
            if (_InternationalLicense != null)
            {
                lblInternationalID.Text = _InternationalLicense.InternationalLicenseID.ToString();
                lblAppID.Text = _InternationalLicense.ApplicationID.ToString();
                lblDateOfBirth.Text= clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);
                lblDriverID.Text= _InternationalLicense.DriverID.ToString();
                lblExDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);
                lblFullName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
                lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Femal";
                lblIsActive.Text= (_InternationalLicense.IsActive==true ?"Yes":"No");
                lblIssueDate.Text= clsFormat.DateToShort(_InternationalLicense.IssueDate);
                 lblnationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
                lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
                if (!string.IsNullOrEmpty(_InternationalLicense.DriverInfo.PersonInfo.ImagePath))
                {
                    pbPersonalImage.Image = Image.FromFile(_InternationalLicense.DriverInfo.PersonInfo.ImagePath);
                }
                else { pbPersonalImage.Image = Resources.Male_512; }
                pbGendor.Image = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? Resources.Male_512 : Resources.Female_512;



            }

            
        }

       private void _ResetDefaultValues()
        {
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExDate.Text = "[????]";
            lblFullName.Text = "[????]";
            lblGendor.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblLocalLicenseID.Text = "[????]";
            lblnationalNo.Text = "[????]";
            pbPersonalImage.Image = Resources.Male_512;
             pbGendor.Image =  Resources.Male_512  ;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
