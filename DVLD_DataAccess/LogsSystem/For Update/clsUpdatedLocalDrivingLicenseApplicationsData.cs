using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public class clsUpdatedLocalDrivingLicenseApplicationsData
    {
        public static DataTable GetAllUpdatedLocalDrivingLicenseApplications()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormUpdatedLocalDrivingLicenseApplicationsTable", connection))
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
        static public bool FindUpdatedLocalDrivingLicenseApplications(ref int? RecordID, ref int? LocalDrivingLicenseApplicationID, ref int? ApplicationID, ref int? LicenseClassID, ref int? UpdatedByUser, ref DateTime? UpdatedDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormUpdatedLocalDrivingLicenseApplicationsTable", connection))
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
                            if (reader["LocalDrivingLicenseApplicationID"] != DBNull.Value)
                            {
                                LocalDrivingLicenseApplicationID = (Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]));
                            }
                            ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                            LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                            if (reader["UpdatedByUser"] != DBNull.Value)
                            {
                                UpdatedByUser = (Convert.ToInt32(reader["UpdatedByUser"]));
                            }
                            if (reader["UpdatedDate"] != DBNull.Value)
                            {
                                UpdatedDate = (Convert.ToDateTime(reader["UpdatedDate"]));
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
        static public int? AddToUpdatedLocalDrivingLicenseApplicationsTable(int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID, int? UpdatedByUser, DateTime? UpdatedDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertIntoUpdatedLocalDrivingLicenseApplicationsTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object)LocalDrivingLicenseApplicationID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    command.Parameters.AddWithValue("@UpdatedByUser", (object)UpdatedByUser ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedDate", (object)UpdatedDate ?? DBNull.Value);

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
        static public bool UpdateUpdatedLocalDrivingLicenseApplicationsTable(int? RecordID, int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID, int? UpdatedByUser, DateTime? UpdatedDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateUpdatedLocalDrivingLicenseApplicationsTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RecordID", RecordID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object)LocalDrivingLicenseApplicationID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    command.Parameters.AddWithValue("@UpdatedByUser", (object)UpdatedByUser ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedDate", (object)UpdatedDate ?? DBNull.Value);

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
        static public bool DeleteUpdatedLocalDrivingLicenseApplications(int RecordID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteFormUpdatedLocalDrivingLicenseApplicationsTable", connection))
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

