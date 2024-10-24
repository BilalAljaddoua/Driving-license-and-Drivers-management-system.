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
    public class clsDeletedPeopleData
    {
        public static DataTable GetAllDeletedPeople()
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SelectFormDeletedPeopleTable", connection))
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
        static public bool FindDeletedPeople(ref int? RecordID, ref int? PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime? DateOfBirth, ref byte? Gendor, ref string Address, ref string Phone, ref string Email, ref int? NationalityCountryID, ref string ImagePath, ref int? DeletedByUser)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindFormDeletedPeopleTable", connection))
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
