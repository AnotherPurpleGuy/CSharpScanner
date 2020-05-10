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
            Scanner scanner = new Scanner("this is \n\n a test to check \t\t that the correct\v values are been returned\r all values");
            Assert.AreEqual("this is ", scanner.nextLine());
            Assert.AreEqual(" a test to check ", scanner.nextLine());
            Assert.AreEqual(" that the correct", scanner.nextLine());
            Assert.AreEqual(" values are been returned", scanner.nextLine());
            Assert.AreEqual(" all values", scanner.nextLine());
        }

        [Test]
        public void GivenString_NextLine_Exceptions()
        {
            Scanner scanner = new Scanner("exception");
            string _tmp = scanner.nextLine();

            try
            {
                _tmp = scanner.nextLine();
                Assert.Fail();
            } catch (NoMoreDataException e)
            {
                Assert.That(e.Message, Is.EqualTo("There is no more lines left to return"));
                Assert.Pass();
            }
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


        [Test]
        public void GivenString_NextInt()
        {
            Scanner scanner = new Scanner("14,39  92 \n 502 :;\t 2147483647, -15");
            Assert.That(scanner.nextInt(), Is.EqualTo(14));
            Assert.That(scanner.nextInt(), Is.EqualTo(39));
            Assert.That(scanner.nextInt(), Is.EqualTo(92));
            Assert.That(scanner.nextInt(), Is.EqualTo(502));
            Assert.That(scanner.nextInt(), Is.EqualTo(2147483647));
            Assert.That(scanner.nextInt(), Is.EqualTo(-15));
        }

        [Test]
        public void GivenString_NextInt_Exceptions()
        {
            Assert.Fail();
        }

        [Test]
        public void GivenString_HasNextInt()
        {
            Assert.Fail();
        }

    }
}