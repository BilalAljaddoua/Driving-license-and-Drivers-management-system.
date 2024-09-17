using DVLD.DriverLicense;
using DVLD.Licenses.International_License;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
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

namespace DVLD.Applications.Local_Driving_License
{

    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;

        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplication();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 75;
            }

            cbFilterBy.SelectedIndex = 0;


        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm =
                        new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            //refresh
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {


            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseAplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =  clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseAplicationID);

            int TotalPassedTest = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
                               
            bool IsLicenseExists = LocalDrivingLicenseApplication.IsPersonHasLicenseExists();;
            //Enable menue Show Driving Licenese
            // Only when there is a License
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled =( (TotalPassedTest == 3)&& !IsLicenseExists);

            //Enable / Disable Edit menue 
            // If the Application is cancelled  or completed we can't edit
            editToolStripMenuItem.Enabled = (!IsLicenseExists && LocalDrivingLicenseApplication.ApplicationStatus==clsLocalDrivingLicenseApplication.enApplicationStatus.New);

            //Enable / Disable Delete item
            // Here we only allow to delete when statue is new , no complete or cancelled
            DeleteApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enalble / Disable Cancel menue
            // We allow to cancel only application that has New mod
            CancelApplicaitonToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            showLicenseToolStripMenuItem.Enabled=(IsLicenseExists);

            ScheduleTestsMenue.Enabled = ((!IsLicenseExists) );

            // Those are used to Enable/Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DosePassTesttype((clsTestType.enTestType.VisionTest));
            bool PassedWriteTest = LocalDrivingLicenseApplication.DosePassTesttype(clsTestType.enTestType.WrittenTest);
            bool PassedPracticalTest = LocalDrivingLicenseApplication.DosePassTesttype(clsTestType.enTestType.PracticalTest);


            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWriteTest|| !PassedPracticalTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                // Here we only allow apply  if he doesn't pass this test
               scheduleVisionTestToolStripMenuItem.Enabled= !PassedVisionTest;
                // Here we only allow apply  if he doesn't pass this test and he pass vision test
                scheduleWrittenTestToolStripMenuItem.Enabled= PassedVisionTest&&!PassedWriteTest;
                // Here we only allow apply  if he doesn't pass this test and he pass Wrien ant vision Test
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest&& PassedWriteTest && !PassedPracticalTest;

            }
             

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm =new  frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value,clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.PracticalTest);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null,null);
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure to delete L.D.L.App with ID : " + ID + " ? ", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if(clsLocalDrivingLicenseApplication.DeleteByLocalDrivingLicenseApplicationID(ID))
                {
                   MessageBox.Show("Deleted Successfully ID : " + ID, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Faild ID : " + ID, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                frmListLocalDrivingLicesnseApplications_Load(null, null);
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null,null);
        }

        private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void showDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm =
            new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            //refresh
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmAddUpdateLocalDrivingLicesnseApplication frm =
                         new frmAddUpdateLocalDrivingLicesnseApplication(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =  clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID( LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
