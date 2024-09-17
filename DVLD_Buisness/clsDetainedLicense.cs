using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsDetainedLicense
    {


        public int LicenseID { get; set; }
        public int DetainID {  get; set; }
        public DateTime DetainDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public short IsReleased { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public enum enMode { enAddNew = 1, enUpdate = 2 }

        public enMode _Mode { get; set; }


        private clsDetainedLicense(int DetainID,int LicenseID, DateTime DetainDate, DateTime ReleaseDate, float FineFees, int CreatedByUserID, short IsReleased, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.ReleaseDate = ReleaseDate;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleasedByUserID = ReleasedByUserID;
            _Mode = enMode.enUpdate;
        }
        public clsDetainedLicense() { _Mode = enMode.enAddNew; }


        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();

        }

        public bool _AddDetainedLicenses ()
        {
            
             this.DetainID= clsDetainedLicenseData.AddDetainedLicenses(LicenseID, DetainDate, ReleaseDate, FineFees, CreatedByUserID, IsReleased, ReleasedByUserID, ReleaseApplicationID);
            return this.DetainID!=-1;
        }
        static public clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now; float FineFees = 0;int CreatedByUserID = 0,DetainID=0; short IsReleased=0; int ReleasedByUserID = 0, ReleaseApplicationID = 0;
            if (clsDetainedLicenseData.FindByLicenseID(LicenseID,ref DetainID, ref DetainDate, ref ReleaseDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, ReleaseDate, FineFees, CreatedByUserID, IsReleased, ReleasedByUserID, ReleaseApplicationID);
            }

            return null;
        }
          public bool _UpdateDetainedLicensesByDetainedID()
        {
            return clsDetainedLicenseData.UpdateDetainedLicensesByDetainedID(LicenseID, DetainDate, ReleaseDate, FineFees, CreatedByUserID, IsReleased, ReleasedByUserID, ReleaseApplicationID);
        }
 

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    {
                         if( _AddDetainedLicenses())
                        {
                            this._Mode = enMode.enUpdate;
                            return true;
                        }
                         return false;
                     }
                case enMode.enUpdate:
                    {
                        return _UpdateDetainedLicensesByDetainedID();
 
                    }
                default: return false;
            }
        }
    }
}

    

