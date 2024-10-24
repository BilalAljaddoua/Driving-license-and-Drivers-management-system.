using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsUpdatedPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? RecordID { set; get; }
        public int? PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get
            {
                if(ThirdName == null)
                    return FirstName + " " + SecondName +" " + LastName;
                else return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public DateTime? DateOfBirth { set; get; }
        public byte? Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int? NationalityCountryID { set; get; }
        public string ImagePath { set; get; }
        public int? DeletedByUser { set; get; }
        public DateTime? DeleteDate { set; get; }
        public DateTime? DateOfUpdate { set; get; }
        public int? UpdatedBuUser { set; get; }
        public clsUpdatedPeople()
        {
            this.RecordID = null;
            this.PersonID = null;
            this.NationalNo = null;
            this.FirstName = null;
            this.SecondName = null;
            this.ThirdName = null;
            this.LastName = null;
            this.DateOfBirth = null;
            this.Gendor = null;
            this.Address = null;
            this.Phone = null;
            this.Email = null;
            this.NationalityCountryID = null;
            this.ImagePath = null;
            this.DeletedByUser = null;
            this.DeleteDate = null;
            this.DateOfUpdate = null;
            this.UpdatedBuUser = null;
            Mode = enMode.AddNew;
        }
        clsUpdatedPeople(int? RecordID, int? PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int? NationalityCountryID, string ImagePath, int? DeletedByUser, DateTime? DeleteDate, DateTime? DateOfUpdate, int? UpdatedBuUser)
        {
            this.RecordID = RecordID;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.DeletedByUser = DeletedByUser;
            this.DeleteDate = DeleteDate;
            this.DateOfUpdate = DateOfUpdate;
            this.UpdatedBuUser = UpdatedBuUser;
            Mode = enMode.Update;
        }
        private bool _AddUpdatedPeople()
        {
            this.RecordID = clsUpdatedPeopleData.AddToUpdatedPeopleTable(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath, DeletedByUser, DeleteDate, DateOfUpdate, UpdatedBuUser);
            return (this.RecordID != -1);
        }
        static public DataTable GetAllUpdatedPeople()
        {
            return clsUpdatedPeopleData.GetAllUpdatedPeople();
        }
        private bool _UpdateUpdatedPeople()
        {
            bool IsSuccess = clsUpdatedPeopleData.UpdateUpdatedPeopleTable(RecordID, PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath, DeletedByUser, DeleteDate, DateOfUpdate, UpdatedBuUser);
            return IsSuccess;
        }
        static public clsUpdatedPeople FindUpdatedPeople(int? RecordID)
        {
            int? PersonID = null; string NationalNo = null; string FirstName = null; string SecondName = null; string ThirdName = null; string LastName = null; DateTime? DateOfBirth = null; byte? Gendor = null; string Address = null; string Phone = null; string Email = null; int? NationalityCountryID = null; string ImagePath = null; int? DeletedByUser = null; DateTime? DeleteDate = null; DateTime? DateOfUpdate = null; int? UpdatedBuUser = null;

            if (clsUpdatedPeopleData.FindUpdatedPeople(ref RecordID, ref PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath, ref DeletedByUser, ref DeleteDate, ref DateOfUpdate, ref UpdatedBuUser))
            {
                return new clsUpdatedPeople(RecordID, PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath, DeletedByUser, DeleteDate, DateOfUpdate, UpdatedBuUser);
            }
            return null;
        }
        static bool DeleteUpdatedPeople(int RecordID)
        {
            return clsUpdatedPeopleData.DeleteUpdatedPeople(RecordID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddUpdatedPeople())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:                    return _UpdateUpdatedPeople();

            }

            return false;
        }
    }
}
