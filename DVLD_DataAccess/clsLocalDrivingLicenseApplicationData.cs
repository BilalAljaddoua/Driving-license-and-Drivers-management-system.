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


            string quere = @"SELECT *
                              FROM LocalDrivingLicenseApplications_View
                              order by ApplicationDate Desc";

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

        //================================================Add New LocalDrivingLicenseApplications===================================================================================
        static public int AddLocalDrivingLicenseApplications(int ApplicationID, int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                                                                          ([ApplicationID]
                                                                          ,[LicenseClassID])
                                                                    VALUES
                                                                          (@ApplicationID,  
                                                                           @LicenseClassID  );select SCOPE_IDENTITY(); ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



            object Result;
            int NewID = -1;
            try
            {
                connection.Open();
                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    NewID = ID;
                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return NewID;


        }

        //================================================Find LocalDrivingLicenseApplications==================================================================================
        static public bool FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"  SELECT LocalDrivingLicenseApplicationID
                                                           ,[ApplicationID]
                                                           ,[LicenseClassID]
                                          FROM [dbo].[LocalDrivingLicenseApplications]
                                  WHERE [LocalDrivingLicenseApplicationID]=@LocalDrivingLicenseApplicationID
   ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID  ", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID  ", LocalDrivingLicenseApplicationID);


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


            string quere = @"  SELECT LocalDrivingLicenseApplicationID
                                                           ,[ApplicationID]
                                                           ,[LicenseClassID]
                                          FROM [dbo].[LocalDrivingLicenseApplications]
                                  WHERE [ApplicationID]=@ApplicationID
   ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID  ", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID  ", LocalDrivingLicenseApplicationID);


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
        static public bool UpdateByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[LocalDrivingLicenseApplications]
                                                                    SET 
                                                                       [LicenseClassID] = @LicenseClassID 
                                                                  WHERE  LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID     ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID ", ApplicationID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        //================================================Delete LocalDrivingLicenseApplications==================================================================================

        static public bool DeleteByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"					 delete from Tests 
					 where TestAppointmentID in(SELECT Tests.TestAppointmentID
FROM     TestAppointments INNER JOIN
                  LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				 
				     where TestAppointments.LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID)

					 delete from dbo.TestAppointments
					 where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
					 delete from dbo.LocalDrivingLicenseApplications
					 where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        //==============================================================================================================================
        static public bool DosePassTesttype(  int LocalDrivingLicenseApplicationID,   int TestTypeID)

        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return Result;

        }

        static public int HowNumberTrials(int LDLApp, int TestTypeID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT COUNT(*) AS Count
                                                                    FROM     LocalDrivingLicenseApplications INNER JOIN
                                                                                      TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                                                                      Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                                                    WHERE  (TestAppointments.LocalDrivingLicenseApplicationID = @LDLApp) AND (TestAppointments.TestTypeID = @TestTypeID)";
            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@LDLApp", LDLApp);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            int CountTrial = -1;
            object Result;
            try
            {
                connection.Open();

                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int Count))
                {
                    CountTrial = Count;
                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return CountTrial; ;


        }

        static public bool IsthereAnActiveAppintmentWithTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT   Found =1
FROM     TestAppointments INNER JOIN
                  TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID
WHERE  (TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) AND (TestTypes.TestTypeID = @TestTypeID) AND (TestAppointments.IsLocked = 0)";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestTypeID  ", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID  ", LocalDrivingLicenseApplicationID);


            bool IsFound = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                IsFound = Reader.HasRows;
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


            string quere = @"  SELECT LicenseID
FROM     LocalDrivingLicenseApplications INNER JOIN
                  LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN
                  Licenses ON LicenseClasses.LicenseClassID = Licenses.LicenseClass
				  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LDLApp and LicenseClass=@LicenseClass";
            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@LDLApp", LDLApp);
             command.Parameters.AddWithValue("@LicenseClass", LicenseClass);



            int LiceneID = -1;
            object Result;
            try
            {
                connection.Open();

                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    LiceneID = ID;
                }

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
