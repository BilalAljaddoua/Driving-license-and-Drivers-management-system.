using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsDeletedLocalDrivingLicenseApplications
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? RecordID { set; get; }
        public int? LocalDrivingLicenseApplicationID { set; get; }
        public int? ApplicationID { set; get; }
        public int? LicenseClassID { set; get; }
        public int? DeletedByUser { set; get; }


        public clsDeletedLocalDrivingLicenseApplications()
        {
            this.RecordID = null;
            this.LocalDrivingLicenseApplicationID = null;
            this.ApplicationID = null;
            this.LicenseClassID = null;
            this.DeletedByUser = null;
            Mode = enMode.AddNew;
        }

        clsDeletedLocalDrivingLicenseApplications(int? RecordID, int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID, int? DeletedByUser)
        {
            this.RecordID = RecordID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.DeletedByUser = DeletedByUser;
            Mode = enMode.Update;
        }
         
        static public DataTable GetAllDeletedLocalDrivingLicenseApplications()
        {
            return clsDeletedLocalDrivingLicenseApplicationsData.GetAllDeletedLocalDrivingLicenseApplications();
        }
         
        static public clsDeletedLocalDrivingLicenseApplications FindDeletedLocalDrivingLicenseApplications(int? RecordID)
        {
            int? LocalDrivingLicenseApplicationID = null; int? ApplicationID = null; int? LicenseClassID = null; int? DeletedByUser = null;

            if (clsDeletedLocalDrivingLicenseApplicationsData.FindDeletedLocalDrivingLicenseApplications(ref RecordID, ref LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID, ref DeletedByUser))
            {
                return new clsDeletedLocalDrivingLicenseApplications(RecordID, LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, DeletedByUser);
            }
            return null;
        }

  
    }
}
