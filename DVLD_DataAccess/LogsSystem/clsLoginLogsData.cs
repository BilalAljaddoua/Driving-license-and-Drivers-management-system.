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
    public class clsLoginLogsData
    {
        public static DataTable GetAllLoginLogs()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormLoginLogsTable", connection))
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
        static public bool FindLoginLogs(ref int? RecordID, ref int? UserID, ref DateTime? DateOfLogin, ref DateTime? DateOfLogOut)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormLoginLogsTable", connection))
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
                            UserID = Convert.ToInt32(reader["UserID"]);
                            DateOfLogin = Convert.ToDateTime(reader["DateOfLogin"]);
                            if (reader["DateOfLogOut"] != DBNull.Value)
                            {
                                DateOfLogOut = (Convert.ToDateTime(reader["DateOfLogOut"]));
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
        static public int? AddToLoginLogsTable(int? UserID, DateTime? DateOfLogin, DateTime? DateOfLogOut)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertIntoLoginLogsTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@DateOfLogin", DateOfLogin);
                    command.Parameters.AddWithValue("@DateOfLogOut", (object)DateOfLogOut ?? DBNull.Value);

                    SqlParameter parameter = new SqlParameter(@"RecordID", SqlDbType.Int)
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
        static public bool UpdateLoginLogsTable(int? RecordID, int? UserID, DateTime? DateOfLogin, DateTime? DateOfLogOut)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateLoginLogsTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RecordID", RecordID);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@DateOfLogin", DateOfLogin);
                    command.Parameters.AddWithValue("@DateOfLogOut", (object)DateOfLogOut ?? DBNull.Value);

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
        static public bool DeleteLoginLogs(int RecordID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteFormLoginLogsTable", connection))
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
