using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseinfo : Form
    {
        private int _InternationalID;
        public frmShowInternationalLicenseinfo(int InternationalID)
        {
            InitializeComponent();
            this._InternationalID = InternationalID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicenseinfo_Load(object sender, EventArgs e)
        {
            ctrlShowInernationalLicenseinfo1.LoadData(_InternationalID);
        }
    }
}
