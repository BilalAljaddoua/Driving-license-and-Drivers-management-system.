using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {

        //================================================Add New Person===================================================================================
        static public int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (
           @NationalNo, 
           @FirstName,  
           @SecondName, 
           @ThirdName,  
           @LastName,  
           @DateOfBirth,  
           @Gendor, 
           @Address, 
           @Phone,
           @Email,  
           @NationalityCountryID, 
           @ImagePath  ) ; select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != null && ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != null && ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);



            int PersonID = 0;

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    PersonID = ID;
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return PersonID;


        }

        //================================================Find Person==================================================================================

        static public DataTable GetAllPeople()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
case
when Gendor = 0
then 'Male'
when Gendor=1
then 'Female'
end
as Gendor
, People.Address, People.Phone, People.Email, Countries.CountryName, 
                  People.ImagePath
FROM     Countries INNER JOIN
                  People ON Countries.CountryID = People.NationalityCountryID";

            SqlCommand command = new SqlCommand(quere, connection);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return dataTable;

        }

        static public bool FindPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
              ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT * from  People where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(quere, connection);
            bool IsRead = false;
            command.Parameters.AddWithValue("@NationalNo", NationalNo);


            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsRead = true;
                    PersonID = Convert.ToInt16(Reader["PersonID"]);
                    NationalNo = Convert.ToString(Reader["NationalNo"]);
                    FirstName = Convert.ToString(Reader["FirstName"]);
                    SecondName = Convert.ToString(Reader["SecondName"]);
                    if (ThirdName != null || ThirdName != "")
                    {
                        ThirdName = Convert.ToString(Reader["ThirdName"]);
                    }
                    else { ThirdName = ""; }
                    LastName = Convert.ToString(Reader["LastName"]);
                    DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                    Gendor = Convert.ToByte(Reader["Gendor"]);
                    Address = Convert.ToString(Reader["Address"]);
                    Phone = Convert.ToString(Reader["Phone"]);
                    if (Email != null || Email != "")
                    {
                        Email = Convert.ToString(Reader["Email"]);
                    }
                    else { Email = ""; }
                    NationalityCountryID = Convert.ToInt16(Reader["NationalityCountryID"]);

                    if (ImagePath != null || ImagePath != "")
                    {
                        ImagePath = Convert.ToString(Reader["ImagePath"]);
                    }
                    else { ImagePath = ""; }

                    Reader.Close();


                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return IsRead;
        }


        static public bool FindPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
        ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT * from  People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(quere, connection);
            bool IsRead = false;
            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsRead = true;
                    PersonID = Convert.ToInt16(Reader["PersonID"]);
                    NationalNo = Convert.ToString(Reader["NationalNo"]);
                    FirstName = Convert.ToString(Reader["FirstName"]);
                    SecondName = Convert.ToString(Reader["SecondName"]);
                    if (ThirdName != null || ThirdName != "")
                    {
                        ThirdName = Convert.ToString(Reader["ThirdName"]);
                    }
                    else { ThirdName = ""; }
                    LastName = Convert.ToString(Reader["LastName"]);
                    DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                    Gendor = Convert.ToInt16(Reader["Gendor"]);
                    Address = Convert.ToString(Reader["Address"]);
                    Phone = Convert.ToString(Reader["Phone"]);
                    if (Email != null || Email != "")
                    {
                        Email = Convert.ToString(Reader["Email"]);
                    }
                    else { Email = ""; }
                    NationalityCountryID = Convert.ToInt16(Reader["NationalityCountryID"]);

                    if (ImagePath != null || ImagePath != "")
                    {
                        ImagePath = Convert.ToString(Reader["ImagePath"]);
                    }
                    else { ImagePath = ""; }



                    Reader.Close();

                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return IsRead;
        }


        //================================================Update Person==================================================================================
        static public bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
                                                                 short Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {

            int Result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                      UPDATE [dbo].[People]
                                         SET [NationalNo] = NationalNo,
                                               [FirstName] = @FirstName,
                                               [SecondName] =@SecondName ,
                                               [ThirdName] = @ThirdName ,
                                               [LastName] = @LastName,  
                                               [DateOfBirth] = @DateOfBirth,  
                                               [Gendor] = @Gendor, 
                                               [Address] = @Address,  
                                               [Phone] = @Phone,  
                                               [Email] = @Email,  
                                               [NationalityCountryID] =@NationalityCountryID, 
                                               [ImagePath] = @ImagePath 
                                       WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != null && ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            }
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
            {
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            }
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != null && ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            }
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


            return Result > 0;

        }

        //================================================Delete Person==================================================================================

        static public bool DeletePerson(string NationalNo)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                DELETE FROM [dbo].[People]
                             WHERE  NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        static public bool IsPersonExist(string NationalNo)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" select Found=1 from People
                                            where NationalNo=@NationalNo
";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);


            bool IsFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        static public bool IsPersonExist(int PersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" select Found=1 from People
                                            where PersonID=@PersonID
";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);


            bool IsFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }


    }
}
