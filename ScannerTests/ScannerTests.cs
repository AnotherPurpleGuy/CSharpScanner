using NUnit.Framework;

using ScannerUtil;
using ScannerUtil.Exceptions;

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
        public void GivenString_Constructor_Exceptions()
        {
            try
            {
                Scanner scanner = new Scanner("");
                Assert.Fail();
            } catch (InvalidArgumentException e)
            {
                Assert.That(e.Message, Is.EqualTo("Empty string was handed to constructor"));
                Assert.Pass();
            }

        }

        [Test]
        public void GivenString_NextLine()
        {
            Scanner scanner = new Scanner("this is \n a test to check \t that the correct\v values are been returned\r all values");
            Assert.AreEqual("this is ", scanner.nextLine());
            Assert.AreEqual(" a test to check ", scanner.nextLine());
            Assert.AreEqual(" that the correct", scanner.nextLine());
            Assert.AreEqual(" values are been returned", scanner.nextLine());
            Assert.AreEqual(" all values", scanner.nextLine());
        }

        [Test]
        public void GivenString_HasNextLine()
        {
            string _tmp;

            Scanner scanner = new Scanner("next line\n look ahead\n test");
            Assert.IsTrue(scanner.hasNextLine());

            _tmp = scanner.nextLine();
            Assert.IsTrue(scanner.hasNextLine());

            _tmp = scanner.nextLine();
            Assert.IsFalse(scanner.hasNextLine());
        }
    }
}