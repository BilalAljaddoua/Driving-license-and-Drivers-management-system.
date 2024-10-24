using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsUpdatedTestAppointments
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
        public int? UpdatedByUser { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public clsUpdatedTestAppointments()
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
            this.UpdatedByUser = null;
            this.UpdatedDate = null;
            Mode = enMode.AddNew;
        }
        clsUpdatedTestAppointments(int? RecordID, int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID, int? UpdatedByUser, DateTime? UpdatedDate)
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
            this.UpdatedByUser = UpdatedByUser;
            this.UpdatedDate = UpdatedDate;
            Mode = enMode.Update;
        }
        private bool _AddUpdatedTestAppointments()
        {
            this.RecordID = clsUpdatedTestAppointmentsData.AddToUpdatedTestAppointmentsTable(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID, UpdatedByUser, UpdatedDate);
            return (this.RecordID != -1);
        }
        static public DataTable GetAllUpdatedTestAppointments()
        {
            return clsUpdatedTestAppointmentsData.GetAllUpdatedTestAppointments();
        }
        private bool _UpdateUpdatedTestAppointments()
        {
            bool IsSuccess = clsUpdatedTestAppointmentsData.UpdateUpdatedTestAppointmentsTable(RecordID, TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID, UpdatedByUser, UpdatedDate);
            return IsSuccess;
        }
        static public clsUpdatedTestAppointments FindUpdatedTestAppointments(int? RecordID)
        {
            int? TestAppointmentID = null; int? TestTypeID = null; int? LocalDrivingLicenseApplicationID = null; DateTime? AppointmentDate = null; decimal? PaidFees = null; int? CreatedByUserID = null; bool? IsLocked = null; int? RetakeTestApplicationID = null; int? UpdatedByUser = null; DateTime? UpdatedDate = null;

            if (clsUpdatedTestAppointmentsData.FindUpdatedTestAppointments(ref RecordID, ref TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID, ref UpdatedByUser, ref UpdatedDate))
            {
                return new clsUpdatedTestAppointments(RecordID, TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID, UpdatedByUser, UpdatedDate);
            }
            return null;
        }
        static bool DeleteUpdatedTestAppointments(int RecordID)
        {
            return clsUpdatedTestAppointmentsData.DeleteUpdatedTestAppointments(RecordID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddUpdatedTestAppointments())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:                    return _UpdateUpdatedTestAppointments();

            }

            return false;
        }
    }
}

