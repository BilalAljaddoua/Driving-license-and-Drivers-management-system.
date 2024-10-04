using System;
using System.Data;
using System.Runtime.InteropServices;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsUser
    {

        public enum enMode { enAddNew = 1, enUpdate = 2 }

        public int PersonID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public clsPerson PersonInfo;
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string NewUsername { get; set; }

        public enMode Mode { get; set; }


        public clsUser()
        {
            Mode = enMode.enAddNew;
        }

        private clsUser(int PersonID, int UserID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonID = PersonID;
            this.Mode = enMode.enUpdate;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static clsUser FindUserByUserName(string UserName)
        {
            int UserID = 0;
            string Password = ""; int PersonID = 0; bool IsActive = false;
            if (clsUserData.FindUserByUserName(UserName, ref UserID, ref PersonID, ref Password, ref IsActive))
            {
                return new clsUser(PersonID, UserID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = 0;
            string Password = "", UserName = ""; bool IsActive = false;
            if (clsUserData.FindUserByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(PersonID, UserID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = 0;
            string Password = "", UserName = ""; bool IsActive = false;
            if (clsUserData.FindUserByUserID(UserID, ref UserName, ref PersonID, ref Password, ref IsActive))
            {
                return new clsUser(PersonID, UserID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindUserByUserNameAndPassword(string Username, string Password)
        {
            int PersonID = -1;
            bool IsActive = false; int UserID = -1;
            if (clsUserData.FindByUserNameAndPassword(Username, Password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(PersonID, UserID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        private bool _AddUser(int PersonID, string UserName, string Password, bool IsActive)
        {
             UserID= clsUserData.AddUser( UserName,PersonID, Password, IsActive);
            return UserID != -1;
        }
        static public bool DeleteUserByUserName(string UserName)
        {
            return clsUserData.DeleteUserByUserName(UserName);
        }
        static public bool DeleteUserByPersonID(int PersonID)
        {
            return clsUserData.DeleteUserByPersonID(PersonID);
        }

        private bool _UpdateUserByUserName(string UserName, string NewUsername, string Password, bool IsActive)
        {
            return clsUserData.UpdateUserByUserName(UserName, NewUsername, Password, IsActive);
        }
        private bool _UpdateUserByUserID(int UserID, string UserName, string Password, bool IsActive)
        {
            return clsUserData.UpdateUserByUserID(UserID, UserName, Password, IsActive);
        }

        static public bool IsUserExistWithPerson(int PersonID)
        {
            return clsUserData.IsUserExist(PersonID);
        }





        public bool Save()
        {
            switch (Mode)
            {
                case enMode.enAddNew:
                    {
                        Mode = enMode.enUpdate;
                        return _AddUser(PersonID, UserName, Password, IsActive);

                    }
                case enMode.enUpdate:
                    {
                        return _UpdateUserByUserID(PersonID, UserName, Password, IsActive);
                    }
                default: { return false; }

            }
        }




    }
}
