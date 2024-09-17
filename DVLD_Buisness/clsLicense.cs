using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;
using DVLD_DataAccess;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Buisness
{
    public class clsLicense 
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public bool IsDetained { get { return _IsLicenseDetained(); } }
        public string StringIssueReason()
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    {
                        return "FirstTime";
                    }
                case enIssueReason.Renew:
                    {
                        return "Renew";
                    }
                case enIssueReason.ReplacementforDamaged:
                    {
                        return "Replacement for Damaged";
                    }

                case enIssueReason.ReplacementforLost:
                    {
                        return "Replacement for Lost";
                    }
                default: { return "UnKnown"; }
            }
        }
        private enum enMode { enAddNew = 1, enUpdate = 2 };
        private enMode _Mode { get; set; }    
        public enum enIssueReason { FirstTime = 1, Renew = 2 , ReplacementforDamaged=3, ReplacementforLost =4};
         public clsLicenseClass LicenseClassInfo { get; set; }
        public clsPerson PersonInfo { get; set; }
        public clsDriver DriverInfo { get; set; }

        public clsDetainedLicense DetainInfo { get; set; }
        public clsLicense() { _Mode = enMode.enAddNew; }

        private clsLicense(int LicenseID, int applicationID, int driverID, float paidFees, int createdByUserID, int licenseClassID, DateTime issueDate, DateTime expirationDate, string notes, bool isActive, enIssueReason issueReason)
        {
            this.LicenseID = LicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            IsActive = isActive;
            IssueReason =  issueReason;
            LicenseClassInfo = clsLicenseClass.Find(licenseClassID);
            DriverInfo = clsDriver.FindDriverByDriverID(DriverID);
            PersonInfo = clsPerson.Find(DriverInfo.PersonID);
            DetainInfo=clsDetainedLicense.FindByLicenseID(this.LicenseID);  
            _Mode = enMode.enUpdate;
        }

        static public DataTable GetAllLocalLicenseForDriver(int DriverID)
        {
            return clsLicenseData.GetAllLocalLicenseForDriver(DriverID);
        }
        private bool _AddLicenses()
        {
            LicenseID = clsLicenseData.AddLicenses(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (int)IssueReason, CreatedByUserID);
            return LicenseID != -1;
        }

        static public clsLicense FindLicensesByLicenseID(int LicenseID)
        {

            int ApplicationID = 0, DriverID = 0; int LicenseClassID = -1; DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now; string Notes = ""; float PaidFees = 0; bool IsActive = false; int IssueReason = -1; int CreatedByUserID = 0;


            if (clsLicenseData.FindLicensesByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, PaidFees, CreatedByUserID, LicenseClassID, IssueDate, ExpirationDate, Notes, IsActive, (enIssueReason)IssueReason);
            }
            return null;

        }
        static public clsLicense FindLicensesByApplicationID(int ApplicationID)
        {

            int LicenseID = 0, DriverID = 0; int LicenseClassID = -1; DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now; string Notes = ""; float PaidFees = 0; bool IsActive = false; int IssueReason = -1; int CreatedByUserID = 0;


            if (clsLicenseData.FindLicensesByApplicationID(ApplicationID, ref LicenseID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, PaidFees, CreatedByUserID, LicenseClassID, IssueDate, ExpirationDate, Notes, IsActive, (enIssueReason)IssueReason);
            }
            return null;

        }
        private bool _UpdateUserByLicenseID()
        {

            return clsLicenseData.UpdateUserByLicenseID(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive,  (int)IssueReason, CreatedByUserID);


        }
        static public bool DeleteLicenseByLicenseID(int LicenseID)
        {
            return clsLicenseData.DeleteLicenseByLicenseID(LicenseID);
        }
        static public bool DeleteLicenseByApplicationID(int ApplicationID)
        {
            return clsLicenseData.DeleteLicenseByApplicationID(ApplicationID);
        }
        static public bool IsLicenseActiveByLicenseID(int LicenseID)
        {
            return clsLicenseData.IsLicenseActiveByLicenseID(LicenseID);
        }
        static public int IsLicenseExists(int ApplicantPersonID, int LicenseClassID)
        {
            return clsLicenseData.IsLicenseExists(ApplicantPersonID, LicenseClassID);
        }
        private bool _IsLicenseDetained()
        {
            return clsDetainedLicenseData.IsLisenseDetained(LicenseID);
        }
        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate <DateTime.Now);
        }
        public bool DeactivLucense()
        {
        return  clsLicenseData.DeActivatLicense(this.LicenseID);
        }
        private clsApplication.enApplicationType GetApplicationType(enIssueReason issueReason)
        {
            if (issueReason == enIssueReason.ReplacementforDamaged)
                return clsApplication.enApplicationType.ReplacementforDamaged;
            else
                return clsApplication.enApplicationType.ReplacementforLost;

        }
        public clsLicense RenewLicense(string Notes,int CreatedByUser)
        {
            clsApplication NewApplicstion = new clsApplication();
            NewApplicstion.ApplicantPersonID = this.PersonInfo.PersonID;
            NewApplicstion.ApplicationTypeID = clsApplication.enApplicationType.RenewDrivingLicense;
            NewApplicstion.ApplicationDate = DateTime.Now;
            NewApplicstion.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewApplicstion.CreatedByUserID = CreatedByUser;
            NewApplicstion.PaidFees = clsApplicationType.FindApplicationType(clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            NewApplicstion.LastStatusDate = DateTime.Now;
            if (!NewApplicstion.Save())
            {
                return null;
            }


            clsLicense NewLicense = new clsLicense();

            NewLicense.PaidFees = clsLicenseClass.Find(this.LicenseClassID).ClassFees;
            NewLicense.Notes = Notes;
            NewLicense.CreatedByUserID = CreatedByUser;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.DriverID = this.DriverID;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(this.LicenseClassID).DefaultValidityLength);
            NewLicense.IsActive = true;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.ApplicationID = NewApplicstion.ApplicationID;
            NewLicense.PersonInfo = this.PersonInfo;
         
            if (!NewLicense.Save())
            {
                return null;

             }
 
           DeactivLucense();

          return NewLicense;
        }
        public int DetainedLicense(float Fine,int UserID)
        {
            clsDetainedLicense NewDetained= new clsDetainedLicense();

            NewDetained.FineFees = Fine;
            NewDetained.DetainDate = DateTime.Now;
            NewDetained.IsReleased = 0;
            NewDetained.CreatedByUserID= UserID;
            NewDetained.LicenseID = this.LicenseID;
             if (!NewDetained.Save())
            {
               return -1;
            }

            return NewDetained.DetainID;
        }
        public bool ReleseLicense( float PaidFees, int UserID,ref int ApplicationID)
        {
            clsDetainedLicense NewDetained = clsDetainedLicense.FindByLicenseID(LicenseID );
            if (NewDetained == null)
            { return false; }


            clsApplication NewApplication= new clsApplication();

            NewApplication.ApplicationDate = DateTime.Now;
            NewApplication.ApplicantPersonID = this.PersonInfo.PersonID;
            NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewApplication.ApplicationTypeID = clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            NewApplication.PaidFees = PaidFees;
            NewApplication.CreatedByUserID = UserID;
            NewApplication.LastStatusDate = DateTime.Now;
            NewApplication.PersonInfo = PersonInfo;
 
            if(! NewApplication.Save()) 
            { return false; }

            NewDetained.ReleaseApplicationID = NewApplication.ApplicationID;
            NewDetained.IsReleased = 1;
            NewDetained.ReleaseDate = DateTime.Now;
            NewDetained.ReleasedByUserID = UserID;
            NewDetained.IsReleased = 1;


            ApplicationID = NewApplication.ApplicationID;


            if (NewDetained.Save())
            {
                return true;
            }

            return false;
        }

        public clsLicense ReplaceForDamagedOrLost(  clsLicense.enIssueReason issueReason, int CreatedByUser)
        {
            clsApplication NewApplicstion = new clsApplication();
            NewApplicstion.ApplicantPersonID = this.PersonInfo.PersonID;
            NewApplicstion.ApplicationTypeID = GetApplicationType(issueReason);
            NewApplicstion.ApplicationDate = DateTime.Now;
            NewApplicstion.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewApplicstion.CreatedByUserID = CreatedByUser;
            NewApplicstion.PaidFees = clsApplicationType.FindApplicationType(GetApplicationType(issueReason)).Fees;
            NewApplicstion.LastStatusDate = DateTime.Now;
            if (!NewApplicstion.Save())
            {
                return null;
            }


            clsLicense NewLicense = new clsLicense();

            NewLicense.PaidFees = 0;
            NewLicense.Notes = this.Notes;
            NewLicense.CreatedByUserID = CreatedByUser;
            NewLicense.IssueReason = issueReason;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.DriverID = this.DriverID;
            NewLicense.ExpirationDate =this.ExpirationDate;
            NewLicense.IsActive = true;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.ApplicationID = NewApplicstion.ApplicationID;
            NewLicense.PersonInfo = this.PersonInfo;

            if (!NewLicense.Save())
            {
                return null;

            }

            DeactivLucense();

            return NewLicense;
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    {
                        return _AddLicenses();
                    }
                case enMode.enUpdate:
                    {
                        return _UpdateUserByLicenseID();
                    }
                default: { return false; }
            }


        }









    }
}
