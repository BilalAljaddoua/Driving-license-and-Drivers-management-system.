using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsUserData
    {

        public static DataTable GetAllUsers()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT Users.UserID, Users.PersonID, (People.FirstName +' '+ People.SecondName +' ' +People.ThirdName +' '+ People.LastName)as FullName, Users.UserName, Users.IsActive
FROM     People INNER JOIN
                  Users ON People.PersonID = Users.PersonID";

            SqlCommand command = new SqlCommand(quere, connection);

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
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

        //================================================Add New User===================================================================================
        static public bool AddUser(int PersonID, string UserName, string Password, bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"INSERT INTO [dbo].[Users]
                                                            ([PersonID]
                                                            ,[UserName]
                                                            ,[Password]
                                                            ,[IsActive])
                                                      VALUES
                                                            ( 
                                                            @PersonID, 
                                                             @UserName,  
                                                             @Password,  
                                                             @IsActive)  ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);




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

        //================================================Find Person==================================================================================
        static public bool FindUserByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Users] 
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
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                    UserName = (string)Reader["UserName"];
                    UserID = (int)Reader["UserID"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindUserByUserName(string UserName, ref int PersonID, ref int UserID, ref string Password, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Users] 
                                        where UserName=@UserName";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    PersonID = (int)Reader["PersonID"];
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                    UserName = (string)Reader["UserName"];
                    UserID = (int)Reader["UserID"];


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindUserByUserID(int UserID, ref string UserName, ref int PersonID, ref string Password, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Users] 
                                        where UserID=@UserID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                    UserName = (string)Reader["UserName"];
                    PersonID = (int)Reader["PersonID"];
                    UserID = (int)Reader["UserID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindByUserNameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Users] 
                                        where UserName=@UserName and Password=@Password";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                    UserName = (string)Reader["UserName"];
                    PersonID = (int)Reader["PersonID"];
                    UserID = (int)Reader["UserID"];

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update User==================================================================================
        static public bool UpdateUserByUserID(int UserID, string UserName, string Password, bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"UPDATE [dbo].[Users]
                                                            SET  
                                                               [UserName] =  @UserName 
                                                               ,[Password] = @Password 
                                                               ,[IsActive] = @IsActive 
                                                          WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

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
        static public bool UpdateUserByUserName(string UserName, string NewUserName, string Password, bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"UPDATE [dbo].[Users]
                                                            SET  
                                                               [UserName] =  @NewUserName 
                                                               ,[Password] = @Password 
                                                               ,[IsActive] = @IsActive 
                                                          WHERE UserName=@UserName";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@NewUserName", NewUserName);

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

        //================================================Delete User==================================================================================

        static public bool DeleteUserByPersonID(int PersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Users]
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
        static public bool DeleteUserByUserName(string UserName)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"
                                    DELETE FROM [dbo].[Users]
                                          WHERE UserName=@UserName";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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


        static public bool IsUserActiveByUsername(string UserName)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Users] 
                                        where UserName=@UserName";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
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
        static public bool IsUserActiveByPersonID(int PersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select * from [dbo].[Users] 
                                        where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
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

        //==============================================================================================================================
        static public bool IsUserExist(int PersonID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"select Found=1 from [dbo].[Users] 
                                        where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            bool IsFound = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {

                    IsFound = true;

                }
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
