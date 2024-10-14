using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        static public int AddApplications(int CreatedByUserID, int ApplicantPersonID, DateTime ApplicationDate, DateTime LastStatusDate, int ApplicationTypeID, int ApplicationStatus, float PaidFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_AddApplications", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            int ApplicationID = 0;
            SqlParameter parameter = new SqlParameter("@ApplicationID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
             try
            {
                connection.Open();
                command.ExecuteNonQuery();
                ApplicationID = (int)command.Parameters["@ApplicationID"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return ApplicationID; ;

        }

        //================================================Find Applications==================================================================================
        static public bool FindApplicationsByApplicationID(int ApplicationID, ref int ApplicationPersonID, ref int CreatedByUserID, ref DateTime ApplicationDate, ref DateTime LastStatusDate, ref int ApplicationTypeID, ref int ApplicationStatus, ref float PaidFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_FindApplicationsByApplicationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;

                    ApplicationPersonID = (int)Reader["ApplicantPersonID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    ApplicationTypeID = (int)Reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToInt16(Reader["ApplicationStatus"]);
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);

                }
                 Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }  
            return IsRead; 
        }
 
        static public DataTable GetAllAppliactions()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_GetAllAppliactions", connection);
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

        //================================================Update Applications==================================================================================
        static public bool UpdateUpdateApplicationByApplicationID(int ApplicationID, int CreatedByUserID, DateTime ApplicationDate, DateTime LastStatusDate, int ApplicationTypeID, int ApplicationStatus, float PaidFees)

        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

              
            SqlCommand command = new SqlCommand("SP_UpdateUpdateApplicationByApplicationID", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        //================================================Delete Applications==================================================================================
        static public bool DeleteUserByApplicationsID(int ApplicantPersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_DeleteUserByApplicationsID", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
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
        //=================================================================================================================================
        static public int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_GetActiveApplicationIDForLicenseClass", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            SqlParameter parameter = new SqlParameter("@ActiveApp", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
             try
            {
                connection.Open();
                command.ExecuteNonQuery();
                ActiveApplicationID = (int)command.Parameters["@ActiveApp"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
             

            return ActiveApplicationID;

        }

         //=================================================================================================================================
        static public bool UpdateStatus(int ApplicationID, DateTime LastStatusDate, int ApplicationStatus)

        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_UpdateStatus", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        //=================================================================================================================================


    }
}
