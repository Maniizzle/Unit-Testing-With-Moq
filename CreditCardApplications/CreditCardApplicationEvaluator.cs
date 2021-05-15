namespace CreditCardApplications
{
    public class CreditCardApplicationEvaluator
    {
        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator)
        {
            this.validator = validator;
        }

        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;
        private readonly IFrequentFlyerNumberValidator validator;

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            if (validator.ServiceInformation.License.LicenseKey == "EXPIRED")
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }
            //if (validator.LicenseKey == "EXPIRED")
            //{
            //    return CreditCardApplicationDecision.ReferredToHuman;
            //}

            var isValidFreuentFlyMember = validator.IsValid(application.FrequentFlyerNumber);
            if (!isValidFreuentFlyMember)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }
            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }
        
        //public CreditCardApplicationDecision EvaluateUsingOut(CreditCardApplication application)
        //{
        //    if (application.GrossAnnualIncome >= HighIncomeThreshold)
        //    {
        //        return CreditCardApplicationDecision.AutoAccepted;
        //    }

        //    validator.IsValid(application.FrequentFlyerNumber, out var isValiFrequentFlyNumber);

        //    if (!isValiFrequentFlyNumber)
        //    {
        //        return CreditCardApplicationDecision.ReferredToHuman;
        //    }
        //    if (application.Age <= AutoReferralMaxAge)
        //    {
        //        return CreditCardApplicationDecision.ReferredToHuman;
        //    }

        //    if (application.GrossAnnualIncome < LowIncomeThreshold)
        //    {
        //        return CreditCardApplicationDecision.AutoDeclined;
        //    }
        //    return CreditCardApplicationDecision.ReferredToHuman;

        //}



    }
}
