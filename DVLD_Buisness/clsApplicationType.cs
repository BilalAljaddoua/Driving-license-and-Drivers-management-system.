using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsApplicationType
    {
        public clsApplication.enApplicationType ApplicationTypeID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }
       public enum enMode { AddNew=1,Update=2}
        private enMode _Mode = enMode.Update;
        //_Mode

        //Public c;sApplicationType
        private clsApplicationType(clsApplication.enApplicationType ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.Title = ApplicationTypeTitle;
            this.Fees = ApplicationFees;

        }


        private bool _UpdateApplictionType()
        {
            return ApplicationType.UpdateApplictionType((int)ApplicationTypeID, Title, Fees);
        }
        static public clsApplicationType FindApplicationType(clsApplication.enApplicationType ApplicationTypeID)
        {
            string ApplicationTypeTitle = ""; float ApplicationFees = 0;
            if (ApplicationType.FindApplicationType((int)ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            return null;
        }
        static public DataTable GetApplicationType()
        {
           return  ApplicationType.GetAllApplicationsType();
 
        }





        public bool  Save()
        {
            switch (_Mode)
            {
                case enMode.Update:
                    {
                        return _UpdateApplictionType();
                    }
                case enMode.AddNew:
                    {
                        return false;
                    }
                    default: {return false;}
            }
        }


    }
}
