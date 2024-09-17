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
    public class ApplicationType
    {
        static public DataTable GetAllApplicationsType()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT
       [ApplicationTypeID]
      ,[ApplicationTypeTitle]
      ,[ApplicationFees]
  FROM [dbo].[ApplicationTypes]
   ";

            SqlCommand command = new SqlCommand(quere, connection);



            bool IsRead = false;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.HasRows)
                {
                    IsRead = true;
                    dt.Load(Reader);

                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return dt;

        }
        //================================================Update LocalDrivingLicenseApplications==================================================================================
        static public bool UpdateApplictionType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" UPDATE [dbo].[ApplicationTypes]
                                               SET 
                                                   [ApplicationTypeTitle] = @ApplicationTypeTitle
                                                  ,[ApplicationFees] = @ApplicationFees
                                             WHERE  ApplicationTypeID=@ApplicationTypeID ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle  ", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees  ", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeID  ", ApplicationTypeID);

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


            return (Result != 0);

        }

        //================================================Delete LocalDrivingLicenseApplications==================================================================================
        static public bool FindApplicationType(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string quere = @" SELECT * FROM  [dbo].[ApplicationTypes]
                                                
                                             WHERE  ApplicationTypeID=@ApplicationTypeID ";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID  ", ApplicationTypeID);

            bool Result = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Result = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);

                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }


            return (Result);

        }



    }
}
