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

namespace DVLD_DataAccess
{
    public class ApplicationType
    {
        static public DataTable GetAllApplicationsType()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_GetAllApplicationsType", connection);
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
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dt;

        }
        //================================================Update LocalDrivingLicenseApplications==================================================================================
        static public bool UpdateApplictionType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_UpdateApplictionType", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicationTypeTitle  ", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees  ", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeID  ", ApplicationTypeID);
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
        static public bool FindApplicationType(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_FindApplicationType", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicationTypeID  ", ApplicationTypeID);

            bool Result = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Result = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);

                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return (Result);

        }



    }
}
