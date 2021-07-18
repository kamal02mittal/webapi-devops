using NUnit.Framework;
using WebApplication2;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAdd()
        {
            Calculator cal = new Calculator();
            Assert.AreEqual(cal.Add(3,6), 9);
        }

        [Test]
        public void TestMul()
        {
            Calculator cal = new Calculator();
            Assert.AreEqual(cal.Mul(3, 6), 18);
        }

        [Test]
        public void TestSub()
        {
            Calculator cal = new Calculator();
            Assert.AreEqual(cal.Sub(9, 2), 7);
        }
    }
}