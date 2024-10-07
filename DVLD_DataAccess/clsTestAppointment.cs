using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using System.Net;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {


        static public DataTable GetAllTestsByLDLAppAndTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SP_GetAllTestsByLDLAppAndTestType", connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
 
                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                } 
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dt;

        }


        //================================================Add New TestAppointment===================================================================================
        static public int AddTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, bool IsLocked, int CreatedByUserID, int RetakeTestApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);



            SqlCommand command = new SqlCommand("SP_AddTestAppointment", connection);
            command.CommandType=CommandType.StoredProcedure;
             command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if(RetakeTestApplicationID!=-1)
            command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);

            SqlParameter parameter = new SqlParameter("@TestAppointmentID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(parameter);
            int TestAppoinment = -1;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                TestAppoinment = (int)command.Parameters["@TestAppointmentID"].Value;

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return TestAppoinment;


        }

        //================================================Find TestAppointment==================================================================================
        static public bool FindTestAppointmentByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees, ref bool IsLocked, ref int CreatedByUserID, int RetakeTestApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
          
            SqlCommand command = new SqlCommand("SP_FindTestAppointmentByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    TestTypeID = (int)Reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)Reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)Reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    IsLocked = (bool)Reader["IsLocked"];
                    RetakeTestApplicationID = (int)Reader["RetakeTestApplicationID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        //================================================Update TestAppointment==================================================================================
        static public bool UpdateTestAppointmentByTestAppointmentID(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, bool IsLocked, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_UpdateTestAppointmentByTestAppointmentID", connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            command.CommandType = CommandType.StoredProcedure;

            bool IsSuccess = false;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
             IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsSuccess;

        }

        //================================================Delete TestAppointment==================================================================================

        static public bool DeleteTestAppointmentByTestAppointmentID(int TestAppointmentID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_DeleteTestAppointmentByTestAppointmentID", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsSuccess = false;
             try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return IsSuccess;


        }


        //========================================================================================================================================

        static public bool IsThereActiveAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return GetActiveAppointmentID(LocalDrivingLicenseApplicationID, TestTypeID) != -1;
        }
        static public int GetActiveAppointmentID(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int AppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_GetActiveAppointmentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
  

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    AppointmentID = (int)Reader["TestAppointmentID"];
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return AppointmentID;

        }

        static public bool LockAppintment(int TestAppointmentID, bool IsLocked)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_LockAppintment", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsSuccess = false;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsSuccess;
        }

        static public int GetTestID(int TestAppointmentID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_GetTestID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            SqlParameter parameter = new SqlParameter("@TestID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
                int TestID = -1;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                TestID = (int)command.Parameters["@TestID"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return TestID ;

        }

    }
}
