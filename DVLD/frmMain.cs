using Bussiness_Layer;
using DVLD.Applications;
using DVLD.Applications.International_License;
using DVLD.Applications.Local_Driving_License;
using DVLD.Applications.Renew_Local_License;
using DVLD.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Applications.Rlease_Detained_License;
using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD.Licenses.Detain_License;
using DVLD.Licenses.International_License;
using DVLD.Licenses.International_Licenses;
using DVLD.Login;
using DVLD.People;
using DVLD.System_Logs;
using DVLD.Tests;
using DVLD.User;
using DVLD_Buisness;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace DVLD
{

    public partial class frmMain : Form
    {
        private frmLogin _frmLogin;
        public int? NumbeerOfStage;
        public frmMain( frmLogin frm )
        {
            InitializeComponent();
            _frmLogin= frm;
        }
 
        public frmMain( int  UserID )
        {
            InitializeComponent();
            clsGlobal.CurrentUser=clsUser.FindByUserID(UserID);
        }
        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            this.Refresh();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLoginLogs logs = clsLoginLogs.FindLoginLogs(NumbeerOfStage);
            logs.DateOfLoginOut = DateTime.Now;
            logs.Save();
            clsGlobal.CurrentUser = null;
            this.Tag = 1;
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
                

        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReNewLocalLicense frm =new frmReNewLocalLicense();
            frm.ShowDialog();

        }

       

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

 
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

      
        private void vehiclesLicensesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
       frmListLocalDrivingLicesnseApplications frm=new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();

        }

      

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenses frm = new frmListInternationalLicenses();
            frm.ShowDialog();

        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicense frm=new frmReplaceLostOrDamagedLicense();
            frm.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListLicensesDetaied frm =new frmListLicensesDetaied();
            frm.ShowDialog();

        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmDetainedLicense frm=new frmDetainedLicense();
            frm.ShowDialog();

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm= new frmReleaseDetainedLicense();
            frm.ShowDialog();

        }

        private void oNewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeleteLogs frm=new frmDeleteLogs();
            frm.ShowDialog();
        }

        private void loginLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogInLogs frm=new frmLogInLogs();
            frm.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            clsLoginLogs logs = clsLoginLogs.FindLoginLogs(NumbeerOfStage);
            logs.DateOfLoginOut = DateTime.Now;
            logs.Save();

        }

        private void updateLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateLogs frm=new frmUpdateLogs();
            frm.ShowDialog();
        }
    }
}
