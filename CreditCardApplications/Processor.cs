using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications
{
   public class Processor
    {
        private readonly IGateway gateway;

        public Processor(IGateway gateway)
        {
            this.gateway = gateway;
        }

        public bool Process(Person person)
        {
            int returnCode = gateway.Execute(ref person);
            return returnCode == 0;
        }
    }
}
