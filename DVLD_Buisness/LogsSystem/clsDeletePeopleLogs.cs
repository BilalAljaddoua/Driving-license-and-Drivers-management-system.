using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsDeletedPeople
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
        public DateTime? DateOfBirth { set; get; }
        public byte? Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int? NationalityCountryID { set; get; }
        public string ImagePath { set; get; }
        public int? DeletedByUser { set; get; }
        public clsDeletedPeople()
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
            Mode = enMode.AddNew;
        }
        clsDeletedPeople(int? RecordID, int? PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int? NationalityCountryID, string ImagePath, int? DeletedByUser)
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
            Mode = enMode.Update;
        }
         

        static public DataTable GetAllDeletedPeople()
        {
            return clsDeletedPeopleData.GetAllDeletedPeople();
        }
        static public clsDeletedPeople FindDeletedPeople(int? RecordID)
        {
            int? PersonID = null; string NationalNo = null; string FirstName = null; string SecondName = null; string ThirdName = null; string LastName = null; DateTime? DateOfBirth = null; byte? Gendor = null; string Address = null; string Phone = null; string Email = null; int? NationalityCountryID = null; string ImagePath = null; int? DeletedByUser = null;

            if (clsDeletedPeopleData.FindDeletedPeople(ref RecordID, ref PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath, ref DeletedByUser))
            {
                return new clsDeletedPeople(RecordID, PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath, DeletedByUser);
            }
            return null;
        } 
     }
}
