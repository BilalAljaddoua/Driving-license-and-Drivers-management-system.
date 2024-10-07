using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestType
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }
        public enTestType ID { get; set; }

        enum enMode { enAddNew = 1, enUpdate = 2 };
        private enMode _Mode { get; set; }
        public enum enTestType { VisionTest = 1, WrittenTest = 2, PracticalTest = 3 }

        clsTestType(enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            this.ID = TestTypeID;
            this.Title = TestTypeTitle;
            this.Description = TestTypeDescription;
            this.Fees = TestTypeFees;
            _Mode = enMode.enUpdate;
        }

        clsTestType()
        {
            this.ID = enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = -1;
            this._Mode = enMode.enAddNew;

        }
        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.ID = (clsTestType.enTestType)clsTestTypeData.AddNewTestType(this.Title, this.Description, this.Fees);

            return (this.Title != "");
        }

        static public clsTestType FindTestType(enTestType TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = ""; float TestTypeFees = 0;
            if (clsTestTypeData.FindTestTypeByTestTypeID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            return null;

        }
        static public DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
        private bool _UpdateTestTypeFees()
        {
            return clsTestTypeData.UpdateByTestTypeID((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    {
                        return _AddNewTestType();

                    }
                case enMode.enUpdate:
                    {
                        return _UpdateTestTypeFees();


                    }
                default: { return false; }

            }

        }


    }
}
