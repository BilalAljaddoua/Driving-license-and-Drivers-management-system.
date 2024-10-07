using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Xml.Linq;
using DVLD_DataAccess;
using static System.Net.Mime.MediaTypeNames;
using static DVLD_Buisness.clsTestType;


namespace DVLD_Buisness
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        private int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID { get { return _LocalDrivingLicenseApplicationID; } }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public int LicenseClassID { get; set; }
        public string PersonFullName
        {
            get { return base.PersonInfo.FullName; }
        }


        public clsLocalDrivingLicenseApplication()
        {
            _LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            Mode = enMode.enAddNew;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            this.Mode = enMode.enUpdate;

            clsApplication App = clsApplication.FindApplicationsByApplicationID(ApplicationID);
            if (App != null)
            {
                this.ApplicantPersonID = App.ApplicantPersonID;
                this.ApplicationDate = App.ApplicationDate;
                this.ApplicationStatus = App.ApplicationStatus;
                this.ApplicationStatus = App.ApplicationStatus;
                this.ApplicationTypeID = App.ApplicationTypeID;
                this.ApplicationTypeInfo = App.ApplicationTypeInfo;
                this.CreatedByUserID = App.CreatedByUserID;
                this.LastStatusDate = App.LastStatusDate;
                this.PersonInfo = App.PersonInfo;
                this.UserInfo = App.UserInfo;
                base.Mode = App.Mode;
            }
        }

        public enum enMode { enAddNew = 1, enUpdate = 2 };
        public enMode Mode;
        
        static public DataTable GetAllLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        private bool _AddLocalDrivingLicenseApplications()
        {
            int ID = clsLocalDrivingLicenseApplicationData.AddLocalDrivingLicenseApplications(ApplicationID, LicenseClassID);
            _LocalDrivingLicenseApplicationID = ID;
            return ID != -1;
        }
        static public clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            if (clsLocalDrivingLicenseApplicationData.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
            }
            return null;
        }
        static public clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;
            if (clsLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
            }
            return null;
        }

        private bool _UpdateByLocalDrivingLicenseApplicationID()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLDLAByLDLA_ID(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }
        static public bool DeleteByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
        }
        static public int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationType, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationType, LicenseClassID);
        }
        public clsTest GetLastTestPerTestType( int TestTypeID)
        {
            int TestID = -1, TestAppointmentID = -1, CreatedByUserID = -1; bool TestResult = false; string Notes = "";

            if (clsTestData.GetLastTestPerTestType(LocalDrivingLicenseApplicationID, TestTypeID, ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            return null;
        }
        static public bool DosePassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
          return    clsLocalDrivingLicenseApplicationData.DosePassTesttype(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public bool DosePassTesttype( clsTestType.enTestType TestTypeID)
        {
          return    clsLocalDrivingLicenseApplicationData.DosePassTesttype(this.LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }
        public int GetActiveLicenseID()
        {
            return clsLocalDrivingLicenseApplicationData.GetActiveLicenseID(this.LocalDrivingLicenseApplicationID, LicenseClassID);
        }
        public bool IsPersonHasLicenseExists()
        {
            return (clsLicense.IsLicenseExists(ApplicantPersonID, LicenseClassID) != -1);
          
        }
         public int NuberOfTrial(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.HowNumberTrials(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
          
        }
        public bool IsAttendTest(clsTestType.enTestType TestTypeID)
        {
            return this.NuberOfTrial(TestTypeID)!=0;
          
        }

        public bool IsThereAnActiveSchudleTest( clsTestType.enTestType TestTypeID)
        {
           return  clsLocalDrivingLicenseApplicationData.IsThereActiveAppintmentWithTest(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

       static  public bool IsThereAnActiveSchudleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereActiveAppintmentWithTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool IsPassedAllTest()
        {
            return clsTest.IsPassedAllTests(LocalDrivingLicenseApplicationID);
        }
        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUser)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindDriverByDriverID(this.PersonInfo.PersonID);


            if (Driver == null)
            {
                // here we check if this person already is driver in system...
                Driver=new clsDriver();
                Driver.PersonID = this.PersonInfo.PersonID;
                Driver.CreatedByUserID = CreatedByUser;
                Driver.CreatedDate = DateTime.Now;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();

            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool Save()
        {

            //becaous the Inhertans we should save the main information in base class

            base.Mode = (clsApplication.enMode)this.Mode;

            if (!base.Save())
            {
                return false;
            }

            //After we save the main information we going to save the sub information
            switch (Mode)
            {
                case enMode.enAddNew:
                    {
                        if (_AddLocalDrivingLicenseApplications())
                        {
                            Mode = enMode.enUpdate;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.enUpdate:
                    {
                        return _UpdateByLocalDrivingLicenseApplicationID();
                    }
                default: { return false; }
            }
        }


    }
}
