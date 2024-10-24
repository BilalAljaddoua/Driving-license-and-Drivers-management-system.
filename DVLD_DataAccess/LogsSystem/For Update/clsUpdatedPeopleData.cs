using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess; 
namespace DataAccessLayer
{
    public class clsUpdatedPeopleData
    {
        public static DataTable GetAllUpdatedPeople()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormUpdatedPeopleTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
                    { connection.Close(); }
                    return dt;
                }
            }
        }
        static public bool FindUpdatedPeople(ref int? RecordID, ref int? PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime? DateOfBirth, ref byte? Gendor, ref string Address, ref string Phone, ref string Email, ref int? NationalityCountryID, ref string ImagePath, ref int? DeletedByUser, ref DateTime? DeleteDate, ref DateTime? DateOfUpdate, ref int? UpdatedBuUser)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormUpdatedPeopleTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RecordID", RecordID);
                    bool IsRead = false;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            IsRead = true;
                            RecordID = Convert.ToInt32(reader["RecordID"]);
                            PersonID = Convert.ToInt32(reader["PersonID"]);
                            NationalNo = Convert.ToString(reader["NationalNo"]);
                            FirstName = Convert.ToString(reader["FirstName"]);
                            SecondName = Convert.ToString(reader["SecondName"]);
                            if (reader["ThirdName"] != DBNull.Value)
                            {
                                ThirdName = (Convert.ToString(reader["ThirdName"]));
                            }
                            LastName = Convert.ToString(reader["LastName"]);
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            Gendor = Convert.ToByte(reader["Gendor"]);
                            Address = Convert.ToString(reader["Address"]);
                            Phone = Convert.ToString(reader["Phone"]);
                            if (reader["Email"] != DBNull.Value)
                            {
                                Email = (Convert.ToString(reader["Email"]));
                            }
                            NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                            if (reader["ImagePath"] != DBNull.Value)
                            {
                                ImagePath = (Convert.ToString(reader["ImagePath"]));
                            }
                            if (reader["DeletedByUser"] != DBNull.Value)
                            {
                                DeletedByUser = (Convert.ToInt32(reader["DeletedByUser"]));
                            }
                            if (reader["DeleteDate"] != DBNull.Value)
                            {
                                DeleteDate = (Convert.ToDateTime(reader["DeleteDate"]));
                            }
                            if (reader["DateOfUpdate"] != DBNull.Value)
                            {
                                DateOfUpdate = (Convert.ToDateTime(reader["DateOfUpdate"]));
                            }
                            if (reader["UpdatedBuUser"] != DBNull.Value)
                            {
                                UpdatedBuUser = (Convert.ToInt32(reader["UpdatedBuUser"]));
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        connection.Close();
                    }
                    return IsRead;
                }
            }
        }
        static public int? AddToUpdatedPeopleTable(int? PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int? NationalityCountryID, string ImagePath, int? DeletedByUser, DateTime? DeleteDate, DateTime? DateOfUpdate, int? UpdatedBuUser)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertIntoUpdatedPeopleTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", (object)ThirdName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DeletedByUser", (object)DeletedByUser ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DeleteDate", (object)DeleteDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DateOfUpdate", (object)DateOfUpdate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedBuUser", (object)UpdatedBuUser ?? DBNull.Value);

                    SqlParameter parameter = new SqlParameter("@RecordID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(parameter);
                    int? RecordID = null;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        RecordID = (int)command.Parameters["@RecordID"].Value;
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        connection.Close();
                    }
                    return RecordID;
                }
            }
        }
        static public bool UpdateUpdatedPeopleTable(int? RecordID, int? PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int? NationalityCountryID, string ImagePath, int? DeletedByUser, DateTime? DeleteDate, DateTime? DateOfUpdate, int? UpdatedBuUser)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateUpdatedPeopleTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RecordID", RecordID);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", (object)ThirdName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DeletedByUser", (object)DeletedByUser ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DeleteDate", (object)DeleteDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DateOfUpdate", (object)DateOfUpdate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedBuUser", (object)UpdatedBuUser ?? DBNull.Value);

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
                    { connection.Close(); }

                    return IsSuccess;
                }
            }
        }
        static public bool DeleteUpdatedPeople(int RecordID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteFormUpdatedPeopleTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RecordID", RecordID);
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
            }
        }

    }
}
