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

            SqlCommand command = new SqlCommand("SP_AddNewTestType", connection);
            command.CommandType = CommandType.StoredProcedure; ;

            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@TestTypeFees", Fees);
            SqlParameter param = new SqlParameter("@TestTypeID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(param);

            try
            {
                connection.Open();

                 command.ExecuteNonQuery();
                TestTypeID=(int)command.Parameters["@TestTypeID"].Value;
 
            }

            catch (Exception ex)
            { 
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

            SqlCommand command = new SqlCommand("SP_FindTestTypeByTestTypeID", connection);
            command.CommandType = CommandType.StoredProcedure; ;
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID); 
            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
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
        static public bool UpdateByTestTypeID(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
              
            SqlCommand command = new SqlCommand("SP_UpdateTestTypeByTestTypeID", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            SqlParameter parameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output,
            };
            command.Parameters.Add(parameter);

            bool Result =false;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Result = (bool)command.Parameters["@IsSuccess"].Value;

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return Result;

        }

        //====================================================================================================================================

        static public DataTable GetAllTestTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
              
            SqlCommand command = new SqlCommand("SP_GetAllTestTypes", connection);
            command.CommandType= CommandType.StoredProcedure;
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
