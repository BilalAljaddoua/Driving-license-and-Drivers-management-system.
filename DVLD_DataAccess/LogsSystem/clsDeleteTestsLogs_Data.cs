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
    public class clsDeletedTestsData
    {
        public static DataTable GetAllDeletedTests()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormDeletedTestsTable", connection))
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
        static public bool FindDeletedTests(ref int? RecordID, ref int? TestID, ref int? TestAppointmentID, ref bool? TestResult, ref string Notes, ref int? CreatedByUserID, ref int? DeletedByUser)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormDeletedTestsTable", connection))
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
                            TestID = Convert.ToInt32(reader["TestID"]);
                            TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                            TestResult = Convert.ToBoolean(reader["TestResult"]);
                            if (reader["Notes"] != DBNull.Value)
                            {
                                Notes = (Convert.ToString(reader["Notes"]));
                            }
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
