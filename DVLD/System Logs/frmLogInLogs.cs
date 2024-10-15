using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.System_Logs
{
    public partial class frmLogInLogs : Form
    {
        public frmLogInLogs()
        {
            InitializeComponent();
        }

        private void frmLogInLogs_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource=clsLoginLogs.GetAllLoginLogs();
        }
    }
}
