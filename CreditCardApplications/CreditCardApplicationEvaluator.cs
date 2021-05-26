﻿namespace CreditCardApplications
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

        public int ValidatorLookUpCount { get;private set; }
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

            validator.ValidationMode = application.Age >= 30 ? ValidationMode.Detailed : ValidationMode.Quick;
            //if (validator.LicenseKey == "EXPIRED")
            //{
            //    return CreditCardApplicationDecision.ReferredToHuman;
            //}
            bool isValidFreuentFlyMember;
            try
            {
                isValidFreuentFlyMember = validator.IsValid(application.FrequentFlyerNumber);

            }
            catch (System.Exception)
            {

                return CreditCardApplicationDecision.ReferredToHuman;
            }
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
