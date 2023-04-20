using DeviationBaisExperiment;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSUnitTest
{
    [TestClass]
    public class ArithmeticTests
    {
        static int[] odd_ds_sample = new[] { 1, 2, 3, 4, 5 };
        static int[] even_ds_sample = new[] { 1, 2, 3, 4, 5, 6 };

        [TestMethod]
        public void StandardDeviationBiasesTest()
        {
            var result = Arithmetic.StandardDeviationBiases(odd_ds_sample);
            Assert.AreEqual(Math.Round(result.withBias, 7), 1.5811388);
            Assert.AreEqual(Math.Round(result.withoutBias, 7), 1.4142136);
        }

        [TestMethod]
        public void VarianceTest()
        {
            Assert.AreEqual(Math.Round(Arithmetic.Variance(odd_ds_sample),
                2), 2);
        }

        [TestMethod]
        public void StandardDeviationTest()
        {
            Assert.AreEqual(Math.Round(Arithmetic.StandardDeviation(odd_ds_sample),
                7), 1.4142136);
        }

        [TestMethod]
        public void CalculateStatisticsTest_Odd()
        {
            var odd_ds_statistics = Arithmetic.CalculateStatistics(odd_ds_sample);

            Assert.AreEqual(odd_ds_statistics.mean, 3);
            Assert.AreEqual(odd_ds_statistics.median, 3);
            Assert.AreEqual(odd_ds_statistics.mode, 1);
            Assert.AreEqual(odd_ds_statistics.range, 4);
            Assert.AreEqual(odd_ds_statistics.IQR, 3);
            Assert.AreEqual(odd_ds_statistics.Q1, 1.5);
            Assert.AreEqual(odd_ds_statistics.Q2, 3);
            Assert.AreEqual(odd_ds_statistics.Q3, 4.5);
        }

        [TestMethod]
        public void CalculateStatisticsTest_Even()
        {
            var even_ds_statistics = Arithmetic.CalculateStatistics(even_ds_sample);

            Assert.AreEqual(even_ds_statistics.mean, 3.5);
            Assert.AreEqual(even_ds_statistics.median, 3.5);
            Assert.AreEqual(even_ds_statistics.mode, 1);
            Assert.AreEqual(even_ds_statistics.range, 5);
            Assert.AreEqual(even_ds_statistics.IQR, 3);
            Assert.AreEqual(even_ds_statistics.Q1, 2);
            Assert.AreEqual(even_ds_statistics.Q2, 3.5);
            Assert.AreEqual(even_ds_statistics.Q3, 5);
        }
    }
}