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
    public partial class frmShowDiffTestAppointment : Form
    {
        public frmShowDiffTestAppointment(int OldInfo,int NewInfo)
        {
            InitializeComponent();
            _NewInfo = NewInfo;
            _OldInfo = OldInfo;
        }
        private int _OldInfo;
        private int _NewInfo;

        private void frmShowDiffTestAppointment_Load(object sender, EventArgs e)
        {
            clsTestAppointment NewtestAppointment=clsTestAppointment.FindByTestAppointmentID(_NewInfo);
            clsTestType.enTestType testType = (clsTestType.enTestType)NewtestAppointment.TestTypeID;
            ctrlSecheduledTest1.LoadData(_NewInfo, testType);
            if (NewtestAppointment.IsLocked)
            {
                lblNewIslock.Text = "Yes";
            }
            else
            {
                lblNewIslock.Text = "No";
            }

            clsUpdatedTestAppointments OldTestAppointment = clsUpdatedTestAppointments.FindUpdatedTestAppointments(_OldInfo);
            NewtestAppointment.AppointmentDate = OldTestAppointment.AppointmentDate.Value;
            NewtestAppointment.PaidFees  = (float)OldTestAppointment.PaidFees.Value;
            NewtestAppointment.IsLocked  = OldTestAppointment.IsLocked.Value;
            if (OldTestAppointment.RetakeTestApplicationID!=null)
                NewtestAppointment.ReTakeTestAppliactionID  = OldTestAppointment.RetakeTestApplicationID.Value; 
            ctrlSecheduledTestNew.LoadData(NewtestAppointment);


            if(NewtestAppointment.IsLocked)
            {
                lblOld.Text = "Yes";
            }
            else
            {
                lblOld.Text = "No";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
