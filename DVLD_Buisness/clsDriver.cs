using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsDriver
    {
        enum enMode { AddNew = 1, Update = 2 };
        public int PersonID { get; set; }
        public int DriverID { get; set; }
        public int CreatedByUserID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public DateTime CreatedDate { get; set; }

        private enMode Mode;

        public clsDriver()
        {
            Mode = enMode.AddNew;
        }
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }

        static public DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }
        static public DataTable GetLocalLicenses(int DriverID)
        {
            return clsDriverData.GetLocalLicense(DriverID);
        }
        static public DataTable GetInternationalLicenses(int DriverID)
        {
            return clsDriverData.GetInternationalLicenses(DriverID);
        }
        public static clsDriver FindDriverByDriverID(int DriverID)
        {
            int PersonID = 0, CreatedByUserID = 0; DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.FindDriverByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }
        public static clsDriver FindDriverByPersonID(int PersonID)
        {
            int DriverID = 0, CreatedByUserID = 0; DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }
        public static clsDriver FindDriverByNationalNo(string NationalNo)
        {
            int DriverID = 0, PersonID = 0, CreatedByUserID = 0; DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.FindDriverNationalNo(NationalNo, ref DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }

        private bool _AddDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = clsDriverData.AddDriver(PersonID, CreatedByUserID, CreatedDate);
            return DriverID != -1;
        }
        private bool _UpDateDriverByPersonID(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            return clsDriverData.UpdateDriverByPersonID(PersonID, CreatedByUserID, CreatedDate);
        }
        private bool _UpByDriverID(int DriverID, int CreatedByUserID, DateTime CreatedDate)
        {
            return clsDriverData.UpdateDriverByDriverID(DriverID, CreatedByUserID, CreatedDate);
        }
        public static bool DeleteByPersonID(int PersonID)
        {
            return clsDriverData.DeleteDriverByPersonID(PersonID);
        }
        public static bool DeleteByDriverID(int DriverID)
        {
            return clsDriverData.DeleteDriverByPersonID(DriverID);
        }
        public int GetPersonID()
        {
            return clsDriverData.GetPersonID(DriverID);
        }
     static public int GetPersonID(int DriverID)
        {
            return clsDriverData.GetPersonID(DriverID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if( _AddDriver(PersonID, CreatedByUserID, CreatedDate))
                        {
                        Mode = enMode.Update;
                            return true;
                        }
                        else { return false; }
                    }
                case enMode.Update:
                    {
                        return _UpDateDriverByPersonID(PersonID, CreatedByUserID, CreatedDate);

                    }

                default: { return false; }

            }

        }

    }
}
