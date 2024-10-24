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
    public class clsDeletedTestAppointmentsData
    {
        public static DataTable GetAllDeletedTestAppointments()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormDeletedTestAppointmentsTable", connection))
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
        static public bool FindDeletedTestAppointments(ref int? RecordID, ref int? TestAppointmentID, ref int? TestTypeID, ref int? LocalDrivingLicenseApplicationID, ref DateTime? AppointmentDate, ref decimal? PaidFees, ref int? CreatedByUserID, ref bool? IsLocked, ref int? RetakeTestApplicationID, ref int? DeletedByUser)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormDeletedTestAppointmentsTable", connection))
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
                            if (reader["DeletedByUser"] != DBNull.Value)
                            {
                                DeletedByUser = (Convert.ToInt32(reader["DeletedByUser"]));
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
 
    }
}
