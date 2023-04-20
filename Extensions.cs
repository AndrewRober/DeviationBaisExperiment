namespace DeviationBaisExperiment
{
    public static class Arithmetic
    {
        public static double Median(this IEnumerable<int> source)
        {
            int count = source.Count();
            if (count == 0)
                throw new InvalidOperationException("Empty collection");

            int middleIndex = count / 2;
            var sorted = source.OrderBy(value => value);
            return count % 2 == 0 ? (sorted.ElementAt(middleIndex - 1) +
                sorted.ElementAt(middleIndex)) / 2.0 : sorted.ElementAt(middleIndex);
        }

        public static double Mode(this int[] source)
        {
            int modeCount = 0, currentCount = 1, mode = 0;
            for (int i = 1; i < source.Length; i++)
            {
                if (source[i] == source[i - 1])
                    currentCount++;
                else
                {
                    if (currentCount > modeCount)
                    {
                        modeCount = currentCount;
                        mode = source[i - 1];
                    }
                    currentCount = 1;
                }
            }
            if (currentCount > modeCount)
            {
                modeCount = currentCount;
                mode = source[source.Length - 1];
            }
            return mode;
        }

        public static (double Q1, double Q2, double Q3) CalculateQuartiles(int[] sortedData)
        {
            if (sortedData.Length == 0)
            {
                throw new ArgumentException("Data must contain at least one element");
            }

            if (sortedData.Length % 2 == 0)
            {
                return (CalculateQuartile(sortedData.Take(sortedData.Length / 2).ToArray()),
                    CalculateQuartile(sortedData), CalculateQuartile(sortedData.TakeLast(sortedData.Length / 2).ToArray()));
            }
            else
            {
                return (CalculateQuartile(sortedData.Take((sortedData.Length - 1) / 2).ToArray()),
                    CalculateQuartile(sortedData), CalculateQuartile(sortedData.TakeLast((sortedData.Length - 1) / 2).ToArray()));
            }
        }

        public static double CalculateQuartile(int[] sortedData)
        {
            if (sortedData.Length == 0)
            {
                throw new ArgumentException("Data must contain at least one element");
            }

            int mid = sortedData.Length / 2;
            return sortedData.Length % 2 == 0 ? (sortedData[mid - 1] + sortedData[mid]) / 2.0 : sortedData[mid];
        }

        public static (double withoutBias, double withBias) StandardDeviationBiases(int[] data)
        {
            double sumOfSquaredDeviations = data.AsParallel().Sum(value => Math.Pow(value - (data.Sum() / data.Length), 2));
            return (Math.Sqrt(sumOfSquaredDeviations / (data.Length - 1)),
                Math.Sqrt(sumOfSquaredDeviations / (data.Length)));
        }

        public static double StandardDeviation(int[] data) => Math.Sqrt(Variance(data));

        public static double Variance(int[] data) =>
            data.AsParallel().Sum(value => Math.Pow(value - (data.Sum() / data.Length), 2)) / (data.Length);
        public static double VarianceWithoutBias(int[] data) =>
            data.AsParallel().Sum(value => Math.Pow(value - (data.Sum() / data.Length), 2)) / (data.Length - 1);

        public static (double mean, double median, double mode, double range,
            double IQR, double Q1, double Q2, double Q3)
            CalculateStatistics(int[] data)
        {
            double mean = data.Average();

            int[] sortedData = data.OrderBy(value => value).ToArray();

            double mode = sortedData.Mode();

            double range = sortedData[sortedData.Length - 1] - sortedData[0];

            var Quartiles = CalculateQuartiles(sortedData);

            return (mean, data.Median(), mode, range, Quartiles.Q3 - Quartiles.Q1, Quartiles.Q1, Quartiles.Q2, Quartiles.Q3);
        }
    }
}