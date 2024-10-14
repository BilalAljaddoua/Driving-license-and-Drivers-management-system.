using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsDeletedTests
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? RecordID { set; get; }
        public int? TestID { set; get; }
        public int? TestAppointmentID { set; get; }
        public bool? TestResult { set; get; }
        public string Notes { set; get; }
        public int? CreatedByUserID { set; get; }
        public int? DeletedByUser { set; get; }


        public clsDeletedTests()
        {
            this.RecordID = null;
            this.TestID = null;
            this.TestAppointmentID = null;
            this.TestResult = null;
            this.Notes = null;
            this.CreatedByUserID = null;
            this.DeletedByUser = null;
            Mode = enMode.AddNew;
        }

        clsDeletedTests(int? RecordID, int? TestID, int? TestAppointmentID, bool? TestResult, string Notes, int? CreatedByUserID, int? DeletedByUser)
        {
            this.RecordID = RecordID;
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.DeletedByUser = DeletedByUser;
            Mode = enMode.Update;
        }
         

        static public DataTable GetAllDeletedTests()
        {
            return clsDeletedTestsData.GetAllDeletedTests();
        }
         

        static public clsDeletedTests FindDeletedTests(int? RecordID)
        {
            int? TestID = null; int? TestAppointmentID = null; bool? TestResult = null; string Notes = null; int? CreatedByUserID = null; int? DeletedByUser = null;

            if (clsDeletedTestsData.FindDeletedTests(ref RecordID, ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID, ref DeletedByUser))
            {
                return new clsDeletedTests(RecordID, TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID, DeletedByUser);
            }
            return null;
        } 
    }
}
