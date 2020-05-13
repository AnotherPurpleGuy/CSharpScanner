using NUnit.Framework;

using Scanners;
using Scanners.Exceptions;

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
            Scanner scanner = new StringScanner("test");
            Assert.Pass();
        }

        [Test]
        public void GivenString_Constructor_Exceptions()
        {
            try
            {
                Scanner scanner = new StringScanner("");
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
            Scanner scanner = new StringScanner("this is \n\n a test to check \t\t that the correct\v values are been returned\r all values");
            Assert.AreEqual("this is ", scanner.NextLine());
            Assert.AreEqual(" a test to check ", scanner.NextLine());
            Assert.AreEqual(" that the correct", scanner.NextLine());
            Assert.AreEqual(" values are been returned", scanner.NextLine());
            Assert.AreEqual(" all values", scanner.NextLine());
        }

        [Test]
        public void GivenString_NextLine_Exceptions()
        {
            Scanner scanner = new StringScanner("exception");
            string _tmp = scanner.NextLine();

            try
            {
                _tmp = scanner.NextLine();
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

            Scanner scanner = new StringScanner("next line\n look ahead\n test");
            Assert.IsTrue(scanner.HasNextLine());

            _tmp = scanner.NextLine();
            Assert.IsTrue(scanner.HasNextLine());

            _tmp = scanner.NextLine();
            Assert.IsFalse(scanner.HasNextLine());
        }


        [Test]
        public void GivenString_NextInt()
        {
            Scanner scanner = new StringScanner("14,39 this is dead text 92 \n 502 :;\t 2147483647, -15");
            Assert.That(scanner.NextInt(), Is.EqualTo(14));
            Assert.That(scanner.NextInt(), Is.EqualTo(39));
            Assert.That(scanner.NextInt(), Is.EqualTo(92));
            Assert.That(scanner.NextInt(), Is.EqualTo(502));
            Assert.That(scanner.NextInt(), Is.EqualTo(2147483647));
            Assert.That(scanner.NextInt(), Is.EqualTo(-15));
        }

        [Test]
        public void GivenString_NextInt_Exceptions()
        {
            Scanner scanner = new StringScanner("53");
            int _tmp = scanner.NextInt();

            try
            {
                _tmp = scanner.NextInt();
                Assert.Fail();
            } catch (NoMoreDataException e)
            {
                Assert.That(e.Message, Is.EqualTo("There is no more lines left to return"));
            }

            scanner = new StringScanner("test");

            try
            {
                _tmp = scanner.NextInt();
                Assert.Fail("This call was suppose to throw and exception");
            } catch (NoMatchFoundException e)
            {
                Assert.That(e.Message, Is.EqualTo("There was no integer found in the remaining string"));
            }
        }

        [Test]
        public void GivenString_HasNextInt()
        {
            int _tmp;

            Scanner scanner = new StringScanner("15,35,-37");
            Assert.True(scanner.HasNextInt());

            _tmp = scanner.NextInt();
            Assert.True(scanner.HasNextInt());

            _tmp = scanner.NextInt();
            Assert.False(scanner.HasNextInt());
        }

        [Test]
        public void Given_String_NextDouble()
        {
            Scanner scanner = new StringScanner("this is a 153.923 ,15.1 25.  15 spacing ; 0.61 \n -25.43");
            Assert.That(scanner.NextDouble(), Is.EqualTo(153.923));   
            Assert.That(scanner.NextDouble(), Is.EqualTo(15.1));   
            Assert.That(scanner.NextDouble(), Is.EqualTo(0.61));   
            Assert.That(scanner.NextDouble(), Is.EqualTo(-25.43));   
        }

        [Test]
        public void Given_String_HasNextDouble()
        {
            double _tmp;

            Scanner scanner = new StringScanner("14.550 93.14 7509.13");
            Assert.True(scanner.HasNextDouble());

            _tmp = scanner.NextDouble();
            Assert.True(scanner.HasNextDouble());

            _tmp = scanner.NextDouble();
            Assert.False(scanner.HasNextDouble());
        }

        [Test]
        public void GivenString_NextDouble_Exceptions()
        {
            Scanner scanner = new StringScanner("15.95");
            double _tmp = scanner.NextDouble();

            try
            {
                _tmp = scanner.NextDouble();
                Assert.Fail();
            } catch (NoMoreDataException e)
            {
                Assert.That(e.Message, Is.EqualTo("There is no more lines left to return"));
            }

            scanner = new StringScanner("test");

            try
            {
                _tmp = scanner.NextDouble();
                Assert.Fail("This call was suppose to throw and exception");
            } catch (NoMatchFoundException e)
            {
                Assert.That(e.Message, Is.EqualTo("There was no double found in the remaining string"));
            }
        }

        [Test]
        public void Given_String_mulitNext()
        {
            Scanner scanner = new StringScanner("this, a number 1 test of multiple \n 666 remainding line \n different next calls 79 \t addition of a double 93.61");
            Assert.That(scanner.NextLine(), Is.EqualTo("this, a number 1 test of multiple "));
            Assert.That(scanner.NextInt(), Is.EqualTo(666));
            Assert.That(scanner.NextLine(), Is.EqualTo(" remainding line "));
            Assert.That(scanner.NextInt(), Is.EqualTo(79));
            Assert.That(scanner.NextDouble(), Is.EqualTo(93.61));
        }
    }
}