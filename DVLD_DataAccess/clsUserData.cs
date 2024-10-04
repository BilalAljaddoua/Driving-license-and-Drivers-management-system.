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

            SqlCommand command = new SqlCommand("SP_GetAllUsers", connection);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                Reader.Read();
                if (Reader.HasRows)
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

        //================================================Add New User===================================================================================
        static public int AddUser(string UserName, string Password, bool IsActive)
        {
              int PersonID=-1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
             SqlCommand command = new SqlCommand("SP_AddNewUser", connection);
            command.CommandType=CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            SqlParameter OutputParameter = new SqlParameter("@PersonID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(OutputParameter);
             try
            {
                connection.Open();
                command.ExecuteNonQuery();
                PersonID = (int)command.Parameters["@PersonID"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return PersonID;

        }

        //================================================Find Person==================================================================================
        static public bool FindUserByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_FindUserbyPersonID", connection);
            command.CommandType= CommandType.StoredProcedure;
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
             

            SqlCommand command = new SqlCommand("SP_FindUserbyName", connection);
            command.CommandType = CommandType.StoredProcedure;
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

            SqlCommand command = new SqlCommand("SP_FindUserbyUserID", connection);
            command.CommandType = CommandType.StoredProcedure;
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
             

            SqlCommand command = new SqlCommand("SP_FindByUserNameAndPassword", connection);
            command.CommandType = CommandType.StoredProcedure;

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
             
            SqlCommand command = new SqlCommand("SP_UpdateUserByUserID", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsSuccess = false;
             try
            {
                connection.Open();
               command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;
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

            SqlCommand command = new SqlCommand("SP_UpdateUserByUserName", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsSuccess = false;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;
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
             
            SqlCommand command = new SqlCommand("SP_DeleteUserByPersonID", connection);
            command.CommandType=CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsSuccess = false;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;
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

            SqlCommand command = new SqlCommand("SP_DeleteUserByUserName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", UserName);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsSuccess = false;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;
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
             
            SqlCommand command = new SqlCommand("SP_IsUserActiveByUserName", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", UserName);
            SqlParameter parameter = new SqlParameter("@IsActive", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsActive = false;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsActive = (bool)command.Parameters["@IsActive"].Value;
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

            SqlCommand command = new SqlCommand("SP_IsUserActiveByPersonID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            SqlParameter parameter = new SqlParameter("@IsActive", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            bool IsActive = false;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                IsActive = (bool)command.Parameters["@IsActive"].Value;
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
