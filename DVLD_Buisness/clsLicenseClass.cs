using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsLicenseClass
    {

        public int LicenseClassID { get; set; }
        public int MinimumAllowedAge { get; set; }
        public string ClassDescription { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }
        public string ClassName { get; set; }
        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode { get; set; }


        clsLicenseClass() { }

        clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.ClassDescription = ClassDescription;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            this.ClassName = ClassName;
            _Mode = enMode.enUpdate;

        }



        static public DataTable GetAllLicenseClass()
        {
            return clsLicenseClassData.GetAllLicenseClass();
        }
        static public clsLicenseClass Find(int LicenseClassID)
        {
            string ClassDescription = "", ClassName = ""; int MinimumAllowedAge = 0, DefaultValidityLength = 0; float ClassFees = 0;
            if (clsLicenseClassData.FindLicenseClassesByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            return null;
        }

        static public clsLicenseClass Find(string ClassName)
        {
            string ClassDescription = ""; int MinimumAllowedAge = 0, DefaultValidityLength = 0; float ClassFees = 0;
            int LicenseClassID = 0;

            if (clsLicenseClassData.FindLicenseClassesByName(ClassName, ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            return null;
        }


        static private bool _UpdateFeesByLicenseClassesID(int LicenseClassID, int ClassFees)
        {
            return clsLicenseClassData.UpdateFeesByLicenseClassesID(LicenseClassID, ClassFees);
        }
        static private bool _UpdateFeesByLicenseClassesName(string ClassName, float ClassFees)
        {
            return clsLicenseClassData.UpdateFeesByLicenseClassesName(ClassName, ClassFees);

        }



        public bool Save()
        {
            return _UpdateFeesByLicenseClassesName(ClassName, ClassFees);
        }


    }
}
