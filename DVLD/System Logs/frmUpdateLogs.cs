using Bussiness_Layer;
using DVLD.Classes;
using DVLD.People;
using DVLD.System_Logs.ShowDiff;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.System_Logs
{
    public partial class frmUpdateLogs : Form
    {
        public frmUpdateLogs()
        {
            InitializeComponent();
        }
        DataTable dt = clsUpdatedPeople.GetAllUpdatedPeople();
        private void frmUpdateLogs_Load(object sender, EventArgs e)
        {
            cbField.SelectedIndex = 0;
            _RefreshForm();
        }
        private void _RefreshForm()
        {

            dataGridView1.DataSource = dt;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
            dtpTo.Value = DateTime.Now.AddDays(1);
            DataTable dtUser = new DataTable();
            dtUser = clsUser.GetAllUsers();
            foreach (DataRow dr in dtUser.Rows)
            {
                string userName = dr["UserName"].ToString();
                cbUsers.Items.Add(userName);
            }
        }
        private void showUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cbField.SelectedIndex == 0)
            {
                frmShowDiffForPerson frmShow = new frmShowDiffForPerson((int)dataGridView1.CurrentRow.Cells[0].Value, (string)dataGridView1.CurrentRow.Cells[4].Value);
                frmShow.ShowDialog();
                _RefreshForPeople();
            }
            else if(cbField.SelectedIndex == 1)
            {
                frmShowDiffTestAppointment frmShowDiffTest = new frmShowDiffTestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value, (int)dataGridView1.CurrentRow.Cells[1].Value);
                frmShowDiffTest.ShowDialog();
            }
            else if (cbField.SelectedIndex == 2)
            {
                frmShowDiffForLDLA frmShowDiff = new frmShowDiffForLDLA((int)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value);
                frmShowDiff.ShowDialog();
            }
        }
        private void _RefreshForPeople()
        {
            dt = clsUpdatedPeople.GetAllUpdatedPeople();
            _RefreshForm();
        }
        private void _RefreshForLDLA()
        {
            dt.Clear();
            dt = clsUpdatedLocalDrivingLicenseApplications.GetAllUpdatedLocalDrivingLicenseApplications();
            dataGridView1.DataSource = dt;
        }
        private void _RefreshForTestAppointment()
        {
            dt.Clear();
            dt = clsUpdatedTestAppointments.GetAllUpdatedTestAppointments();
            dataGridView1.DataSource = dt;
        }

        private void undoTheModificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cbField.SelectedIndex == 0)
            {
                if (MessageBox.Show("Do you want to Undo the modification?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    clsPerson person = clsPerson.Find((string)dataGridView1.CurrentRow.Cells[4].Value);
                    clsUpdatedPeople clsUpdated = clsUpdatedPeople.FindUpdatedPeople((int)dataGridView1.CurrentRow.Cells[0].Value);
                    if (person != null)
                    {
                        person.FirstName = clsUpdated.FirstName;
                        person.LastName = clsUpdated.LastName;
                        person.Email = clsUpdated.Email;
                        person.Phone = clsUpdated.Phone;
                        person.Address = clsUpdated.Address;
                        person.ImagePath = clsUpdated.ImagePath;
                        person.ThirdName = clsUpdated.ThirdName;
                        person.Gendor = (short)clsUpdated.Gendor;
                        person.DateOfBirth = (DateTime)clsUpdated.DateOfBirth;
                        person.NationalityCountryID = (int)clsUpdated.NationalityCountryID;
                        person.SecondName = clsUpdated.SecondName;
                        person.UpdatedByUser = clsGlobal.CurrentUser.UserID;
                        if (person.Save())
                        {
                            MessageBox.Show("Undo the modification Succfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                        _RefreshForPeople();
                    }
                }
            }
            else if (cbField.SelectedIndex == 1)
            {
                if (MessageBox.Show("Do you want to Undo the modification?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    clsTestAppointment NewAppointmentInfo = clsTestAppointment.FindByTestAppointmentID((int)dataGridView1.CurrentRow.Cells[1].Value);
                    clsUpdatedTestAppointments OldAppointmentInfo = clsUpdatedTestAppointments.FindUpdatedTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value);
                    if (NewAppointmentInfo != null&&OldAppointmentInfo != null)
                    {
                        NewAppointmentInfo.IsLocked= OldAppointmentInfo.IsLocked.Value ;
                        NewAppointmentInfo.AppointmentDate = OldAppointmentInfo.AppointmentDate.Value;
                        NewAppointmentInfo.UpdatedByUserID = OldAppointmentInfo.UpdatedByUser.Value;
                        NewAppointmentInfo.PaidFees = (float)OldAppointmentInfo.PaidFees;


                        if (NewAppointmentInfo.Save())
                        {
                            MessageBox.Show("Undo the modification Succfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _RefreshForTestAppointment();

                        }
                        else
                        {
                            MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                     }
                }

            }
            else if (cbField.SelectedIndex == 2)
            {
                if (MessageBox.Show("Do you want to Undo the modification?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    clsLocalDrivingLicenseApplication drivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID((int)dataGridView1.CurrentRow.Cells[1].Value );
                    clsUpdatedLocalDrivingLicenseApplications OldInfo = clsUpdatedLocalDrivingLicenseApplications.FindUpdatedLocalDrivingLicenseApplications((int)dataGridView1.CurrentRow.Cells[0].Value);
                     if (drivingLicenseApplication != null&& OldInfo!=null)
                    {

                        drivingLicenseApplication.LicenseClassID = OldInfo.LicenseClassID.Value;
                        drivingLicenseApplication.UpdatedByUser = OldInfo.UpdatedByUser.Value;
                        drivingLicenseApplication.UpdateDate=DateTime.Now;
                       
                        if (drivingLicenseApplication.Save())
                        {
                            MessageBox.Show("Undo the modification Succfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _RefreshForLDLA();
                        }
                        else
                        {
                            MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                         
                    }
                }

            }

        }
        private void PreperForPeople()
        {
            if (cbUsers.SelectedIndex >= 0)
            {
                string Filter = cbUsers.SelectedItem.ToString();
                string filterExpression = $"[UserName] = '{Filter}' AND [DateOfUpdate] >= '{dtpFrom.Value:yyyy-MM-dd}' AND [DateOfUpdate] <= '{dtpTo.Value:yyyy-MM-dd}'";
                dt.DefaultView.RowFilter = filterExpression;
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("There are no results by UserName:" + cbUsers.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbUsers.SelectedIndex = -1;
                }
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Record ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "Date of Update";
                dataGridView1.Columns[1].Width = 200;

                dataGridView1.Columns[2].HeaderText = "Updated By User ";
                dataGridView1.Columns[2].Width = 100;

                dataGridView1.Columns[3].HeaderText = "Person ID";
                dataGridView1.Columns[3].Width = 100;

                dataGridView1.Columns[4].HeaderText = "National No";
                dataGridView1.Columns[4].Width = 100;

                dataGridView1.Columns[5].HeaderText = "First Name";
                dataGridView1.Columns[5].Width = 100;

                dataGridView1.Columns[6].HeaderText = "Secound Name";
                dataGridView1.Columns[6].Width = 100;

                dataGridView1.Columns[7].HeaderText = "Thired Name";
                dataGridView1.Columns[7].Width = 100;

                dataGridView1.Columns[8].HeaderText = "Last Name";
                dataGridView1.Columns[8].Width = 100;

                dataGridView1.Columns[9].HeaderText = "Date Of Birth";
                dataGridView1.Columns[9].Width = 150;

                dataGridView1.Columns[10].HeaderText = "Gender";
                dataGridView1.Columns[10].Width = 100;

                dataGridView1.Columns[11].HeaderText = "Address";
                dataGridView1.Columns[11].Width = 100;

                dataGridView1.Columns[12].HeaderText = "Phone";
                dataGridView1.Columns[12].Width = 100;

                dataGridView1.Columns[13].HeaderText = "Email";
                dataGridView1.Columns[13].Width = 150;

                dataGridView1.Columns[14].HeaderText = "Country";
                dataGridView1.Columns[14].Width = 100;

                dataGridView1.Columns[15].HeaderText = "Image Path";
                dataGridView1.Columns[15].Width = 200;

            }
        }
        private void PreperForLDLA()
        {
            dt=clsUpdatedLocalDrivingLicenseApplications.GetAllUpdatedLocalDrivingLicenseApplications();
            dataGridView1.DataSource = dt;


            if (dataGridView1.Rows.Count > 0)
            {
                 dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "Record ID";

                dataGridView1.Columns[1].Width =300;
                dataGridView1.Columns[1].HeaderText = "Local Driving License ID";

                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[2].HeaderText = "Application ID ";

                dataGridView1.Columns[3].HeaderText = "Old Class Name";
                dataGridView1.Columns[3].Width = 250;

                dataGridView1.Columns[4].HeaderText = "Updated By User";
                dataGridView1.Columns[4].Width = 200;

                dataGridView1.Columns[5].HeaderText = "Updated Date";
                dataGridView1.Columns[5].Width = 150;
                 
                 
            }

        }
        private void PreperForTestAppointments()
        {
            dt = clsUpdatedTestAppointments.GetAllUpdatedTestAppointments();
            dataGridView1.DataSource = dt;


            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "Record ID";

                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[1].HeaderText = "Test Appointment ID";

                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[2].HeaderText = "Test Type  ";

                dataGridView1.Columns[3].HeaderText = "Application Type";
                dataGridView1.Columns[3].Width = 200;

                dataGridView1.Columns[4].HeaderText = "Old Appointment Date";
                dataGridView1.Columns[4].Width = 100;

                dataGridView1.Columns[5].HeaderText = "Paid Fees";
                dataGridView1.Columns[5].Width = 100;


                dataGridView1.Columns[6].HeaderText = "Is locked";
                dataGridView1.Columns[6].Width = 100;

                dataGridView1.Columns[7].HeaderText = "Retake TestApplication ID";
                dataGridView1.Columns[7].Width = 100;

                dataGridView1.Columns[8].HeaderText = "Updated By User";
                dataGridView1.Columns[8].Width = 100;

                dataGridView1.Columns[9].HeaderText = "Updated Date";
                dataGridView1.Columns[9].Width = 100;

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbField.SelectedIndex == 0)
            {
                PreperForPeople();
            }
            else if (cbField.SelectedIndex == 1)
            {
                PreperForTestAppointments();
            }
            else if (cbField.SelectedIndex == 2)
            {
                PreperForLDLA();

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedIndex >= 0)
            {
                string Filter = cbUsers.SelectedItem.ToString();
                string filterExpression = $"[UserName] = '{Filter}' AND [UpdatedDate] >= '{dtpFrom.Value:yyyy-MM-dd}' AND [UpdatedDate] <= '{dtpTo.Value:yyyy-MM-dd}'";
                dt.DefaultView.RowFilter = filterExpression;
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("There are no results by UserName:" + cbUsers.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbUsers.SelectedIndex = -1;
                }
            }
        }
    }
}
