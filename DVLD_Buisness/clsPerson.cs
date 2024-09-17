using System;
using System.Data;
using System.Xml.Linq;
using DVLD_DataAccess;


namespace DVLD_Buisness
{
    public class clsPerson
    {
        public enum enMode { enAddNew = 1, enUpdate = 2 }
        public string NationalNo { get; set; }
        public int PersonID { get; set; }
        public string FullName
        {

            get { return FirstName + "  " + SecondName + "  " + ThirdName + "  " + LastName + " "; }
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public clsCountry CountryInfo;
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public enMode Mode { get; set; }





        private clsPerson(string NationalNo, int PersonID, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.CountryInfo = clsCountry.FindByID(NationalityCountryID);
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.Mode = enMode.enUpdate;

        }



        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.enAddNew;
        }
        //=====================================================================================================================================================================
        static public DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }


        static public clsPerson Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            string NationalNo = ""; int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = clsPersonData.FindPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath);

            if (IsFound)
            {
                return new clsPerson(NationalNo, PersonID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            return null;
        }


        static public clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = clsPersonData.FindPersonByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath);

            if (IsFound)
            {
                return new clsPerson(NationalNo, PersonID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            return null;
        }
        //===================================================================================================================================================================
        static public bool DeletePerson(string NationalNo)
        {
            return clsPersonData.DeletePerson(NationalNo);
        }

        //=====================================================================================================================================================================
        private int _AddNewPerson()
        {
            return clsPersonData.AddPerson
                (NationalNo, FirstName, SecondName, ThirdName,
                LastName, DateOfBirth, Gendor, Address, Phone, Email,
                NationalityCountryID, ImagePath);
        }

        //=====================================================================================================================================================================
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName,
                ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
        }

        //=====================================================================================================================================================================

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.enAddNew:
                    {
                        Mode = enMode.enUpdate;

                        this.PersonID = _AddNewPerson();

                        return PersonID > 0;
                    }
                case enMode.enUpdate:
                    {
                        return _UpdatePerson();
                    }
                default: { return false; }







            }
        }
    }
}
