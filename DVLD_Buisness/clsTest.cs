using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTest
    {

        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public int TestID { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTestAppointment TestAppointmentInfo {  get; set; }
        enum enMode { enAddNew = 1, enUpdate = 2 }
        private enMode _Mode { get; set; }




        public clsTest()
        {
            _Mode = enMode.enAddNew;
        }
        public clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.TestID = TestID;
            TestAppointmentInfo=clsTestAppointment.FindByTestAppointmentID(TestAppointmentID);  
            _Mode = enMode.enUpdate;
        }

        static private bool _AddNewTests(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            return clsTestData.AddTests(TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }
        static public clsTest FindTestByTestID(int TestID)
        {
            int TestAppointmentID = 0; bool TestResult = false; string Notes = ""; int CreatedByUserID = 0;
            if (clsTestData.FindTestByTestID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            return null;
        }
         static public clsTest FindTestByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = 0; bool TestResult = false; string Notes = ""; int CreatedByUserID = 0;
            if (clsTestData.FindTestByTestAppointmentID(TestAppointmentID, ref TestID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            return null;
        }
         static private bool _UpdateTestByTestID(int TestID, byte TestResult, string Notes, int CreatedByUserID)
        {
            return clsTestData.UpdateTestByTestID(TestID, TestResult, Notes, CreatedByUserID);
        }
         static private bool _UpdateTestByTestAppointmentID(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            return clsTestData.UpdateTestByTestAppointmentID(TestAppointmentID, TestResult, Notes, CreatedByUserID);

        }
         static public bool DeleteTestByTestAppointmentID(int TestAppointmentID)
        {
            return clsTestData.DeleteTestByTestAppointmentID(TestAppointmentID);
        }
         static public bool DeleteTestByTestID(int TestID)
        {
            return clsTestData.DeleteTestByTestID(TestID);
        }
        static public bool IsLDLAppPassTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestData.IsLDLAppPassTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        static public int GetPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTests(LocalDrivingLicenseApplicationID);
        }
        static public bool IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.IsPassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    {
                        return _AddNewTests(TestAppointmentID, TestResult, Notes, CreatedByUserID);
                    }
                case enMode.enUpdate:
                    {
                        return _UpdateTestByTestAppointmentID(TestAppointmentID, TestResult, Notes, CreatedByUserID);
                    }
                default: { return false; }

            }
        }



    }
}
