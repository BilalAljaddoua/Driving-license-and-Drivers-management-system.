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

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {





        //================================================Find LicenseClass==================================================================================
        static public DataTable GetAllLicenseClass()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT *
                                                    FROM [dbo].[LicenseClasses]    ";

            SqlCommand command = new SqlCommand(quere, connection);



            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

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
        static public bool FindLicenseClassesByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT [ClassName]
                                                        ,[ClassDescription]
                                                        ,[MinimumAllowedAge]
                                                        ,[DefaultValidityLength]
                                                        ,[ClassFees]
                                                    FROM [dbo].[LicenseClasses]
                                                        WHERE       LicenseClassID=@LicenseClassID ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    ClassName = (string)Reader["ClassName"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt16(Reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt16(Reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                    LicenseClassID = Convert.ToInt16(Reader["LicenseClassID"]);

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }
        static public bool FindLicenseClassesByName(string ClassName, ref int LicenseClassID, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @"SELECT [LicenseClassID]
                                                         ,[ClassName]
                                                        ,[ClassDescription]
                                                        ,[MinimumAllowedAge]
                                                        ,[DefaultValidityLength]
                                                        ,[ClassFees]
                                                    FROM [dbo].[LicenseClasses]
                                                        WHERE       ClassName=@ClassName        ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);


            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    ClassName = (string)Reader["ClassName"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt16(Reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt16(Reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                    LicenseClassID = Convert.ToInt16(Reader["LicenseClassID"]);

                }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update Fees==================================================================================
        static public bool UpdateFeesByLicenseClassesID(int LicenseClassID, int ClassFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[LicenseClasses]
                                                              SET [ClassFees] = @ClassFees 
                                                            WHERE LicenseClassID=@LicenseClassID ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            bool IsSuccess = false;
            object Result;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    IsSuccess = (ID != 0);
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsSuccess;

        }
        static public bool UpdateFeesByLicenseClassesName(string ClassName, float ClassFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[LicenseClasses]
                                                              SET [ClassFees] = @ClassFees 
                                                            WHERE ClassName=@ClassName ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            bool IsSuccess = false;
            object Result;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    IsSuccess = (ID != 0);
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsSuccess;

        }

        //====================================================================================================================================





    }
}
