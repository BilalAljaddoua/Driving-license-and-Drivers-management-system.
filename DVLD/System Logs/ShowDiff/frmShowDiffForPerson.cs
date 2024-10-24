using Bussiness_Layer;
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


namespace DVLD.System_Logs
{
    public partial class frmShowDiffForPerson : Form
    {
        public frmShowDiffForPerson(int oldInfoID,string NewInfo)
        {
            InitializeComponent();
            _OldInfoID = oldInfoID;
            _NewInfo = NewInfo;
        }
        int _OldInfoID;
        string _NewInfo
            ;
        clsUpdatedPeople _Person=new clsUpdatedPeople();
        private void LoadOldInfo()
        {
              _Person =clsUpdatedPeople.FindUpdatedPeople(_OldInfoID);
            if (_Person  != null)
            {
                 lblPersonID.Text = _Person.PersonID.ToString();
                lblNationalNo.Text = _Person.NationalNo;
                lblFullName.Text = _Person.FullName;
                lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
                lblEmail.Text = _Person.Email;
                lblPhone.Text = _Person.Phone;
                lblDateOfBirth.Text = _Person.DateOfBirth.ToString().Substring(0,11);
                lblCountry.Text = clsCountry.FindByID(Convert.ToInt32((_Person.NationalityCountryID.ToString()))).CountryName;
                lblAddress.Text = _Person.Address;
                _LoadPersonImage();



            }
        }
        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != null)
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmShowDiff_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadPersonInfo(_NewInfo);
            LoadOldInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
