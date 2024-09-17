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
    public class clsTestData
    {

        //================================================Add New Tests===================================================================================
        static public bool AddTests(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" INSERT INTO [dbo].[Tests]
                                                             ([TestAppointmentID]
                                                             ,[TestResult]
                                                             ,[Notes]
                                                             ,[CreatedByUserID])
                                                       VALUES
                                                             (@TestAppointmentID 
                                                             ,@TestResult 
                                                             ,@Notes 
                                                             ,@CreatedByUserID ) 

                                UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (!string.IsNullOrEmpty(Notes))
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            }
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




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

        //================================================Find Tests==================================================================================
        static public bool FindTestByTestID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT [TestID]
                                                            ,[TestAppointmentID]
                                                            ,[TestResult]
                                                            ,[Notes]
                                                            ,[CreatedByUserID]
                                                        FROM [dbo].[Tests]
                                    WHERE TestID=@TestID
                                                      ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    TestAppointmentID = (int)Reader["TestAppointmentID"];
                    TestResult = (bool)Reader["TestResult"];
                    Notes = (string)Reader["Notes"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool GetLastTestPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID, ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT top(1) Tests.TestID, Tests.TestAppointmentID, Tests.TestResult, Tests.Notes, Tests.CreatedByUserID
FROM     Tests INNER JOIN
                  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN
                  LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
				  where TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID
				  order by TestID desc ";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    TestAppointmentID = (int)Reader["TestAppointmentID"];
                    TestResult = (bool)Reader["TestResult"];
                    Notes = (string)Reader["Notes"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

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
        static public bool FindTestByTestAppointmentID(int TestAppointmentID, ref int TestID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT [TestID]
                                                            ,[TestAppointmentID]
                                                            ,[TestResult]
                                                            ,[Notes]
                                                            ,[CreatedByUserID]
                                                        FROM [dbo].[Tests]
                                    WHERE TestAppointmentID=@TestAppointmentID
                                                      ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    TestAppointmentID = (int)Reader["TestAppointmentID"];
                    TestResult = (bool)Reader["TestResult"];
                    Notes = (string)Reader["Notes"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    TestID = (int)Reader["TestID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
 
        //================================================Update Tests==================================================================================
        static public bool UpdateTestByTestID(int TestID, byte TestResult, string Notes, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[Tests]
                                              SET   [TestResult] = @TestResult   
                                                      ,[Notes] = @Notes  
                                                      ,[CreatedByUserID] = @CreatedByUserID   
                                      WHERE TestID=@TestID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestID", TestID);

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
        static public bool UpdateTestByTestAppointmentID(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[Tests]
                                              SET   [TestResult] = @TestResult   
                                                      ,[Notes] = @Notes  
                                                      ,[CreatedByUserID] = @CreatedByUserID   
                                      WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        //================================================Delete Tests==================================================================================

        static public bool DeleteTestByTestID(int TestID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Tests]
                                          WHERE TestID=@TestID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

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
        static public bool DeleteTestByTestAppointmentID(int TestAppointmentID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Tests]
                                          WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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


        //========================================================================================================================================

        static public bool IsLDLAppPassTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT Result =1 FROM     TestAppointments INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				 where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID  and TestTypeID=@TestTypeID and TestResult=1
                 ORDER BY Tests.TestID DESC";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            bool IsSuccess = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsSuccess = true;
                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsSuccess;

        }

        static public int GetPassedTests(int LocalDrivingLicenseApplicationID)
        {
            int LDLApp = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT TestCount= COUNT(*) FROM     Tests INNER JOIN
                  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
				  where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1
                                                      ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    LDLApp = (int)Reader["TestCount"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return LDLApp;

        }
        static public bool IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {

            return GetPassedTests(LocalDrivingLicenseApplicationID) == 3;

        }
    }
}
