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
    public class clsDetainedLicenseData
    {
        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllDetainedLicenses", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        static public int AddDetainedLicenses(int LicenseID, DateTime DetainDate, DateTime ReleaseDate, float FineFees, int CreatedByUserID, short IsReleased, int ReleasedByUserID, int ReleaseApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_AddDetainedLicenses", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            if((ReleaseDate)!=DateTime.MinValue)
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            else
                command.Parameters.AddWithValue("@ReleaseDate", System.DBNull.Value);
            if ( (ReleasedByUserID )!=0)
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", System.DBNull.Value);

            if (  (ReleaseApplicationID )!=0)
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", System.DBNull.Value);

            SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);


            int DetainID = -1;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                DetainID = (int)command.Parameters["@ID"].Value;

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return DetainID ;


        }

        //================================================Find Detained==================================================================================
        static public bool FindByLicenseID(int LicenseID, ref int DetainID,ref DateTime DetainDate, ref DateTime ReleaseDate, ref float FineFees, ref int CreatedByUserID, ref short IsReleased, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_FindByLicenseID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;
                    DetainID  = Convert.ToInt32(Reader["DetainID"]);
                    LicenseID = Convert.ToInt32(Reader["LicenseID"]);
                    DetainDate = (DateTime)Reader["DetainDate"];
                    FineFees = Convert.ToSingle(Reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    IsReleased = Convert.ToInt16(Reader["IsReleased"]);
                    if(Reader["ReleaseDate"]!=System.DBNull.Value)
                    ReleaseDate = (DateTime)Reader["ReleaseDate"];
                    if (Reader["ReleasedByUserID"] != System.DBNull.Value)
                        ReleasedByUserID = Convert.ToInt32(Reader["ReleasedByUserID"]);
                    if (Reader["ReleaseApplicationID"] != System.DBNull.Value)
                        ReleaseApplicationID = Convert.ToInt32(Reader["ReleaseApplicationID"]);

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update Detained==================================================================================
        static public bool UpdateDetainedLicensesByDetainedID(int LicenseID, DateTime DetainDate, DateTime ReleaseDate, float FineFees, int CreatedByUserID, short IsReleased, int ReleasedByUserID, int ReleaseApplicationID)

        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_UpdateDetainedLicensesByDetainedID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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

        //================================================Delete Detained==================================================================================
        static public bool DeleteDetainedLicensesByDetainedID(int DetainID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_DeleteDetainedLicensesByDetainedID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DetainID", DetainID);
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

        //===============================================================================================================================
        static public bool IsLisenseDetained(int LicenseID )
            {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_IsLisenseDetained", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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







    }
}
