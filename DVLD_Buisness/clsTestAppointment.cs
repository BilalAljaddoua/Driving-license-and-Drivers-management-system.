using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestAppointment
    {

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; }
        public int CreatedByUserID { get; set; }
        public int TestID { get { return _GetTestID(); } }
        public int ReTakeTestAppliactionID { get; set; }

        private enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode { get; set; }



        public clsTestAppointment() { _Mode = enMode.AddNew; }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, bool IsLocked, int CreatedByUserID, int ReTakeTestAppliactionID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.IsLocked = IsLocked;
            this.CreatedByUserID = CreatedByUserID;
            this.AppointmentDate = AppointmentDate;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.PaidFees = PaidFees;
            this.ReTakeTestAppliactionID = ReTakeTestAppliactionID;
            _Mode = enMode.Update;
        }




        private bool _AddTestAppointment()
        {
            int TestAppointment = -1;
            TestAppointment = clsTestAppointmentData.AddTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, IsLocked, CreatedByUserID, ReTakeTestAppliactionID);
            this.TestAppointmentID = TestAppointment;
            return TestAppointment != -1;
        }
        static public clsTestAppointment FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestTypeID = 0, ReTakeTestAppliactionID = 0, LocalDrivingLicenseApplicationID = 0; DateTime AppointmentDate = DateTime.Now; float PaidFees = 0; bool IsLocked = false; int CreatedByUserID = 0;

            if (clsTestAppointmentData.FindTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref IsLocked, ref CreatedByUserID, ReTakeTestAppliactionID))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, IsLocked, CreatedByUserID, ReTakeTestAppliactionID);
            }
            else
                return null;



        }
        private bool _UpdateTestByTestID()
        {
            return clsTestAppointmentData.UpdateTestByTestID(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, IsLocked, CreatedByUserID);
        }
        static public bool DeleteByTestAppointmentID(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestByTestAppointmentID(TestAppointmentID);
        }
        static public bool IsThereActiveAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.IsThereActiveAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        static public int GetActiveAppointmentID(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetActiveAppointmentID(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        static public DataTable GetAllTestsByLDLAppAndTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetAllTestsByLDLAppAndTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool LockAppintment()
        {
            bool IsLocked = false;
            return clsTestAppointmentData.LockAppintment(TestAppointmentID, IsLocked);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        return _AddTestAppointment();

                    }
                case enMode.Update:
                    {
                        return _UpdateTestByTestID();
                    }
                default:
                    {
                        return false;
                    }
            }
        }

























    }
}
