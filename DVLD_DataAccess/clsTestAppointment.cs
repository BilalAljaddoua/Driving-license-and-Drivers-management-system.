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


            string quere = @" SELECT TestAppointments.TestAppointmentID, TestAppointments.PaidFees, TestAppointments.AppointmentDate, Users.UserName,   TestAppointments.IsLocked
FROM     TestAppointments INNER JOIN
                  Users ON TestAppointments.CreatedByUserID = Users.UserID
WHERE         LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by AppointmentDate desc ;
";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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


            string quere = @" INSERT INTO [dbo].[TestAppointments]
                                                                                      ([TestTypeID]
                                                                                      ,[LocalDrivingLicenseApplicationID]
                                                                                      ,[AppointmentDate]
                                                                                      ,[PaidFees]
                                                                                      ,[CreatedByUserID]
                                                                                      ,[IsLocked]
                                                                                      ,[RetakeTestApplicationID])
                                                                      VALUES
                                                                           (@TestTypeID, 
                                                                            @LocalDrivingLicenseApplicationID,  
                                                                            @AppointmentDate, 
                                                                            @PaidFees, 
                                                                            @CreatedByUserID, 
                                                                            @IsLocked ,
                                                                            @RetakeTestApplicationID); select SCOPE_IDENTITY(); ";

            SqlCommand command = new SqlCommand(quere, connection);

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





            object Result;
            int TestAppoinment = -1;

            try
            {
                connection.Open();
                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    TestAppoinment = ID;
                }

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


            string quere = @"  SELECT
                                            [TestTypeID],
                                            [LocalDrivingLicenseApplicationID]
                                           ,[AppointmentDate]
                                           ,[PaidFees]
                                           ,[CreatedByUserID]
                                           ,[IsLocked]
                                           ,[RetakeTestApplicationID]
                                           
                                                FROM [dbo].[TestAppointments]
                                          WHERE         TestAppointmentID=@TestAppointmentID ;   ";

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
        static public bool UpdateTestByTestID(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, bool IsLocked, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[TestAppointments]
                                                      SET          [TestTypeID] = @TestTypeID 
                                                                     ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID 
                                                                     ,[AppointmentDate] = @AppointmentDate 
                                                                     ,[PaidFees] = @PaidFees 
                                                                     ,[CreatedByUserID] = @CreatedByUserID 
                                                                     ,[IsLocked] = @IsLocked 
                                                             WHERE  TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

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

        //================================================Delete TestAppointment==================================================================================

        static public bool DeleteTestByTestAppointmentID(int TestAppointmentID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[TestAppointments]
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

        static public bool IsThereActiveAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return GetActiveAppointmentID(LocalDrivingLicenseApplicationID, TestTypeID) != -1;
        }
        static public int GetActiveAppointmentID(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int AppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"  SELECT  TestAppointmentID     FROM [dbo].[TestAppointments]
                                            WHERE         LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID and IsLocked=0 ;";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            bool IsRead = false;

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


            string quere = @" UPDATE [dbo].[TestAppointments]
                                                      SET          [IsLocked] = @IsLocked 
                                                             WHERE  TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

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

        static public int GetTestID(int TestAppointmentID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT TestID from Tests 
where TestAppointmentID=@TestAppointmentID
                            select SCOPE_IDENTITY();  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
 
            object Result;
            int TestID = -1;
            try
            {
                connection.Open();
                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                        TestID = ID;
                }

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
