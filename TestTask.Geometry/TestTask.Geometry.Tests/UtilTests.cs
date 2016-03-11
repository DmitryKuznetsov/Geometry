namespace TestTask.Geometry.RightAngledTriangle.Tests
{
    using System;

    using NBehave.Spec.NUnit;

    using NUnit.Framework;

    [TestFixture]
    public class UtilTests
    {
        private const double cathetus1 = 10;

        private const double cathetus2 = 8;

        private double hypotenuse = Math.Sqrt(cathetus1 * cathetus1 + cathetus2 * cathetus2);

        private double area = cathetus1 * cathetus2 / 2;

        [Test]
        public void CorrectArgumentsTest()
        {
            Util.Area(cathetus1, cathetus2, hypotenuse).ShouldEqual(area);
        }

        [Test]
        public void ToleranceTest()
        {
            Extensions.ShouldThrow<ArgumentException>(
                () =>
                Util.Area(
                    cathetus1, 
                    cathetus2, 
                    hypotenuse + Util.Tolerance));
        }

        [Test]
        public void PositiveInfinityTest()
        {
            double.IsPositiveInfinity(
                Util.Area(
                    double.PositiveInfinity, 
                    double.PositiveInfinity, 
                    double.PositiveInfinity)).ShouldBeTrue();
        }

        [Test]
        [Combinatorial]
        public void WrongArgumentsTest(
            [Values(double.MinValue, double.NegativeInfinity, double.NaN)] double a, 
            [Values(double.NegativeInfinity, double.NaN)] double b, 
            [Values(double.MinValue, double.NegativeInfinity, double.NaN)] double c)
        {
            Extensions.ShouldThrow<ArgumentOutOfRangeException>(() => Util.Area(a, b, c));
        }

        [Test]
        public void WrongHypotenuseTest()
        {
            Extensions.ShouldThrow<ArgumentException>(
                () => Util.Area(cathetus1, cathetus2, hypotenuse + 1));
        }

        [Test]
        public void ZeroAreaTest()
        {
            Util.Area(double.Epsilon, double.Epsilon, double.Epsilon).ShouldEqual(0);
        }
    }
}