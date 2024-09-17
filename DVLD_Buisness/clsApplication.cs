using System;
using System.Data;
using DVLD_DataAccess;


namespace DVLD_Buisness
{
    public class clsApplication
    {

        private int _ApplicatoinID;
        public int ApplicationID
        {
            get { return _ApplicatoinID; }
            set { _ApplicatoinID = value; }
        }
        public int ApplicantPersonID { set; get; }
        public clsPerson PersonInfo { set; get; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }
        public enApplicationType ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public float PaidFees { get; set; }
        public DateTime LastStatusDate { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string  StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        {
                            return "New";
                        }
                    case enApplicationStatus.Cancelled:
                        {
                            return "Cancelled";
                        }
                    case enApplicationStatus.Completed:
                        {
                            return "Compleated";
                        }
                    default: { return "unknown"; }

                }

            }
        }

 

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public enum enApplicationType
        {
            NewLocalDrivingLicense = 1, RenewDrivingLicense = 2, ReplacementforLost = 3,
            ReplacementforDamaged = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        }

        public enum enMode { enAddnew = 1, enUpdate = 2 };
        public enMode Mode;

        public clsApplication()
        {
            Mode = enMode.enAddnew;
        }
        private clsApplication(int ApplicationID, int ApplicantPersonID, int CreatedByUserID, DateTime ApplicationDate,
            DateTime LastStatusDate, enApplicationType ApplicationTypeID, enApplicationStatus ApplicationStatus, float PaidFees)
        {
            this._ApplicatoinID = ApplicationID;
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.FindApplicationType(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.CreatedByUserID = CreatedByUserID;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            Mode = enMode.enUpdate;
        }
        private int _AddApplications()
        {
            return clsApplicationData.AddApplications(CreatedByUserID, ApplicantPersonID, ApplicationDate, LastStatusDate, (int)ApplicationTypeID, (int)ApplicationStatus, PaidFees);
        }
        static public clsApplication FindApplicationsByApplicationID(int ApplicationID)
        {
            int CreatedByUserID = 0; DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            int ApplicationTypeID = 0, ApplicationStatus = 0; float PaidFees = 0;
            int ApplicantPersonID = 0;

            if (clsApplicationData.FindApplicationsByApplicationID(ApplicationID, ref ApplicantPersonID, ref CreatedByUserID, ref ApplicationDate
                                                                                                           , ref LastStatusDate, ref ApplicationTypeID, ref ApplicationStatus, ref PaidFees))
            {
                return new clsApplication((int)ApplicationID, ApplicantPersonID, CreatedByUserID, ApplicationDate, LastStatusDate,
                   (enApplicationType)ApplicationTypeID, (enApplicationStatus)ApplicationStatus, PaidFees);
            }
            return null;
        }
        private bool _UpdateByApplicantID()
        {
            return clsApplicationData.UpdateUpdateApplicationByApplicationID(ApplicationID, CreatedByUserID, ApplicationDate, LastStatusDate, (int)ApplicationTypeID, (int)ApplicationStatus, PaidFees);
        }
        static public bool DeleteUserByApplicationID(int ApplicantPersonID)
        {
            return clsApplicationData.DeleteUserByApplicationsID((int)ApplicantPersonID);
        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, DateTime.Now, 2);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, DateTime.Now, 3);
        }

        static public int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID,(int)ApplicationTypeID,LicenseClassID);
        }
        public bool Save()
        {
            switch (Mode)
            {

                case enMode.enAddnew:
                    {

                        _ApplicatoinID = _AddApplications();
                        return _ApplicatoinID != -1;
                    }
                case enMode.enUpdate:
                    {
                        return _UpdateByApplicantID();
                    }
                default: { return false; }



            }
        }
    }
}
