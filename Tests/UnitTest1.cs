using System;
using System.Collections.Generic;
using Tracer;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckOneMethodResult()
        {
            //Arrange
            var tracer = new Tracer.Tracer();
            List<ThreadInfo> result;
            //Act
            tracer.StartTrace();
            tracer.StopTrace();
            result = tracer.GetTraceResult();
            //Assert
            Assert.True(result.Count ==1 && result[0].ThreadMethods.Count == 1);
        }
        [Fact]
        public void CheckTwoMethodResult()
        {
            
            //Arrange
            var tracer = new Tracer.Tracer();
            List<ThreadInfo> result;
            //Act
            tracer.StartTrace();
            SomeMethod(tracer);
            tracer.StopTrace();
            result = tracer.GetTraceResult();

            //Assert
            Assert.NotNull(result[0].ThreadMethods[0].Children[0].Children);
        }
        public void SomeMethod(ITracer tracer)
        {
            tracer.StartTrace();
            int x = 1;
            x += 3;
            tracer.StopTrace();
        }

    }
}