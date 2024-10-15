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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.System_Logs
{
    public partial class frmLogInLogs : Form
    {
        public frmLogInLogs()
        {
            InitializeComponent();
        }
        DataTable dtLogs=new DataTable();
        private void frmLogInLogs_Load(object sender, EventArgs e)
        {
            dtLogs = clsLoginLogs.GetAllLoginLogs();
            dataGridView1.DataSource=dtLogs;
            if (dataGridView1.RowCount > 0)
            {
                 dataGridView1.Columns[0].Width =100 ;
                dataGridView1.Columns[0].HeaderText = "Record ID";

                 dataGridView1.Columns[1].Width =100 ;
                dataGridView1.Columns[1].HeaderText = "User ID";

                dataGridView1.Columns[2].HeaderText = "Full Name";
                dataGridView1.Columns[2].Width = 200;

                dataGridView1.Columns[3].HeaderText = "UserName";
                dataGridView1.Columns[3].Width = 150;

                dataGridView1.Columns[4].HeaderText = "Login Date";
                dataGridView1.Columns[4].Width = 300;

                dataGridView1.Columns[5].HeaderText = "Logout Date";
                dataGridView1.Columns[5].Width = 300;   

            }
             dtpFrom.Value = DateTime.Now.AddDays(-7);
            dtpTo.Value = DateTime.Now.AddDays(1);
            DataTable dt = new DataTable();
            dt = clsUser.GetAllUsers();
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
                string filterExpression = $"[UserName] = '{Filter}' AND [DateOfLogin] >= '{dtpFrom.Value:yyyy-MM-dd}' AND [DateOfLogin] <= '{dtpTo.Value:yyyy-MM-dd}'";
                dtLogs.DefaultView.RowFilter = filterExpression;
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("There are no results by UserName:" + cbUsers.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbUsers.SelectedIndex = -1;
                 }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
