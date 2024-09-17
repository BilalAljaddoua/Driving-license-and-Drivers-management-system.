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
    public class clsDriverData
    {
        static public DataTable GetAllDrivers()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT Drivers.DriverID, People.PersonID, People.NationalNo,
      CONCAT(People.FirstName, ' ', People.SecondName, ' ', People.ThirdName, ' ', People.LastName) As FullName
      , Drivers.CreatedDate ,
      (SELECT COUNT(LicenseClass)
       FROM Licenses 
       WHERE Drivers.DriverID = Licenses.DriverID) As LicensesCount
FROM People 
INNER JOIN Drivers ON People.PersonID = Drivers.PersonID order by DriverID desc";

            SqlCommand command = new SqlCommand(quere, connection);
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
        public static DataTable GetLocalLicense(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
FROM     Licenses INNER JOIN
                  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
WHERE  (Licenses.DriverID = @DriverID) order by LicenseID desc";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
        public static DataTable GetInternationalLicenses(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT InternationalLicenses.InternationalLicenseID, Licenses.ApplicationID, Licenses.LicenseID, InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, InternationalLicenses.IsActive
FROM     LicenseClasses INNER JOIN
                  Licenses ON LicenseClasses.LicenseClassID = Licenses.LicenseClass INNER JOIN
                  InternationalLicenses ON Licenses.LicenseID = InternationalLicenses.IssuedUsingLocalLicenseID
				  where InternationalLicenses.DriverID=@DriverID
				  order by LicenseID desc;";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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

        static public int GetPersonID(int DriverID)
        { 

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


        string quere = @"select PersonID from [dbo].[Drivers] 
                                        where DriverID=@DriverID";

        SqlCommand command = new SqlCommand(quere, connection);

        command.Parameters.AddWithValue("@DriverID", DriverID);
            int PersonID = -1;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                     PersonID = (int) Reader["PersonID"]; 


                 }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
    connection.Close();
}


return PersonID;

        }
        //================================================Add New Person===================================================================================
        static public int AddDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"INSERT INTO [dbo].[Drivers]
                                                                          ([PersonID]
                                                                         ,[CreatedByUserID]
                                                                         ,[CreatedDate])
                                                                   VALUES
                                                                         (@PersonID,  
                                                                          @CreatedByUserID, 
                                                                          @CreatedDate ) ;SELECT SCOPE_IDENTITY(); ";
            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            int DriverID = -1;
            object Result;
            try
            {
                connection.Open();
                Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    DriverID = ID;
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return DriverID;

        }

        //================================================Find Driver==================================================================================
        static public bool FindDriverByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Drivers] 
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
                    PersonID = (int)Reader["PersonID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)Reader["CreatedDate"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Drivers] 
                                        where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    DriverID = (int)Reader["DriverID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)Reader["CreatedDate"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindDriverNationalNo(string NationalNo, ref int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT *
                                          FROM     Drivers INNER JOIN
                  People ON Drivers.PersonID = People.PersonID
				  where People.NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    DriverID = (int)Reader["DriverID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)Reader["CreatedDate"];
                    PersonID = (int)Reader["PersonID"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update Driver==================================================================================
        static public bool UpdateDriverByPersonID(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"UPDATE [dbo].[Drivers]
                                                                SET  
                                                                       [CreatedByUserID] = @CreatedByUserID,  
                                                                      ,[CreatedDate] = @CreatedDate, 
                                                   WHERE PersonID=@PersonID ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
        static public bool UpdateDriverByDriverID(int DriverID, int CreatedByUserID, DateTime CreatedDate)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"UPDATE [dbo].[Drivers]
                                                                SET  
                                                                      ,[CreatedByUserID] = @CreatedByUserID,  
                                                                      ,[CreatedDate] = @CreatedDate, 
                                                   WHERE DriverID=@DriverID ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        //================================================Delete Driver==================================================================================

        static public bool DeleteDriverByPersonID(int PersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Drivers]
                                          WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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
        static public bool DeleteDriverByDriverID(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Users]
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



    }
}
