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
using System.ComponentModel;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {



        static public DataTable GetAllLocalLicenseForDriver(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"    SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
FROM     Licenses INNER JOIN
                  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
WHERE  (Licenses.DriverID = @DriverID)  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    dataTable.Load(Reader);

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dataTable;

        }

        //================================================Add New License===================================================================================
        static public int AddLicenses(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" INSERT INTO [dbo].[Licenses]
                                                                              ([ApplicationID]
                                                                              ,[DriverID]
                                                                              ,[LicenseClass]
                                                                              ,[IssueDate]
                                                                              ,[ExpirationDate]
                                                                              ,[Notes]
                                                                              ,[PaidFees]
                                                                              ,[IsActive]
                                                                              ,[IssueReason]
                                                                              ,[CreatedByUserID])
                                                                        VALUES
                                                                           ( @ApplicationID,  
                                                                             @DriverID,  
                                                                             @LicenseClass, 
                                                                             @IssueDate,  
                                                                             @ExpirationDate, 
                                                                             @Notes,  
                                                                             @PaidFees,  
                                                                             @IsActive,  
                                                                             @IssueReason, 
                                                                             @CreatedByUserID ) ;SELECT SCOPE_IDENTITY(); ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            object Result;
            int ID = -1;
            try
            {
                connection.Open();
                Result = command.ExecuteScalar();
                if (Result != null && (int.TryParse(Result.ToString(), out int NewID)))
                {
                    ID = NewID;
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return ID; ;


        }

        //================================================Find License==================================================================================
        static public bool FindLicensesByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" 
                                    SELECT     
                                                      [ApplicationID]
                                                     ,[DriverID]
                                                     ,[LicenseClass]
                                                     ,[IssueDate]
                                                     ,[ExpirationDate]
                                                     ,[Notes]
                                                     ,[PaidFees]
                                                     ,[IsActive]
                                                     ,[IssueReason]
                                                     ,[CreatedByUserID]
                                                 FROM [dbo].[Licenses]
                                                WHERE LicenseID=@LicenseID   ";

            SqlCommand command = new SqlCommand(quere, connection);

 
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    if (!(Reader["Notes"] == System.DBNull.Value))
                        Notes = (string)Reader["Notes"];
                    else
                        Notes = "";
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToInt16(Reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt16(Reader["CreatedByUserID"]);
                    LicenseID = Convert.ToInt16(Reader["LicenseID"]);


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        static public bool FindLicensesByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" 
                                    SELECT     
                                                      [LicenseID]
                                                     ,[DriverID]
                                                     ,[LicenseClass]
                                                     ,[IssueDate]
                                                     ,[ExpirationDate]
                                                     ,[Notes]
                                                     ,[PaidFees]
                                                     ,[IsActive]
                                                     ,[IssueReason]
                                                     ,[CreatedByUserID]
                                                 FROM [dbo].[Licenses]
                                                WHERE ApplicationID=@ApplicationID   ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);


            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    if (!(Reader["Notes"] == System.DBNull.Value))
                        Notes = (string)Reader["Notes"];
                    else
                        Notes = "";
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToInt16(Reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt16(Reader["CreatedByUserID"]);
                    LicenseID = Convert.ToInt16(Reader["LicenseID"]);



                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update License==================================================================================
        static public bool UpdateUserByLicenseID(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[Licenses]
                                         SET                  [ApplicationID] = @ApplicationID 
                                                                ,[DriverID] = @DriverID 
                                                                ,[LicenseClass] = @LicenseClass 
                                                                ,[IssueDate] = @IssueDate 
                                                                ,[ExpirationDate] = @ExpirationDate 
                                                                ,[Notes] = @Notes 
                                                                ,[PaidFees] = @PaidFees 
                                                                ,[IsActive] = @IsActive 
                                                                ,[IssueReason] = @IssueReason 
                                                                ,[CreatedByUserID] = @CreatedByUserID 
                                   WHERE        LicenseID=@LicenseID                   ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);
            command.Parameters.AddWithValue("@DriverID  ", DriverID);
            command.Parameters.AddWithValue("@LicenseClass  ", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate  ", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate  ", ExpirationDate);
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value); command.Parameters.AddWithValue("@PaidFees  ", PaidFees);
            command.Parameters.AddWithValue("@IsActive  ", IsActive);
            command.Parameters.AddWithValue("@IssueReason  ", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID  ", CreatedByUserID);
            command.Parameters.AddWithValue("@LicenseID  ", LicenseID);
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
        static public bool UpdateUserByApplicationID(int ApplicationID, int DriverID, string LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, string IssueReason, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[Licenses]
                                         SET                [DriverID] = @DriverID 
                                                                ,[LicenseClass] = @LicenseClass 
                                                                ,[IssueDate] = @IssueDate 
                                                                ,[ExpirationDate] = @ExpirationDate 
                                                                ,[Notes] = @Notes 
                                                                ,[PaidFees] = @PaidFees 
                                                                ,[IsActive] = @IsActive 
                                                                ,[IssueReason] = @IssueReason 
                                                                ,[CreatedByUserID] = @CreatedByUserID 
                                   WHERE        ApplicationID=@ApplicationID                   ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);
            command.Parameters.AddWithValue("@DriverID  ", DriverID);
            command.Parameters.AddWithValue("@LicenseClass  ", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate  ", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate  ", ExpirationDate);
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value); command.Parameters.AddWithValue("@PaidFees  ", PaidFees);
            command.Parameters.AddWithValue("@IsActive  ", IsActive);
            command.Parameters.AddWithValue("@IssueReason  ", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID  ", CreatedByUserID);
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

        //================================================Delete License==================================================================================

        static public bool DeleteLicenseByLicenseID(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Licenses]
                                          WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
        static public bool DeleteLicenseByApplicationID(int ApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Licenses]
                                          WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
        static public bool DeActivatLicense(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"UPDATE [dbo].[Licenses]
                                                          SET   [IsActive] =0 
                                                        WHERE  LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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


        //=================================================Is Active================================================================================


        static public bool IsLicenseActiveByLicenseID(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Licenses] 
                                        where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            bool IsActive = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsActive = (bool)Reader["IsActive"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsActive;

        }
        static public int IsLicenseExists(int ApplicantPersonID, int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT Licenses.LicenseID
                 FROM     Licenses INNER JOIN
                  Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN
                  LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID

				  where ApplicantPersonID=@ApplicantPersonID and LicenseClass=@LicenseClass";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseID);
            int ID = -1;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    ID = (int)Reader["LicenseID"];


                }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return ID;

        }


    }
}
