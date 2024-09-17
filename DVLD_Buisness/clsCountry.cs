using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsCountry
    {
        public string CountryName { get; set; }

        public int CountryID { get; set; }

        public clsCountry()
        {
            this.CountryName = "";
            this.CountryID = -1;
        }
        private clsCountry(string countryName, int CountryID)
        {
            this.CountryName = countryName;
            this.CountryID = CountryID;
        }

        public static clsCountry FindByID(int id)
        {
            string CountryName = "";
            if (clsCountryData.GetCountryInfoByID(id, ref CountryName))
            {
                return new clsCountry(CountryName, id);
            }
            return null;

        }
        public static clsCountry FindByName(string CountryName)
        {
            int ID = -1;
            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID))
            {
                return new clsCountry(CountryName, ID);
            }
            return null;

        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
