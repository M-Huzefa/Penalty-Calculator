using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Classes
{
    public class UserData
    {
        public string CheckoutDate { get; set; }
        public string ReturnDate { get; set; }
        public int Id { get; set; }

        //public UserData(DateTime checkoutDate, DateTime returnDate, int id)
        //{
        //    CheckoutDate = checkoutDate;
        //    ReturnDate = returnDate;
        //    Id= id;
        //}
    }
}
