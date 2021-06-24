using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestApp.ViewModels
{
    public class NewCreditCardApplicationDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public decimal? GrossAnnualIncome { get; set; }
        public string FrequentFlyerNumber { get; set; }

    }
}
