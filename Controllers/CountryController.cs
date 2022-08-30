using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PenaltyCalculator.BusinessLayer;
using PenaltyCalculator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CountryController : ControllerBase
    {
    
        // Dependency injection
        ICalculations calculation;
        public CountryController(ICalculations calculations)
        {
            calculation = calculations;
        }
        
        // Get request to get Countries Data from DB

        [Route("CountryData")]
        [HttpGet]

        public List<Country> GetCountriesData()
        {
            List<Country> countrydata = calculation.GetCountriesData();
            return countrydata;
        }
        
        // post request to get user input and return results

        [Route("CalculatedAmount")]
        [HttpPost]

        public Results Calculations([FromBody] UserData input)
        {
            Results results = calculation.FinalCalculations(input);
            return results;
        }

    }
}
