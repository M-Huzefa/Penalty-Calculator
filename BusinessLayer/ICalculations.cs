using PenaltyCalculator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.BusinessLayer
{
    // Interface of Calculations Class
    public interface ICalculations
    {
        List<Country> GetCountriesData();

        Results FinalCalculations(UserData inputs);
    }
}
