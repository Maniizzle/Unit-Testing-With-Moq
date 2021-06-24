using CreditCardApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestApp.Infrastructure
{
    public class CreditCardApplicationRepository : ICreditCardApplicationRepository
    {
        public Task AddAsync(CreditCardApplication creditCardApplication)
        {
            return Task.CompletedTask;
        }
    }
}
