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

            SqlCommand command = new SqlCommand("SP_AddPerson", connection);
            command.CommandType = CommandType.StoredProcedure;
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
            SqlParameter parameter = new SqlParameter("@PersonID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);



            int PersonID = 0;

            try
            {
                connection.Open();
                command.ExecuteScalar();
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
         
        static public DataTable GetAllPeople()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_GetAllPeople", connection);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
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
 
            SqlCommand command = new SqlCommand("SP_FindPersonByID", connection);
            command.CommandType = CommandType.StoredProcedure;
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
                                                                 short Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, int UpdatedByUser)
        {
            bool IsSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

             
            SqlCommand command = new SqlCommand("SP_UpdatePerson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UpdatedByUser", UpdatedByUser);

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
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
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


            return  IsSuccess;

        }

        //================================================Delete Person==================================================================================

        static public bool DeletePerson(string NationalNo,int UserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_DeletePerson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@UserID", UserID);

            bool IsSuccess = false;
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
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


        //=================================================================================================================================

        static public bool IsPersonExist(string NationalNo)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 
            SqlCommand command = new SqlCommand("SP_IsPersonExist", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            bool IsSuccess = false;
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
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

        static public bool IsPersonExist(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_IsPersonExistByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            bool IsSuccess = false;
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
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


    }
}
