using NUnit.Framework;
using ScannerUtil;

namespace ScannerTests
{
    public class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenStringConstructor()
        {
            Scanner scanner = new Scanner("test");
            Assert.Pass();
        }

        [Test]
        public void GivenStringNextLine()
        {
            Scanner scanner = new Scanner("this is \n a test to check \t that the correct\v values are been returned\r all values");
            Assert.AreEqual("this is ", scanner.nextLine());
            Assert.AreEqual(" a test to check ", scanner.nextLine());
            Assert.AreEqual(" that the correct", scanner.nextLine());
            Assert.AreEqual(" values are been returned", scanner.nextLine());
            Assert.AreEqual(" all values", scanner.nextLine());
        }
    }
}