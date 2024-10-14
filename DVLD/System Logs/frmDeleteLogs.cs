using Bussiness_Layer;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.System_Logs
{
    public partial class frmDeleteLogs : Form
    {
        public frmDeleteLogs()
        {
            InitializeComponent();
        }  
        private DataTable dt=new DataTable();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                GetDeletedPeople();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                GetDeletedTests();
            }
           else if(comboBox1.SelectedIndex == 2)
           {
                GetDeletedTestAppointment();
           }
            else if(comboBox1.SelectedIndex == 3)
            {
                GetDeletedLDLA();                 
            }

        }
         
        private void GetDeletedPeople()
        {

            dt.Clear();
            dt =clsDeletedPeople.GetAllDeletedPeople();
            dataGridView1.DataSource = dt;

            if (dataGridView1.Rows.Count > 0) 
            {
                dataGridView1.Columns[0].HeaderText = "RecordID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "PersonID";
                dataGridView1.Columns[1].Width = 100;

                dataGridView1.Columns[2].HeaderText = "NationalNo";
                dataGridView1.Columns[2].Width = 100;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 150;

                dataGridView1.Columns[4].HeaderText = "Date of Birth";
                dataGridView1.Columns[4].Width = 100;

                dataGridView1.Columns[5].HeaderText = "Gender";
                dataGridView1.Columns[5].Width = 100;

                dataGridView1.Columns[6].HeaderText = "Address";
                dataGridView1.Columns[6].Width = 100;

                dataGridView1.Columns[7].HeaderText = "Phone";
                dataGridView1.Columns[7].Width = 100;

                dataGridView1.Columns[8].HeaderText = "Email";
                dataGridView1.Columns[8].Width = 100;

                dataGridView1.Columns[9].HeaderText = "Image Path";
                dataGridView1.Columns[9].Width = 100;

                dataGridView1.Columns[10].HeaderText = "Country Name";
                dataGridView1.Columns[10].Width = 100;

                dataGridView1.Columns[11].HeaderText = "Delete Date";
                dataGridView1.Columns[11].Width = 100;

                dataGridView1.Columns[12].HeaderText = "Deleted By User";
                dataGridView1.Columns[12].Width = 100;

            }
            
        }
        private void GetDeletedTests()
        {
            dt.Clear();
             dt = clsDeletedTests.GetAllDeletedTests();
            dataGridView1.DataSource = dt;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Record ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "Test ID";
                dataGridView1.Columns[1].Width = 100;

                dataGridView1.Columns[2].HeaderText = "Test Appointment ID";
                dataGridView1.Columns[2].Width = 150;

                dataGridView1.Columns[3].HeaderText = "Test Result";
                dataGridView1.Columns[3].Width = 150;

                dataGridView1.Columns[4].HeaderText = "Notes";
                dataGridView1.Columns[4].Width = 300;

                dataGridView1.Columns[5].HeaderText = "Created By User";
                dataGridView1.Columns[5].Width = 100;
                 dataGridView1.Columns[6].HeaderText = "Deleted By User";
                dataGridView1.Columns[6].Width = 100;
                  dataGridView1.Columns[7].HeaderText = "Delete Date";
                dataGridView1.Columns[7].Width = 150;

 

            }

        }
        private void GetDeletedTestAppointment()
        {
            dt.Clear();
            dt = clsDeletedTestAppointments.GetAllDeletedTestAppointments();
            dataGridView1.DataSource = dt;
        }
        private void GetDeletedLDLA()
        {
            dt.Clear();
            dt = clsDeletedLocalDrivingLicenseApplications.GetAllDeletedLocalDrivingLicenseApplications();
            dataGridView1.DataSource = dt;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Record ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "Local Driving License Application ID";
                dataGridView1.Columns[1].Width = 300;

                dataGridView1.Columns[2].HeaderText = "Application Type Title";
                dataGridView1.Columns[2].Width = 250;

                dataGridView1.Columns[3].HeaderText = "Class Name";
                dataGridView1.Columns[3].Width = 250;

                dataGridView1.Columns[4].HeaderText = "Deleted By User";
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 200;

            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void frmDeleteLogs_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
            dtpTo.Value = DateTime.Now.AddDays(1);
            DataTable dt = new DataTable();
            dt=clsUser.GetAllUsers();
            foreach (DataRow dr in dt.Rows)
            {
                string userName = dr["UserName"].ToString();  
                cbUsers.Items.Add(userName);  
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedIndex >= 0)
            {
                string Filter = cbUsers.SelectedItem.ToString();
                string filterExpression = $"[DeletedByUser] = '{Filter}' AND [DeleteDate] >= '{dtpFrom.Value:yyyy-MM-dd}' AND [DeleteDate] <= '{dtpTo.Value:yyyy-MM-dd}'";
                dt.DefaultView.RowFilter = filterExpression;
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("There are no results by UserName:" + cbUsers.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbUsers.SelectedIndex = -1;
                    comboBox1.PerformLayout();
                }
            }

        }
    }
}
