using CreditCardApplications;
using System;
using Xunit;
using Moq;
//allow us to mock protected man
using Moq.Protected;
using System.Collections.Generic;

namespace CreditCardApplications.Test
{
    public class CreditCardAllicationEvaluatesShould
    {

        private Mock<IFrequentFlyerNumberValidator> mockValidator;
        private CreditCardApplicationEvaluator sut;

        //test willl create a new instance for every test scenario
        public CreditCardAllicationEvaluatesShould()
        {
            mockValidator=new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.SetupAllProperties();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        
             this.sut = new CreditCardApplicationEvaluator(mockValidator.Object);

        }


        [Fact]
        public void AcceptHighIncomeApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(); 
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }


        [Fact]
        public void ReferYoungApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
          
            mockValidator.DefaultValue = DefaultValue.Mock;
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

                var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

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

            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
             var application = new CreditCardApplication { GrossAnnualIncome = 19_999,
            Age=42,FrequentFlyerNumber="x"};

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferInvalidFrequentApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            
            //mockValidator.DefaultValue = DefaultValue.Mock;
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);


           // var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }
        
        [Fact]
        public void DeclareLowIncomeApplication()
        {
            //Commented so we could use the one in the constructor
            // Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            // mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

            bool isValid = true;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>(),out isValid));

            //Commented so we could use the one in the constructor
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        //[Fact]
        //public void DeclineLowIncomeApplicationOutDemo()
        //{
        //    Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

        //    bool isValid = true;
        //    mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

        //     var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
        //    var application = new CreditCardApplication { Age = 42, GrossAnnualIncome=19_999 };

        //    CreditCardApplicationDecision decision = sut.EvaluateUsingOut(application);
        //    Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);

        //}

        [Fact]
        public void ReferWhenLicenseKeyExpired()
        {
            // Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

            //var mockLicenseData = new Mock<ILicenseData>();
            //mockLicenseData.Setup(x => x.LicenseKey).Returns("EXPIRED");

            //var mockServiceInfo = new Mock<IServiceInformation>();
            //mockServiceInfo.Setup(x => x.License).Returns(mockLicenseData.Object);

            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation).Returns(mockServiceInfo.Object);

            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("EXPIRED");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 42 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision); 
        }

      string  GetLicenseKeyExpiryString()
        {
            return "EXPIRED";
        }

        [Fact]
        public void UseDetailedLookupForOlderApplications()
        {
            // Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

            //to make the mockup remember changes made to the property
            //call it before making any setup
            //  mockValidator.SetupProperty(x => x.ValidationMode);

            //Commented so we could use the one in the constructor
            //mockValidator.SetupAllProperties();
            // mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            // var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 30 };

           sut.Evaluate(application);
            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }
        [Fact]
        public void ValidateFrequentFlyerNumberForLowIncomeApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
           // mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
          //  var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { FrequentFlyerNumber="q"};

            sut.Evaluate(application);

            //verify if isvalid was called with a value of null
            //mockValidator.Verify(x => x.IsValid(null));
            //verify if isvalid was called with any value
            //mockValidator.Verify(x => x.IsValid(It.IsAny<string>()));
            
            //verify if isvalid was called with value "q" and passing custom message
           mockValidator.Verify(x => x.IsValid("q"),"Frequent flyer numbers should bee validated");
           
            //verify if IsValid method was called once
           // mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);

        }
        [Fact]
        public void NotValidateFrequentFlyerNumberForHighIncomeApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
           
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            sut.Evaluate(application);
            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);

            //verify if isvalid was called with a value of null
            //mockValidator.Verify(x => x.IsValid(null));
            //verify if isvalid was called with any value
            //mockValidator.Verify(x => x.IsValid(It.IsAny<string>()));

            //verify if isvalid was called with value "q" and passing custom message
            //mockValidator.Verify(x => x.IsValid("q"), "Frequent flyer numbers should bee validated");
        }

        [Fact]
        public void CheckLicenseKeyForLowIncomeApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { GrossAnnualIncome = 99_000 };

            sut.Evaluate(application);
            //verify that a property  was read
            //because the Gross Annual Income was Less,it got to the place where Licensekey would be read
            mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey);//  Times.Never);

            }

        [Fact]
        public void SetDetailedLookUpFOrOlderApplications()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { Age = 30 };

            sut.Evaluate(application);
            //verify that a property was set to validtaion.Detailed
            mockValidator.VerifySet(x => x.ValidationMode=ValidationMode.Detailed);//  Times.Never);
             //verify that a property was set 
            //mockValidator.VerifySet(x => x.ValidationMode=It.IsAny<ValidationMode>());//  Times.Never);

        }


        [Fact]
        public void ReferWhenFrequentFlyerValidationError()
        {
            //Commented so we could use the one in the constructor
           // Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Throws<Exception>();

            //Commented so we could use the one in the constructor
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { Age = 42 };

           var decision= sut.Evaluate(application);
            //verify that a property was set to validtaion.Detailed
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,decision);//  Times.Never);
                                                                                     //verify that a property was set 
                                                                                     //mockValidator.VerifySet(x => x.ValidationMode=It.IsAny<ValidationMode>());//  Times.Never);

        }


        [Fact]
        public void IncrementLookUpCount()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>()))
                .Returns(true)
                .Raises(x=>x.ValidatorLookUpPerformed+=null,EventArgs.Empty);


            //Commented so we could use the one in the constructor
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { Age = 25,FrequentFlyerNumber="x" };

            var decision = sut.Evaluate(application);
            
            //raise event
            //mockValidator.Raise(x => x.ValidatorLookUpPerformed += null, EventArgs.Empty);
            Assert.Equal(1, sut.ValidatorLookUpCount);//  Times.Never);
                                                                                  //verify that a property was set 
                                                                                  //mockValidator.VerifySet(x => x.ValidationMode=It.IsAny<ValidationMode>());//  Times.Never);

        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications_ReturnValuesSequence()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
          
            //will return several sequential value
            mockValidator.SetupSequence(x => x.IsValid(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);

            //Commented so we could use the one in the constructor
            //var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { Age = 25};

            //first return value
            CreditCardApplicationDecision firstDecision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, firstDecision);
             
            //second return value
            CreditCardApplicationDecision secondDecision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, secondDecision);
                
        }


        [Fact]
        public void ReferInvalidFrequentFlyerApplications_MultipleCallSequence()
        {

            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var frequentFlyNumbersPassed = new List<string>();
            //will return several sequential value
            mockValidator.SetupSequence(x => x.IsValid(Capture.In(frequentFlyNumbersPassed)));

            //Commented so we could use the one in the constructor
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
           
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { Age = 25,FrequentFlyerNumber="aa" };
            var application2 = new CreditCardApplication { Age = 25,FrequentFlyerNumber="bb" };
            var application3 = new CreditCardApplication { Age = 25,FrequentFlyerNumber="cc" };

            //first return value
             sut.Evaluate(application);
             sut.Evaluate(application2);
             sut.Evaluate(application3);

            //second return value
            Assert.Equal(new List<string> {"aa","bb","cc" }, frequentFlyNumbersPassed);

        }


        [Fact]
        public void ReferFraudRisk()
        {
            //Commented so we could use the one in the constructor
            //Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            Mock<FraudLookUp> mockFraudLookUp = new Mock<FraudLookUp>();


            //Commented so we could use the one in the constructor
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            
            //mockFraudLookUp.Setup(x => x.IsFraudRisk(It.IsAny<CreditCardApplication>())).Returns(true);

            mockFraudLookUp.Protected().Setup<bool>("CheckApplication",ItExpr.IsAny<CreditCardApplication>()).Returns(true);


           // var frequentFlyNumbersPassed = new List<string>();
           // mockValidator.SetupSequence(x => x.IsValid(Capture.In(frequentFlyNumbersPassed))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object, mockFraudLookUp.Object);
            var application = new CreditCardApplication();
            CreditCardApplicationDecision decision = sut.Evaluate(application);

             Assert.Equal(CreditCardApplicationDecision.ReferredToHumanFraudRisk, decision);
        }

        [Fact]
        public void LinqToMocks()
        {
          IFrequentFlyerNumberValidator mockValidator= Mock.Of<IFrequentFlyerNumberValidator>(
                validator=> validator.ServiceInformation.License.LicenseKey=="OK" &&
                validator.IsValid(It.IsAny<string>())==true
            );
            var sut = new CreditCardApplicationEvaluator(mockValidator);
            //var application = new CreditCardApplication();
            var application = new CreditCardApplication { Age = 25, FrequentFlyerNumber = "x" };

            var decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
           
        }

    }
}
