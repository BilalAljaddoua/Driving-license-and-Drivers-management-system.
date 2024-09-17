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
    public class clsInternationalLicenseData
    {

        static public DataTable GetAllInternationalLicenseForDriver(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT InternationalLicenses.InternationalLicenseID,  Licenses.ApplicationID,LicenseID, LicenseClasses.ClassName,   InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive
                 
             FROM     InternationalLicenses INNER JOIN
                  Licenses ON InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID INNER JOIN
                  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
				  where InternationalLicenses.DriverID=@DriverID  ";

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

        static public DataTable GetAllInternationalLicense()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT InternationalLicenses.InternationalLicenseID, Licenses.ApplicationID, Licenses.DriverID, InternationalLicenses.IssuedUsingLocalLicenseID, InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, 
                  InternationalLicenses.IsActive
FROM     InternationalLicenses INNER JOIN
                  Licenses ON InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID INNER JOIN
                  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
	 ";

            SqlCommand command = new SqlCommand(quere, connection);

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

        //================================================Add New InternationalLicenses===================================================================================
        static public int AddInternationalLicenses(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int AddInternationalLicensesID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                  UPDATE [dbo].[InternationalLicenses]
                                                           SET       [IsActive] = 0
                                                          WHERE DriverID=@DriverID; 

                                                                            INSERT INTO [dbo].[InternationalLicenses]
                                                                                 ([ApplicationID]
                                                                                 ,[DriverID]
                                                                                 ,[IssuedUsingLocalLicenseID]
                                                                                 ,[IssueDate]
                                                                                 ,[ExpirationDate]
                                                                                 ,[IsActive]
                                                                                 ,[CreatedByUserID])
                                                                           VALUES
                                                                               (@ApplicationID,  
                                                                                 @DriverID,  
                                                                                 @IssuedUsingLocalLicenseID, 
                                                                                 @IssueDate, 
                                                                                 @ExpirationDate, 
                                                                                 @IsActive, 
                                                                                 @CreatedByUserID )
                                                                                          SELECT  SCOPE_IDENTITY(); ";
            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




            object Result  ;
            try
            {
                connection.Open();
                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    AddInternationalLicensesID=ID;
                }


            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return AddInternationalLicensesID;


        }

        //================================================Find InternationalLicenses==================================================================================
        static public bool FindByInternationalLicenseID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[InternationalLicenses] 
                                        where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)Reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    IsActive = (bool)Reader["IsActive"];
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
        static public bool FindByLocalLicenseID(int IssuedUsingLocalLicenseID, ref int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT  *  FROM     InternationalLicenses where IssuedUsingLocalLicenseID=@IssuedUsingLocalLicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    InternationalLicenseID = (int)Reader["InternationalLicenseID"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    IsActive = (bool)Reader["IsActive"];
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

        static public bool FindByDriverID(int DriverID, ref int InternationalLicenseID, ref int ApplicationID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[InternationalLicenses] 
                                        where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    ApplicationID = (int)Reader["ApplicationID"];
                    InternationalLicenseID = (int)Reader["InternationalLicenseID"];
                    IssuedUsingLocalLicenseID = (int)Reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    IsActive = (bool)Reader["IsActive"];
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
        static public bool FindByApplicationID(int ApplicationID, ref int DriverID, ref int InternationalLicenseID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[InternationalLicenses] 
                                        where ApplicationID=@ApplicationID";

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
                    DriverID = (int)Reader["DriverID"];
                    InternationalLicenseID = (int)Reader["InternationalLicenseID"];
                    IssuedUsingLocalLicenseID = (int)Reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    IsActive = (bool)Reader["IsActive"];
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

        //================================================Update InternationalLicenses==================================================================================
        static public bool UpdateInternationalLicenseByInternationalLicenseID(int ApplicationID, ref int DriverID, ref int InternationalLicenseID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)

        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);



            string quere = @" 
                                               UPDATE [dbo].[InternationalLicenses]
                                                  SET                  [ApplicationID] = @ApplicationID,  
                                                                         ,[DriverID] = @DriverID 
                                                                         ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
                                                                         ,[IssueDate] = @IssueDate
                                                                         ,[ExpirationDate] = @ExpirationDate
                                                                         ,[IsActive] = @IsActive
                                                                         ,[CreatedByUserID] = @CreatedByUserID
                                                WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
        static public bool UpdateInternationalLicenseByDriverID(int DriverID, int ApplicationID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)

        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);



            string quere = @" 
                                               UPDATE [dbo].[InternationalLicenses]
                                                  SET                  [ApplicationID] = @ApplicationID
                                                                         
                                                                         ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
                                                                         ,[IssueDate] = @IssueDate
                                                                         ,[ExpirationDate] = @ExpirationDate
                                                                         ,[IsActive] = @IsActive
                                                                         ,[CreatedByUserID] = @CreatedByUserID
                                                WHERE DriverID=@DriverID";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        //================================================Delete InternationalLicenses==================================================================================

        static public bool DeleteDriverByInternationalLicenseID(int InternationalLicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[InternationalLicenses]
                                          WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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
        static public bool DeleteInternationalLicenseByDriverID(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[InternationalLicenses]
                                          WHERE DriverID=@DriverID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@Drivers", DriverID);

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


        static public bool IsInternationalLicensesActiveByInternationalLicenseID(int InternationalLicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[InternationalLicenses] 
                                        where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
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
        static public bool IsInternationalLicensesActiveDriverID(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[InternationalLicenses] 
                                        where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
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

        //================================================================================================================================

    }
}
