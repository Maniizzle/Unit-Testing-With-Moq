using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications
{
   public class FraudLookUp
    {
        //virtual bcuz partial mock method needs to be virtual
       // public virtual bool IsFraudRisk(CreditCardApplication application)
        
        public  bool IsFraudRisk(CreditCardApplication application)
        {
            
            return CheckApplication(application);
        }

        protected virtual bool CheckApplication(CreditCardApplication application)
        {
            if (application.LastName == "Smith")
            {
                return true;
            }
            return false;
        }
    }
}
