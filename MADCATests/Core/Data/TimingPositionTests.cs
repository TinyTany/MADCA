using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MADCA.Core.Data.Tests
{
    [TestClass()]
    public class TimingPositionTests
    {
        [TestMethod()]
        public void TimingPositionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TimingPositionTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            var lhs = new TimingPosition(2, 1);
            var rhs = new TimingPosition(3, 1);
            var sum = new TimingPosition(6, 5);
            Assert.IsTrue(lhs > rhs);
            Assert.IsTrue(lhs >= rhs);
            Assert.IsTrue(lhs != rhs);
            Assert.IsFalse(lhs < rhs);
            Assert.IsFalse(lhs <= rhs);
            Assert.IsFalse(lhs == rhs);
            Assert.IsTrue(lhs + rhs == sum);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Assert.Fail();
        }
    }
}