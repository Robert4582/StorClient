using NUnit.Framework;

namespace ClientTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdditionWorks()
        {
            //Arrange
            int a = 2;
            int b = 4;

            int expected = 6;

            //Act
            int actual = Client.MainWindow.Add(a, b);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SubtractionDoesnt()
        {
            //Arrange
            int a = 2;
            int b = 4;

            int expected = -2;

            //Act
            int actual = Client.MainWindow.Subtract(a, b);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}