using PenaltyCalculator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.DataLayer
{
    // Interface of SqlDataBase Class
    public interface ISqlDataBase
    {
        List<Country> GetCountries();

        List<selectedCountry> SelectedCountryData(int countryId);
    }
}
