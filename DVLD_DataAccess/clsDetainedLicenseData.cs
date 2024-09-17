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

            string query = "select * from detainedLicenses_View order by IsReleased ,DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

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


            string quere = @" 
                                               INSERT INTO [dbo].[DetainedLicenses]
                                                          ([LicenseID]
                                                          ,[DetainDate]
                                                          ,[FineFees]
                                                          ,[CreatedByUserID]
                                                          ,[IsReleased]
                                                          ,[ReleaseDate]
                                                          ,[ReleasedByUserID]
                                                          ,[ReleaseApplicationID])
                                                    VALUES
                                                         (  
                                                          @LicenseID,
                                                          @DetainDate,   
                                                          @FineFees,  
                                                          @CreatedByUserID, 
                                                          @IsReleased,  
                                                          @ReleaseDate,  
                                                          @ReleasedByUserID, 
                                                          @ReleaseApplicationID )
                                                          select SCOPE_IDENTITY(); ";
            SqlCommand command = new SqlCommand(quere, connection);
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




            int DetainID = -1;
            try
            {
                connection.Open();
                object Result;
              Result=    command.ExecuteScalar();
                if (Result != null&&int.TryParse(Result.ToString(),out int ID))
                {
                    DetainID= ID;
                }

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


            string quere = @"select * from [dbo].[DetainedLicenses]
                                         WHERE LicenseID=@LicenseID          ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
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


            string quere = @"UPDATE [dbo].[DetainedLicenses]
                                                           SET      
                                                                       [DetainDate] = @DetainDate 
                                                                      ,[FineFees] = @FineFees 
                                                                      ,[CreatedByUserID] = @CreatedByUserID 
                                                                      ,[IsReleased] = @IsReleased 
                                                                      ,[ReleaseDate] = @ReleaseDate 
                                                                      ,[ReleasedByUserID] = @ReleasedByUserID 
                                                                      ,[ReleaseApplicationID] = @ReleaseApplicationID 
                                                               WHERE   LicenseID  =@LicenseID  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            bool IsSuccess = false;
            object Result;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    IsSuccess = (ID != 0);
                }
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


            string quere = @"
                                    DELETE FROM [dbo].[DetainedLicenses]
                                          WHERE DetainID=@DetainID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            bool IsSuccess = false;
            object Result;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    IsSuccess = (ID != 0);
                }
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


            string quere = @"select Found=1  from [dbo].[DetainedLicenses]
                                         WHERE LicenseID=@LicenseID  and IsReleased=0        ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                IsRead = Reader.HasRows;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }







    }
}
