using Bussiness_Layer;
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

namespace DVLD.System_Logs.ShowDiff
{
    public partial class frmShowDiffForLDLA : Form
    {
        public frmShowDiffForLDLA(int OldInfo,int NewInfo)
        {
            InitializeComponent();
            _OldInfo = OldInfo;
            _NewInfo = NewInfo;
        }
        private int _OldInfo;
        private int _NewInfo;
        private void frmShowDiffForLDLA_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_OldInfo);
            clsUpdatedLocalDrivingLicenseApplications UpdatedLocal = clsUpdatedLocalDrivingLicenseApplications.FindUpdatedLocalDrivingLicenseApplications(_NewInfo);
            if (UpdatedLocal != null)
            {
                lblRecordID.Text = UpdatedLocal.RecordID.ToString();
                lblByUser.Text=(clsUser.FindByUserID(UpdatedLocal.UpdatedByUser.Value).UserName.ToString());
                lblNewClass.Text=clsLicenseClass.Find(UpdatedLocal.LicenseClassID.Value).ClassName;
                lblUpdateDate.Text=UpdatedLocal.UpdatedDate.ToString();
                lblLDLA.Text=UpdatedLocal.LocalDrivingLicenseApplicationID.ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
