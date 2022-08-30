using PenaltyCalculator.Classes;
using PenaltyCalculator.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.BusinessLayer
{
    public class Calculations : ICalculations
    {
        
        //Dependency injection   
        ISqlDataBase sqlData;

        public Calculations(ISqlDataBase sqlDataBase)
        {
            sqlData = sqlDataBase;
        }
        
        //Function to get data from DL to send it to controller

        public List<Country> GetCountriesData()
        {
            List<Country> countryData = sqlData.GetCountries();
            return countryData;
        }
        
        // Business Days and penalty calculation Function

        public Results FinalCalculations(UserData inputs)
        {
            DateTime checkoutDate = Convert.ToDateTime(inputs.CheckoutDate);
            DateTime returnDate = Convert.ToDateTime(inputs.ReturnDate);
            int countryId = inputs.Id;
            
            // Function to get Complete data of selected country 

            List<selectedCountry> selectedCountry = sqlData.SelectedCountryData(countryId);

            // Store list of national Holidays of selected country coming from DB

            List<DateTime> nationalHolidays = selectedCountry.Select(c => c.Date).ToList();

            // DayofWeek Enumeration
            int holiday1 = selectedCountry[0].Weekend1;
            int holiday2 = selectedCountry[0].Weekend2;

            
            int sYear = checkoutDate.Year;
            int fYear = returnDate.Year;
            int totalyears = fYear - sYear + 1;

            TimeSpan span = returnDate - checkoutDate;
            int totalDays = span.Days + 1;
            int totalBusinessDays = 0;

            List<DateTime> totalNationalHolidays = new List<DateTime>();
            
            // Get list of national Holidays of selected country against selected years
            for (int i = 0; i < totalyears; i++)
            {
                for (int j = 0; j < nationalHolidays.Count; j++)
                {
                    totalNationalHolidays.Add(new DateTime(sYear + i, nationalHolidays[j].Month, nationalHolidays[j].Day));
                }
            }
            
            // Calculate Business Days
            for (int count = 0; count < totalDays; count++)
            {
                DateTime sdate = checkoutDate.AddDays(count);
                int dayofweek = (int)sdate.DayOfWeek;

                if (dayofweek == holiday1 || dayofweek == holiday2)
                {
                    continue;
                }
                else if (totalNationalHolidays.Contains(sdate))
                {
                    continue;
                }
                else
                {
                    totalBusinessDays++;
                }
            }

            Results result = new Results();
            result.BusinessDays = totalBusinessDays;
            
            //Penalty Calculations
            if (totalBusinessDays <= 10)
            {
                result.TotalPenalty = "No Penalty";
            }
            else
            {
                decimal penalty = (totalBusinessDays - 10) * selectedCountry[0].PenaltyPrice;
                decimal tax = penalty * (selectedCountry[0].TaxPenalty) / 100;
                decimal totalPenalty = penalty + tax;
                result.TotalPenalty = ("Total penalty is " + totalPenalty + " " + selectedCountry[0].CurrencyCode);
            }
            return result;
        }
    }
}
