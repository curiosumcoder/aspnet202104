using System;
using Xunit;

namespace WA90.Tests
{
    public class Hello_Tests
    {
        [Fact]
        public void SayHelloWorld()
        {
            // Arrange
            bool variableBooleana;

            // Act
            variableBooleana = true;

            // Assert
            Assert.True(variableBooleana, "No es verdadero");
        }
    }
}
