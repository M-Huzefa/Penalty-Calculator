using Microsoft.Extensions.Configuration;
using PenaltyCalculator.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace PenaltyCalculator.DataLayer
{
    public class SqlDataBase : ISqlDataBase
    {
        string conString = "";
        
        // Dependency injection
        public SqlDataBase(IConfiguration configuration)
        {
            conString = configuration.GetConnectionString("connection");
        }
        
        //Initializing lists
        public List<Country> countryList = new List<Country>();
        public List<selectedCountry> selectedCountryList = new List<selectedCountry>();

        

        // Function to get Country list directly from DB
        public List<Country> GetCountries()
        {
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlDataAdapter SDA = new SqlDataAdapter("SELECT * FROM country", connection);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Country country = new Country();
                country.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                country.Name = dt.Rows[i]["Name"].ToString();
                //country.TaxPenalty = Convert.ToInt32(dt.Rows[i]["TaxPenalty"]);
                //country.PenaltyPrice = Convert.ToDecimal(dt.Rows[i]["PenaltyPrice"]);
                //country.CurrencyCode = dt.Rows[i]["CurrencyCode"].ToString();
                //country.Weekend1 = Convert.ToInt32(dt.Rows[i]["Weekend1"]);
                //country.Weekend2 = Convert.ToInt32(dt.Rows[i]["Weekend2"]);
                countryList.Add(country);
            }
            connection.Close();

            return (countryList);
        }
        
        //Function to get selected country detail directly from DB
        public List<selectedCountry> SelectedCountryData(int countryId)
        {
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlDataAdapter SDA = new SqlDataAdapter("select c.*, h.Name as HolidayName, h.Date from country as c inner join holidays as h on c.id = h.CountryID where c.ID = '" + countryId + "'", connection);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectedCountry selectedCountry = new selectedCountry();
                selectedCountry.CountryId = Convert.ToInt32(dt.Rows[i]["ID"]);
                selectedCountry.CountryName = dt.Rows[i]["Name"].ToString();
                selectedCountry.TaxPenalty = Convert.ToInt32(dt.Rows[i]["TaxPenalty"]);
                selectedCountry.PenaltyPrice = Convert.ToDecimal(dt.Rows[i]["PenaltyPrice"]);
                selectedCountry.CurrencyCode = dt.Rows[i]["CurrencyCode"].ToString();
                selectedCountry.Weekend1 = Convert.ToInt32(dt.Rows[i]["Weekend1"]);
                selectedCountry.Weekend2 = Convert.ToInt32(dt.Rows[i]["Weekend2"]);
                selectedCountry.HolidayName = dt.Rows[i]["HolidayName"].ToString();
                selectedCountry.Date = Convert.ToDateTime(dt.Rows[i]["Date"]);
                selectedCountryList.Add(selectedCountry);
            }
            connection.Close();

            return (selectedCountryList);
        }
    }
}
