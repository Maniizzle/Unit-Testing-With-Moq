using CreditCardApplications;
using System;
using Xunit;
using Moq;

namespace CreditCardApplications.Test
{
    public class CreditCardAllicationEvaluatesShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(); 
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }


        [Fact]
        public void ReferYoungApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
              mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);


            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            
            //setting the isvalid method
           // mockValidator.Setup(x => x.IsValid("x")).Returns(true);
          
            //Argument Matching
            //return true when any string is passed to the method
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            
            //return true to a particular scenario
           // mockValidator.Setup(x => x.IsValid(It.Is<string>(numb=>numb.StartsWith("y")))).Returns(true);
          
            //return true if it is in a range
            //mockValidator.Setup(x => x.IsValid(It.IsInRange<string>("a","z",Moq.Range.Inclusive))).Returns(true);

            //return true if it is in a range
            //mockValidator.Setup(x => x.IsValid(It.IsIn<string>("z", "y", "x"))).Returns(true);

            //return true if it satisfies the regex
            mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]"))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
             var application = new CreditCardApplication { GrossAnnualIncome = 19_999,
            Age=42,FrequentFlyerNumber="x"};

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferInvalidFrequentApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);


            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }
        
        [Fact]
        public void DeclareLowIncomeApplication()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

            bool isValid = true;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>(),out isValid));
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        //[Fact]
        //public void ReferYoungApplications()
        //{
        //    Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
        //    mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

        //    var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
        //    var application = new CreditCardApplication { Age = 19 };

        //    CreditCardApplicationDecision decision = sut.Evaluate(application);
        //    Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        //}
    }
}
