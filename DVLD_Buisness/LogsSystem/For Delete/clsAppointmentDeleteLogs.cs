using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsDeletedTestAppointments
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? RecordID { set; get; }
        public int? TestAppointmentID { set; get; }
        public int? TestTypeID { set; get; }
        public int? LocalDrivingLicenseApplicationID { set; get; }
        public DateTime? AppointmentDate { set; get; }
        public decimal? PaidFees { set; get; }
        public int? CreatedByUserID { set; get; }
        public bool? IsLocked { set; get; }
        public int? RetakeTestApplicationID { set; get; }
        public int? DeletedByUser { set; get; }


        public clsDeletedTestAppointments()
        {
            this.RecordID = null;
            this.TestAppointmentID = null;
            this.TestTypeID = null;
            this.LocalDrivingLicenseApplicationID = null;
            this.AppointmentDate = null;
            this.PaidFees = null;
            this.CreatedByUserID = null;
            this.IsLocked = null;
            this.RetakeTestApplicationID = null;
            this.DeletedByUser = null;
            Mode = enMode.AddNew;
        }

        clsDeletedTestAppointments(int? RecordID, int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID, int? DeletedByUser)
        {
            this.RecordID = RecordID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.DeletedByUser = DeletedByUser;
            Mode = enMode.Update;
        }
         
        static public DataTable GetAllDeletedTestAppointments()
        {
            return clsDeletedTestAppointmentsData.GetAllDeletedTestAppointments();
        }
         

        static public clsDeletedTestAppointments FindDeletedTestAppointments(int? RecordID)
        {
            int? TestAppointmentID = null; int? TestTypeID = null; int? LocalDrivingLicenseApplicationID = null; DateTime? AppointmentDate = null; decimal? PaidFees = null; int? CreatedByUserID = null; bool? IsLocked = null; int? RetakeTestApplicationID = null; int? DeletedByUser = null;

            if (clsDeletedTestAppointmentsData.FindDeletedTestAppointments(ref RecordID, ref TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID, ref DeletedByUser))
            {
                return new clsDeletedTestAppointments(RecordID, TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID, DeletedByUser);
            }
            return null;
        }
          
    }
}
