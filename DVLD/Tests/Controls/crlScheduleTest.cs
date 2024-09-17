using DVLD.Classes;
using DVLD.Properties;
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
using static DVLD_Buisness.clsTestType;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Tests
{

  
    public partial class ctrlScheduleTest : UserControl
    {
      
        public  enum enMode { AddNew=1,Update=2}
        private enMode _Mode = enMode.AddNew;
       public enum enCreationMode { FirstTimeSchedule = 1, RetakeTestSchedule };
       private enCreationMode _CreationMode=enCreationMode.FirstTimeSchedule;
 
       private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
       private int _localDrivingLicenseApplicationID = -1;

        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        private clsTestType.enTestType _TestType;
        public clsTestType.enTestType TestType
        {
            get { return _TestType; }
            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case enTestType.VisionTest:
                        {
                            lblTitle.Text = "Vision Test";
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }
                    case enTestType.WrittenTest:
                        {

                            lblTitle.Text = "Written Test";
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;

                            break;
                        }
                    case enTestType.PracticalTest:
                        {

                            lblTitle.Text = "Practical Test";
                            gbTestType.Text = "Practical Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }





                }
            }
        }



        public void LoadInfo(int LocalDrivingLicenseApplicationID,int AppointmentID=-1)
        {
            if(AppointmentID==-1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


            _localDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID;
            _TestAppointmentID=AppointmentID;

            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
            if( _localDrivingLicenseApplication == null )
            {
                MessageBox.Show("There is no Local Application with localDrivingLicenseApplication : " + LocalDrivingLicenseApplicationID);
                return;
            }

            if (!_localDrivingLicenseApplication.IsAttendTest(TestType))
                _CreationMode = enCreationMode.FirstTimeSchedule;
            else
                _CreationMode = enCreationMode.RetakeTestSchedule;

            if (_CreationMode == enCreationMode.FirstTimeSchedule)
            {
                gbRetakeTestInfo.Enabled = false;
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                lblUserMessage.Visible = false;
 

            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppFees.Text = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lblUserMessage.Visible = false;
                lblRetakeTestAppID.Text = "N/A";


            }

            lblLocalDrivingLicenseAppID.Text = LocalDrivingLicenseApplicationID.ToString();
            lblFullName.Text=_localDrivingLicenseApplication.PersonFullName.ToString();
            lblDrivingClass.Text=_localDrivingLicenseApplication.LicenseClassInfo.ClassName;


            lblTrial.Text= _localDrivingLicenseApplication.NuberOfTrial(_TestType).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = (clsTestType.FindTestType(_TestType)).Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment=new clsTestAppointment();

            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;

            }
             

             lblTotalFees.Text=(Convert.ToSingle(lblRetakeAppFees.Text)+Convert.ToSingle(lblFees.Text)).ToString();

            if (!_HandleAppointmentLockedConstraint())
                return;
            if(!_HandlePrviousTestConstraint())
                return;
            if(!_HandleActiveTestAppointmentConstraint())
                 return;
             



        }
        private bool _LoadTestAppointmentData()
        {
         _TestAppointment=clsTestAppointment.FindByTestAppointmentID(_TestAppointmentID);
                 if (_TestAppointment == null)
                {
                    MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = false;
                    return false;
                }

            lblFees.Text=_TestAppointment.PaidFees.ToString();
            if(DateTime.Compare(DateTime.Now,_TestAppointment.AppointmentDate)>0)
            {
                dtpTestDate.MinDate= DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
            }

            if(_TestAppointment.ReTakeTestAppliactionID==-1)
            {
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
            }
            else
            {
                lblRetakeAppFees.Text=clsApplicationType.FindApplicationType(clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                gbRetakeTestInfo.Enabled = true ;
                lblTitle.Text = "Schedule Retake Test";
               lblRetakeTestAppID.Text=_TestAppointment.ReTakeTestAppliactionID.ToString();

            }
            return true;
        }
         
        private bool _HandleActiveTestAppointmentConstraint()
        {

            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveSchudleTest(_localDrivingLicenseApplicationID,_TestType))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;

            }


            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {

            if(_TestAppointment.IsLocked)
            {
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Visible = true;
                lblUserMessage.Text = " This Test is Locked";
                return true;
            }

            return false;
        }
        private bool _HandlePrviousTestConstraint()
        {

             switch(_TestType)
            {
                case enTestType.VisionTest:
                    {
                         break;
                    }
                case enTestType.WrittenTest:
                    {
                        if (!_localDrivingLicenseApplication.DosePassTesttype(_TestType))
                        {
                            lblUserMessage.Visible = true;
                            lblUserMessage.Text = " You have to pass vision test firstly...";                            
                            return false;

                        }
                        else
                        {
                            lblUserMessage.Visible = false;
                            return true;
                        }
                    }
                case enTestType.PracticalTest:
                    {
                        if (!_localDrivingLicenseApplication.DosePassTesttype(_TestType))
                        {
                            lblUserMessage.Visible = true;
                            lblUserMessage.Text = " You have to pass Written test firstly...";
                            return false;

                        }
                        else
                        {
                            lblUserMessage.Visible = false;
                            return true;
                        } 
                    }
            }
    
            return true;

        }
        private bool _HandleRetakeApplication()
        {
           if(_Mode==enMode.AddNew&&_CreationMode==enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID=_localDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.ApplicationDate=DateTime.Now;
                Application.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
                Application.CreatedByUserID=clsGlobal.CurrentUser.UserID;
                Application.PaidFees = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.RetakeTest).Fees;

                if(!Application.Save())
                {
                    MessageBox.Show("Save Error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _TestAppointment.ReTakeTestAppliactionID = -1;
                    return false;
                }
                _TestAppointment.ReTakeTestAppliactionID = Application.ApplicationID;
               

            } 
           return true;
         
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;
 

            _TestAppointment.TestTypeID =(int) _TestType;
            _TestAppointment.LocalDrivingLicenseApplicationID = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblTotalFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _TestAppointment.ReTakeTestAppliactionID = -1;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
