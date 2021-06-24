using CreditCardApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestApp.Infrastructure
{
   public interface ICreditCardApplicationRepository
    {
       Task AddAsync(CreditCardApplication creditCardApplication);
    }
}
