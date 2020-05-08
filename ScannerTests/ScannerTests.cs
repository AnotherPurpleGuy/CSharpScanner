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
        public void Test1()
        {
            Scanner scanner = new Scanner("test");
            Assert.Pass();
        }
    }
}