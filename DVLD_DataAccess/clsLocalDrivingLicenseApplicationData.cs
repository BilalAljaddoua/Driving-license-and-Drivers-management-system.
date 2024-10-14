using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        static public DataTable GetAllLocalDrivingLicenseApplications()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_GetAllLocalDrivingLicenseApplications", connection);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.HasRows)
                {
                    dt.Load(Reader);
                }

                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dt;

        }
        //================================================Add New LocalDrivingLicenseApplications===================================================================================
        static public int AddLocalDrivingLicenseApplications(int ApplicationID, int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            SqlCommand command = new SqlCommand("SP_AddLocalDrivingLicenseApplications", connection);
            command.CommandType= CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            SqlParameter parameter = new SqlParameter("@LocalDrivingLicenseApplicationID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
             
            int LocalDrivingLicenseApplicationID = -1;
            try
            {
                connection.Open();
                 command.ExecuteNonQuery();
                LocalDrivingLicenseApplicationID = (int)command.Parameters["@LocalDrivingLicenseApplicationID"].Value;

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;


        }
        //================================================Find LocalDrivingLicenseApplications==================================================================================
        static public bool FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_FindByLocalDrivingLicenseApplicationID", connection);
            command.CommandType = CommandType.StoredProcedure; 
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;
                    ApplicationID = (int)Reader["ApplicationID"];
                    LocalDrivingLicenseApplicationID = (int)Reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)Reader["LicenseClassID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindByApplicationID(int ApplicationID,ref int LocalDrivingLicenseApplicationID,   ref int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_FindByApplicationID", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID); 


            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;
                    ApplicationID = (int)Reader["ApplicationID"];
                    LocalDrivingLicenseApplicationID = (int)Reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)Reader["LicenseClassID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        //================================================Update LocalDrivingLicenseApplications==================================================================================
        static public bool UpdateLDLAByLDLA_ID(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_UpdateLDLAByLDLA_ID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        //================================================Delete LocalDrivingLicenseApplications==================================================================================
        static public bool DeleteByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID,int UserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_DeleteByLocalDrivingLicenseApplicationID", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@UserID", UserID);
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
        //==============================================================================================================================
        static public bool DosePassTesttype(  int LocalDrivingLicenseApplicationID,   int TestTypeID)
        {
              
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_DosePassTesttype", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            bool IsSuccess = false;
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;               
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return IsSuccess;

        }
        static public int HowNumberTrials(int LDLApp, int TestTypeID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_HowNumberTrials", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LDLApp", LDLApp);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            SqlParameter parameter = new SqlParameter("@CountTrial", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);



            int CountTrial = -1;
             try
            {
                connection.Open();
                command.ExecuteNonQuery();
                CountTrial = (int)command.Parameters["@CountTrial"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return CountTrial; ;


        }
        static public bool IsThereActiveAppintmentWithTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_IsThereActiveAppintmentWithTest", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);

            bool IsFound = false;

            try
            {
                connection.Open();
                 command.ExecuteNonQuery();
                IsFound = (bool)command.Parameters["@IsSuccess"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsFound;

        }
        static public int GetActiveLicenseID(int LDLApp,int LicenseClass )
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_GetActiveLicenseID", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LDLApp", LDLApp);
             command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            SqlParameter parameter = new SqlParameter("@LicenseID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            }; 
            command.Parameters.Add(parameter);


            int LiceneID = -1;
             try
            {
                connection.Open();

                    command.ExecuteNonQuery();
                    LiceneID=(int)command.Parameters["@LicenseID"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return LiceneID; ;


        }
       
    
    
    }
}