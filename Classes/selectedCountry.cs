using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Classes
{
    public class selectedCountry
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int TaxPenalty { get; set; }
        public decimal PenaltyPrice { get; set; }
        public string CurrencyCode { get; set; }
        public int Weekend1 { get; set; }
        public int Weekend2 { get; set; }
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }

        public selectedCountry() { }

        public selectedCountry(int countryid, string countryname, int taxPenalty, decimal penaltyPrice, string currencyCode, int weekend1, int weekend2, string holidayName, DateTime date)
        {
            CountryId = countryid;
            CountryName = countryname;
            TaxPenalty = taxPenalty;
            PenaltyPrice = penaltyPrice;
            CurrencyCode = currencyCode;
            Weekend1 = weekend1;
            Weekend2 = weekend2;
            HolidayName = holidayName;
            Date = date;
        }
    }
}
