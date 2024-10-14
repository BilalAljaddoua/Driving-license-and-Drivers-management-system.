using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using System.Net;
using System.Security.Policy;
using System.ComponentModel;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {



        static public DataTable GetAllLocalLicenseForDriver(int DriverID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_GetAllLocalLicenseForDriver", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DriverID", DriverID);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    dataTable.Load(Reader);

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dataTable;

        } 

        //================================================Add New License===================================================================================
        static public int AddLicenses(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_AddLicenses", connection);
            command.CommandType= CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            SqlParameter parameter = new SqlParameter("@LicensID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
             
            int ID = -1;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@LicensID"].Value;
            
            }

            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return ID; ;


        }

        //================================================Find License==================================================================================
        static public bool FindLicensesByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_FindLicensesByLicenseID", connection);
            command.CommandType = CommandType.StoredProcedure;
 
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    if (!(Reader["Notes"] == System.DBNull.Value))
                        Notes = (string)Reader["Notes"];
                    else
                        Notes = "";
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToInt16(Reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt16(Reader["CreatedByUserID"]);
                    LicenseID = Convert.ToInt16(Reader["LicenseID"]);


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        static public bool FindLicensesByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_FindLicensesByApplicationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);


            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    IsRead = true;
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    if (!(Reader["Notes"] == System.DBNull.Value))
                        Notes = (string)Reader["Notes"];
                    else
                        Notes = "";
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToInt16(Reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt16(Reader["CreatedByUserID"]);
                    LicenseID = Convert.ToInt16(Reader["LicenseID"]);



                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update License==================================================================================
        static public bool UpdateLicensesByLicenseID(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
 

            SqlCommand command = new SqlCommand("SP_UpdateLicensesByLicenseID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicationID  ", ApplicationID);
            command.Parameters.AddWithValue("@DriverID  ", DriverID);
            command.Parameters.AddWithValue("@LicenseClass  ", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate  ", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate  ", ExpirationDate);
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value); command.Parameters.AddWithValue("@PaidFees  ", PaidFees);
            command.Parameters.AddWithValue("@IsActive  ", IsActive);
            command.Parameters.AddWithValue("@IssueReason  ", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID  ", CreatedByUserID);
            command.Parameters.AddWithValue("@LicenseID  ", LicenseID);
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
 
        //================================================Delete License==================================================================================

        static public bool DeleteLicenseByLicenseID(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("@SP_DeleteLicenseByLicenseID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
        static public bool DeleteLicenseByApplicationID(int ApplicationID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_DeleteLicenseByApplicationID", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        static public bool DeActivatLicense(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             

            SqlCommand command = new SqlCommand("SP_DeActivatLicense", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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


        static public bool IsLicenseActiveByLicenseID(int LicenseID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_IsLicenseActiveByLicenseID", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
        static public int IsLicenseExists(int ApplicantPersonID, int LicenseClassID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
             
            SqlCommand command = new SqlCommand("SP_IsLicenseExists", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            SqlParameter parameter = new SqlParameter("@LicenseID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(parameter);
            int  LicenseID=-1;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                LicenseID = (int)command.Parameters["@LicenseID"].Value;
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return LicenseID;


        }


    }
}
