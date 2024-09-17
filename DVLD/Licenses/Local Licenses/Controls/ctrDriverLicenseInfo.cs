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
using System.IO;
namespace DVLD.Licenses.Controls
{
    public partial class ctrDriverLicenseInfo : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }
        private bool _IsActive { get; set; }
        public bool IsActive
        {
            get { return _IsActive; }
        }
        private bool _IsDetained { get; set; }
        public bool IsDetained
        {
            get { return _IsDetained; }
        }
        private DateTime _ExpirationDate { get; set; }
        public DateTime ExpirationDate 
        {
            get { return _ExpirationDate; }
        }
        private bool _IsFound {  get; set; }
        public bool IsLicenseFound
        {
            get { return _IsFound; }
        }

        private void _LoadImage()
        {
            if (_License.PersonInfo.Gendor == 0)
                pbPersonalImage.Image = Resources.Male_512;
            else
                pbPersonalImage.Image = Resources.Female_512;

            string ImagPath = _License.PersonInfo.ImagePath;
            if (ImagPath != "")
                if (!File.Exists(ImagPath))
                    MessageBox.Show("The image is not exist in path :" + ImagPath);
            else
                    pbPersonalImage.Load(ImagPath);
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            this._LicenseID = LicenseID;
            _License=clsLicense.FindLicensesByLicenseID(LicenseID);

            if(_License==null)
            {
                MessageBox.Show("There is no License with ID : "+LicenseID,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _SetDefaultValues();
                _LicenseID = -1;

                _IsFound = false;
                return;
            }
            _IsFound = true;
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblDateOfBirth.Text= _License.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text= _License.DriverID.ToString();
            lblExDate.Text= _License.ExpirationDate.ToShortDateString();
            lblGendor.Text = _License.PersonInfo.Gendor == 0 ? "Male" : "Femal";
            lblIsActive.Text= _License.IsActive==true? "Yes" : "No";
            lblIsDetained.Text =_License.IsDetained==true? "Yes" : "No"; 
            lblIssueDate.Text= _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.StringIssueReason();
            lblLicenseID.Text= _License.LicenseID.ToString();
            lblFullName.Text = _License.PersonInfo.FullName;
            lblnationalNo.Text = _License.PersonInfo.NationalNo;
            lblNote.Text = _License.Notes==""?"No Notes " : _License.Notes;
            _LoadImage();
            _IsActive = _License.IsActive;
            _IsDetained = _License.IsDetained;
            _ExpirationDate=_License.ExpirationDate;


        }
        private void _SetDefaultValues()
        {
            lblClass.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
                lblExDate.Text = "[????]";
            lblGendor.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblIsDetained.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblIssueReason.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblFullName.Text = "[????]";
            lblnationalNo.Text = "[????]";
            lblNote.Text = "[????]";
            pbGendor.Image = Resources.Male_512;
            pbPersonalImage.Image=Resources.Male_512;

        }

        public ctrDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
