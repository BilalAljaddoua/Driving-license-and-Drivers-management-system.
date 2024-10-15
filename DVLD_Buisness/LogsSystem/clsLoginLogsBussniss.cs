using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsLoginLogs
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? RecordID { set; get; }
        public int? UserID { set; get; }
        public DateTime? DateOfLogin { set; get; }
        public DateTime? DateOfLoginOut { set; get; }
        public clsLoginLogs()
        {
            this.RecordID = null;
            this.UserID = null;
            this.DateOfLogin = null;
            this.DateOfLoginOut = null;
            Mode = enMode.AddNew;
        }
        clsLoginLogs(int? RecordID, int? UserID, DateTime? DateOfLogin, DateTime? DateOfLoginOut)
        {
            this.RecordID = RecordID;
            this.UserID = UserID;
            this.DateOfLogin = DateOfLogin;
            this.DateOfLoginOut = DateOfLoginOut;
            Mode = enMode.Update;
        }
        private bool _AddLoginLogs()
        {
            this.RecordID = clsLoginLogsData.AddToLoginLogsTable(UserID, DateOfLogin, DateOfLoginOut);
            return (this.RecordID != -1);
        }
        static public DataTable GetAllLoginLogs()
        {
            return clsLoginLogsData.GetAllLoginLogs();
        }
        private bool _UpdateLoginLogs()
        {
            bool IsSuccess = clsLoginLogsData.UpdateLoginLogsTable(RecordID, UserID, DateOfLogin, DateOfLoginOut);
            return IsSuccess;
        }
        static public clsLoginLogs FindLoginLogs(int? RecordID)
        {
            int? UserID = null; DateTime? DateOfLogin = null; DateTime? DateOfLoginOut = null;

            if (clsLoginLogsData.FindLoginLogs(ref RecordID, ref UserID, ref DateOfLogin, ref DateOfLoginOut))
            {
                return new clsLoginLogs(RecordID, UserID, DateOfLogin, DateOfLoginOut);
            }
            return null;
        }
        static bool DeleteLoginLogs(int RecordID)
        {
            return clsLoginLogsData.DeleteLoginLogs(RecordID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddLoginLogs())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:                    return _UpdateLoginLogs();

            }

            return false;
        }
    }
}
