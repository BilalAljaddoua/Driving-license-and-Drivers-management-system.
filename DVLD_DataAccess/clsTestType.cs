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
    public class clsTestTypeData
    {
        //================================================Add TestType==================================================================================

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeTitle,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestTypeID;

        }

        //================================================Find TestType==================================================================================
        static public bool FindTestTypeByTestTypeID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT 
                                                          [TestTypeID]
                                                         ,[TestTypeTitle]
                                                         ,[TestTypeDescription]
                                                         ,[TestTypeFees]
                                                     FROM [dbo].[TestTypes]
                                                          where TestTypeID=@TestTypeID;
                                                          ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    IsRead = true;
                    TestTypeID = (int)Reader["TestTypeID"];
                    TestTypeTitle = (string)Reader["TestTypeTitle"];
                    TestTypeDescription = (string)Reader["TestTypeDescription"];
                    TestTypeFees = Convert.ToSingle(Reader["TestTypeFees"]);


                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return IsRead;

        }

        //================================================Update Fees==================================================================================
        static public bool UpdateFeesByLicenseClassesID(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[TestTypes]
                                               SET   [TestTypeTitle] = @TestTypeTitle 
                                                        ,[TestTypeDescription] = @TestTypeDescription 
                                                        ,[TestTypeFees] = @TestTypeFees 
                                             WHERE  TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            int Result = 0;
            try
            {
                connection.Open();
                Result = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return Result != 0;

        }

        //====================================================================================================================================

        static public DataTable GetAllTestTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "SELECT * FROM TestTypes order by TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);


            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    dataTable.Load(Reader);


                }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dataTable; ;

        }



    }
}
