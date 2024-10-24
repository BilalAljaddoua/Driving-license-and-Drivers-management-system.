using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsUpdatedLocalDrivingLicenseApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? RecordID { set; get; }
        public int? LocalDrivingLicenseApplicationID { set; get; }
        public int? ApplicationID { set; get; }
        public int? LicenseClassID { set; get; }
        public int? UpdatedByUser { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public clsUpdatedLocalDrivingLicenseApplications()
        {
            this.RecordID = null;
            this.LocalDrivingLicenseApplicationID = null;
            this.ApplicationID = null;
            this.LicenseClassID = null;
            this.UpdatedByUser = null;
            this.UpdatedDate = null;
            Mode = enMode.AddNew;
        }
        clsUpdatedLocalDrivingLicenseApplications(int? RecordID, int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID, int? UpdatedByUser, DateTime? UpdatedDate)
        {
            this.RecordID = RecordID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.UpdatedByUser = UpdatedByUser;
            this.UpdatedDate = UpdatedDate;
            Mode = enMode.Update;
        }
        private bool _AddUpdatedLocalDrivingLicenseApplications()
        {
            this.RecordID = clsUpdatedLocalDrivingLicenseApplicationsData.AddToUpdatedLocalDrivingLicenseApplicationsTable(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, UpdatedByUser, UpdatedDate);
            return (this.RecordID != -1);
        }
        static public DataTable GetAllUpdatedLocalDrivingLicenseApplications()
        {
            return clsUpdatedLocalDrivingLicenseApplicationsData.GetAllUpdatedLocalDrivingLicenseApplications();
        }
        private bool _UpdateUpdatedLocalDrivingLicenseApplications()
        {
            bool IsSuccess = clsUpdatedLocalDrivingLicenseApplicationsData.UpdateUpdatedLocalDrivingLicenseApplicationsTable(RecordID, LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, UpdatedByUser, UpdatedDate);
            return IsSuccess;
        }
        static public clsUpdatedLocalDrivingLicenseApplications FindUpdatedLocalDrivingLicenseApplications(int? RecordID)
        {
            int? LocalDrivingLicenseApplicationID = null; int? ApplicationID = null; int? LicenseClassID = null; int? UpdatedByUser = null; DateTime? UpdatedDate = null;

            if (clsUpdatedLocalDrivingLicenseApplicationsData.FindUpdatedLocalDrivingLicenseApplications(ref RecordID, ref LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID, ref UpdatedByUser, ref UpdatedDate))
            {
                return new clsUpdatedLocalDrivingLicenseApplications(RecordID, LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, UpdatedByUser, UpdatedDate);
            }
            return null;
        }
        static bool DeleteUpdatedLocalDrivingLicenseApplications(int RecordID)
        {
            return clsUpdatedLocalDrivingLicenseApplicationsData.DeleteUpdatedLocalDrivingLicenseApplications(RecordID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddUpdatedLocalDrivingLicenseApplications())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:                    return _UpdateUpdatedLocalDrivingLicenseApplications();

            }

            return false;
        }
    }
}

