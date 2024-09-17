using DVLD.Licenses.Controls;
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

namespace DVLD.Applications.International_License.Controls
{
    public partial class ctrIDriverLicenseWithFilter : UserControl
    {


        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }


        private int _LicenseID;
        public int LicenseID
        {
            get
            {
                return ctrDriverLicenseInfo1.LicenseID;
            }
        }
        public clsLicense SelectedLicenseInfo
        { get { return ctrDriverLicenseInfo1.SelectedLicenseInfo; } }

        public bool IsActive
        {
            get { return ctrDriverLicenseInfo1.IsActive; }
        }
        public DateTime ExpirationDate
        {
            get { return ctrDriverLicenseInfo1.ExpirationDate; }
        }
        public bool IsDetained
        {
            get { return ctrDriverLicenseInfo1.IsDetained; }
        }
        public bool IsLicenseFound
        { get { return ctrDriverLicenseInfo1.IsLicenseFound; }        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                 gbFilters.Enabled = _FilterEnabled;
            }
        }
 

        public ctrIDriverLicenseWithFilter()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFilter.Focus();
                return;

            }
 
            _LicenseID=Convert.ToInt32(txtFilter.Text);
            LoadDateInfo(_LicenseID);

        }
        public void LoadDateInfo(int LicenseID)
        {
            txtFilter.Text = LicenseID.ToString();  

            ctrDriverLicenseInfo1.LoadLicenseInfo(Convert.ToInt16(txtFilter.Text));
            _LicenseID = ctrDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }
        private void ctrIDriverLicenseWithFilter_Load(object sender, EventArgs e)
        {
 
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar ==(char)13)
                btnFind.PerformClick();


        }
        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "This Field is required");
                txtFilter.Focus();

            }
            else { errorProvider1.SetError(txtFilter, null); }
        }
        public void FilterFocus()
        {
            txtFilter.Focus();  
        }
     
    }
}
