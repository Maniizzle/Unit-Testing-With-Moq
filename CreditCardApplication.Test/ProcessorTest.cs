using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CreditCardApplications.Test
{
   public class ProcessorTest
    {

        [Fact]
        public void ProcessorShouldExecute()
        {
            Mock<IGateway> mockValidator = new Mock<IGateway>(MockBehavior.Strict);
            var person = new Person();
            mockValidator.Setup(x => x.Execute(ref person)).Returns(-1); 
           
            var sut = new Processor(mockValidator.Object);
            var result=sut.Process(person);

            Assert.True(result);
        }
        [Fact]
        public void ProcessorShouldMatchAnyCompatibleType()
        {
            Mock<IGateway> mockValidator = new Mock<IGateway>(MockBehavior.Strict);
            var person = new Person();
            var person2 = new Person();
            mockValidator.Setup(x => x.Execute(ref It.Ref<Person>.IsAny)).Returns(-1);

            var sut = new Processor(mockValidator.Object);
            var result = sut.Process(person);
            var result2 = sut.Process(person2);

            Assert.Equal(result, result2);

            Assert.True(result);
        }

    }
}
