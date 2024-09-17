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


            string quere = @" INSERT INTO [dbo].[Applications]
                                                                             ([ApplicantPersonID]
                                                                             ,[ApplicationDate]
                                                                             ,[ApplicationTypeID]
                                                                             ,[ApplicationStatus]
                                                                             ,[LastStatusDate]
                                                                             ,[PaidFees]
                                                                             ,[CreatedByUserID])
                                                                       VALUES
                                                                             (@ApplicantPersonID
                                                                             ,@ApplicationDate 
                                                                             ,@ApplicationTypeID 
                                                                             ,@ApplicationStatus 
                                                                             ,@LastStatusDate 
                                                                             ,@PaidFees 
                                                                             ,@CreatedByUserID ) ;select  SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            int ApplicationID = 0;
            object Result;
            try
            {
                connection.Open();

                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int AppID))
                {
                    ApplicationID = AppID;
                }

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


            string quere = @"SELECT [ApplicationID]
      ,[ApplicantPersonID]
      ,[ApplicationDate]
      ,[ApplicationTypeID]
      ,[ApplicationStatus]
      ,[LastStatusDate]
      ,[PaidFees]
      ,[CreatedByUserID]
  FROM [dbo].[Applications] where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
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


            string quere = @"SELECT * from  Applications  ";

            SqlCommand command = new SqlCommand(quere, connection);
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


            string quere = @"
                UPDATE [dbo].[Applications]
                         SET       [ApplicationDate] = @ApplicationDate 
                                     ,[ApplicationTypeID] = @ApplicationTypeID 
                                     ,[ApplicationStatus] = @ApplicationStatus 
                                     ,[LastStatusDate] = @LastStatusDate 
                                     ,[PaidFees] = @PaidFees 
                                     ,[CreatedByUserID] = @CreatedByUserID 
                        WHERE  ApplicationID=@ApplicationID  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            int Result = 0;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return Result != 0;

        }

        //================================================Delete Applications==================================================================================
        static public bool DeleteUserByApplicationsID(int ApplicantPersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" DELETE FROM [dbo].[Applications]
                                          WHERE ApplicantPersonID=@ApplicantPersonID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

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
        //=================================================================================================================================
        static public int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT  LocalDrivingLicenseApplications.ApplicationID  FROM     Applications INNER JOIN
                  ApplicationTypes ON Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID INNER JOIN
                  People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN
                  LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                  LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
                 WHERE  ((Applications.ApplicantPersonID =@PersonID) AND (Applications.ApplicationTypeID = @ApplicationTypeID) AND (LocalDrivingLicenseApplications.LicenseClassID=@LicenseClassID)And (Applications.ApplicationStatus=1 ))";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            bool IsRead = false;
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    ActiveApplicationID = Convert.ToInt16(Reader["ApplicationID"]);
                }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return ActiveApplicationID;

        }
        static public bool IsApplicationExist(int ApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" select Found=1 FROM [dbo].[Applications]
                                          WHERE ApplicantPersonID=@ApplicantPersonID  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", ApplicationID);

            bool IsRead = false;
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();


                IsRead = Reader.HasRows;

                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"   select ActiveApplicationID=ApplicationID from dbo.Applications
                                              where ApplicantPersonID=@PersonID and ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object Result;
                Result = command.ExecuteScalar();

                if ((Result != null) && (int.TryParse(Result.ToString(), out int ID)))
                {
                    ApplicationID = ID;
                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return ApplicationID;

        }
        static bool DoesPersonHasActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(PersonID, ApplicationTypeID) != -1;
        }
        //=================================================================================================================================
        static public bool UpdateStatus(int ApplicationID, DateTime LastStatusDate, int ApplicationStatus)

        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                UPDATE [dbo].[Applications]
                         SET 
                                      [ApplicationStatus] = @ApplicationStatus 
                                     ,[LastStatusDate] = @LastStatusDate 
                         WHERE  ApplicationID=@ApplicationID  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            int Result = 0;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return Result != 0;

        }

        //=================================================================================================================================


    }
}
