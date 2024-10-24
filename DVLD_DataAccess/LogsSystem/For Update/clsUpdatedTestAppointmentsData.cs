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
    public class clsUpdatedTestAppointmentsData
    {
        public static DataTable GetAllUpdatedTestAppointments()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormUpdatedTestAppointmentsTable", connection))
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
        static public bool FindUpdatedTestAppointments(ref int? RecordID, ref int? TestAppointmentID, ref int? TestTypeID, ref int? LocalDrivingLicenseApplicationID, ref DateTime? AppointmentDate, ref decimal? PaidFees, ref int? CreatedByUserID, ref bool? IsLocked, ref int? RetakeTestApplicationID, ref int? UpdatedByUser, ref DateTime? UpdatedDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormUpdatedTestAppointmentsTable", connection))
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
                            TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                            TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                            LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                            PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                            IsLocked = Convert.ToBoolean(reader["IsLocked"]);
                            if (reader["RetakeTestApplicationID"] != DBNull.Value)
                            {
                                RetakeTestApplicationID = (Convert.ToInt32(reader["RetakeTestApplicationID"]));
                            }
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
        static public int? AddToUpdatedTestAppointmentsTable(int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID, int? UpdatedByUser, DateTime? UpdatedDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertIntoUpdatedTestAppointmentsTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsLocked", IsLocked);
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", (object)RetakeTestApplicationID ?? DBNull.Value);
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
        static public bool UpdateUpdatedTestAppointmentsTable(int? RecordID, int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID, int? UpdatedByUser, DateTime? UpdatedDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateUpdatedTestAppointmentsTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RecordID", RecordID);
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsLocked", IsLocked);
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", (object)RetakeTestApplicationID ?? DBNull.Value);
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
        static public bool DeleteUpdatedTestAppointments(int RecordID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteFormUpdatedTestAppointmentsTable", connection))
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

