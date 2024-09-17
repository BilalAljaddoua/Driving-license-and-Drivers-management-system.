using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsInternationalLicense : clsApplication
    {

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsActive { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
         public clsDriver DriverInfo { get; set; }
        enum enMode { enAddNew = 1, enUpdate = 2 };
        private enMode Mode { get; set; }


        public clsInternationalLicense()
        {
            Mode = enMode.enAddNew;
        }

        static public DataTable GetAllInternationalLicenseForDriver(int DriverID)
        {
            return clsInternationalLicenseData.GetAllInternationalLicenseForDriver(DriverID);
        }
        static public DataTable GetAllInternationalLicense( )
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();
        }

        private clsInternationalLicense(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {

            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.LastStatusDate = LastStatusDate;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationStatus = ApplicationStatus;
            base.PaidFees = PaidFees;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            DriverInfo =clsDriver.FindDriverByDriverID(DriverID);

            


            Mode = enMode.enUpdate;
        }

        private bool _AddInternationalLicenses()
        {
            int ID;
            ID= clsInternationalLicenseData.AddInternationalLicenses(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID); 
            this.InternationalLicenseID = ID;
            return ID != -1;
        }
        static public clsInternationalLicense FindByInternationalLicenseID(int InternationalLicenseID)
        {

            int ApplicationID = 0, DriverID = 0, IssuedUsingLocalLicenseID = 0, CreatedByUserID = 0; DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now; bool IsActive = false;

            if (clsInternationalLicenseData.FindByInternationalLicenseID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.FindApplicationsByApplicationID(ApplicationID);


                return new clsInternationalLicense(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                     Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);
            }
            return null;

        }  
        static public clsInternationalLicense FindByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {

            int ApplicationID = 0, DriverID = 0,  InternationalLicenseID= 0, CreatedByUserID = 0; DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now; bool IsActive = false;

            if (clsInternationalLicenseData.FindByLocalLicenseID(IssuedUsingLocalLicenseID,ref InternationalLicenseID, ref ApplicationID, ref DriverID,  ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.FindApplicationsByApplicationID(ApplicationID);


                return new clsInternationalLicense(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                     Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);
            }
            return null;

        }
        private bool _UpdateByInternationalLicenseID()
        {
            return clsInternationalLicenseData.UpdateInternationalLicenseByDriverID(DriverID, ApplicationID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }
        static public bool _DeleteDriverByInternationalLicenseID(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.DeleteDriverByInternationalLicenseID(InternationalLicenseID);
        }
        static public bool IsInternationalLicensesActiveByInternationalLicenseID(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.IsInternationalLicensesActiveByInternationalLicenseID(InternationalLicenseID);
        }
 



        public bool Save()
        {         
            
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.enAddNew:
                    {
                        return _AddInternationalLicenses();
                    }
                case enMode.enUpdate:
                    {
                        return _UpdateByInternationalLicenseID();
                    }
                default: { return false; }
            }



        }

    }
}
