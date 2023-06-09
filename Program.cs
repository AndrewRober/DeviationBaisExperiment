﻿using System.Text;

namespace DeviationBaisExperiment
{
    public static class Program
    {
        const int SMALL_DS_SIZE = 1000, MEDIUM_DS_SIZE = 10_000, LARGE_DS_SIZE = 1_000_000,
            DS_MIN_VALUE = 0, DS_MAX_VALUE = 1000;
        const double SAMPLING_START = 0.005, SAMPLING_END = 0.1, SAMPLING_INC = 0.005;
        static readonly int[] RANDOM_SEEDS = new[] { 0, 1 };

        static void Main(string[] args)
        {
            Experiment1();
            Experiment2(200);
            Experiment3(200);
        }

        static void Experiment1()
        {
            //initializing a random generator using constant seed
            Console.WriteLine($"Instantiating Random generators, seeds used [{string.Join(", ", RANDOM_SEEDS)}]");
            var r1 = new Random(Seed: RANDOM_SEEDS[0]);
            var r2 = new Random(Seed: RANDOM_SEEDS[1]);
            Console.WriteLine();


            //generating the datasets
            Console.WriteLine($"Generating datasets with the following sizes ({SMALL_DS_SIZE}, {MEDIUM_DS_SIZE}, {LARGE_DS_SIZE}) of range [min:{DS_MIN_VALUE} -> max:{DS_MAX_VALUE}] in memory...");
            var smallDs = Enumerable.Range(0, SMALL_DS_SIZE)
                .Select(i => r1.Next(DS_MIN_VALUE, DS_MAX_VALUE)).ToArray();
            var mediumDs = Enumerable.Range(0, MEDIUM_DS_SIZE)
                .Select(i => r1.Next(DS_MIN_VALUE, DS_MAX_VALUE)).ToArray();
            var largeDs = Enumerable.Range(0, LARGE_DS_SIZE)
                .Select(i => r1.Next(DS_MIN_VALUE, DS_MAX_VALUE)).ToArray();
            Console.WriteLine();


            //generated datasets preview
            Console.WriteLine($"{"Small".PadRight(10)} {$"dataset({smallDs.Count():n0})".PadRight(20)} [{string.Join(", ", smallDs.Take(10))}, ...]");
            Console.WriteLine($"{"Medium".PadRight(10)} {$"dataset({mediumDs.Count():n0})".PadRight(20)} [{string.Join(", ", mediumDs.Take(10))}, ...]");
            Console.WriteLine($"{"Large".PadRight(10)} {$"dataset({largeDs.Count():n0})".PadRight(20)} [{string.Join(", ", largeDs.Take(10))}, ...]");
            Console.WriteLine();


            //saving the datasets for further inspection and plotting
            Directory.CreateDirectory("Datasets");
            Console.WriteLine($"Saving datasets(Datasets\\small-ds.txt, Datasets\\medium-ds.txt, Datasets\\large-ds.txt) to disk...");
            Console.WriteLine($"at {Environment.CurrentDirectory}");
            File.WriteAllText("Datasets\\small-ds.txt", string.Join(",", smallDs));
            File.WriteAllText("Datasets\\medium-ds.txt", string.Join(",", mediumDs));
            File.WriteAllText("Datasets\\large-ds.txt", string.Join(",", largeDs));
            Console.WriteLine();


            //creating the sampling rate array
            var samplingArrayCount = (int)((SAMPLING_END - SAMPLING_START) / SAMPLING_INC) + 1;
            var samplingArray = Enumerable.Range((int)SAMPLING_START + 1, (int)samplingArrayCount)
                .Select(i => i * SAMPLING_INC).ToArray();
            Console.WriteLine($"Sampling {samplingArrayCount} times; [{string.Join(", ", samplingArray)}]");
            Console.WriteLine();


            //sampling the datasets
            Console.WriteLine("Generating samples...");
            var sds_samples_counts = Enumerable.Range(0, samplingArrayCount)
                .Select(i => (int)(samplingArray[i] * SMALL_DS_SIZE));
            var sds_samples = sds_samples_counts
                .Select(j => Enumerable.Range(0, (int)j).Select(k => smallDs[r2.Next(0, SMALL_DS_SIZE)]));
            var mds_samples_counts = Enumerable.Range(0, samplingArrayCount)
                .Select(i => (int)(samplingArray[i] * MEDIUM_DS_SIZE));
            var mds_samples = mds_samples_counts
                .Select(j => Enumerable.Range(0, (int)j).Select(k => mediumDs[r2.Next(0, MEDIUM_DS_SIZE)]));
            var lds_samples_counts = Enumerable.Range(0, samplingArrayCount)
                .Select(i => (int)(samplingArray[i] * LARGE_DS_SIZE));
            var lds_samples = lds_samples_counts
                .Select(j => Enumerable.Range(0, (int)j).Select(k => largeDs[r2.Next(0, LARGE_DS_SIZE)]));
            Console.WriteLine($"Actual Sampling Counts [{string.Join(", ", sds_samples_counts)}]");
            Console.WriteLine($"Actual Sampling Counts [{string.Join(", ", mds_samples_counts)}]");
            Console.WriteLine($"Actual Sampling Counts [{string.Join(", ", lds_samples_counts)}]");
            Console.WriteLine();


            //previewing samples
            //Small ds
            Console.WriteLine($"Small Dataset({SMALL_DS_SIZE})");
            Console.WriteLine($"{"Sample Size"}\t\t{"Preview"}");
            foreach (var samples in sds_samples)
                Console.WriteLine($"\t{samples.Count()}\t\t{$"[{string.Join(", ", samples.Take(15))}{((samples.Count() > 15) ? ", ..." : "")}]"}");
            Console.WriteLine();
            //Medium ds
            Console.WriteLine($"Medium Dataset({MEDIUM_DS_SIZE})");
            Console.WriteLine($"{"Sample Size"}\t\t{"Preview"}");
            foreach (var samples in mds_samples)
                Console.WriteLine($"\t{samples.Count()}\t\t{$"[{string.Join(", ", samples.Take(15))}{((samples.Count() > 15) ? ", ..." : "")}]"}");
            Console.WriteLine();
            //Large ds
            Console.WriteLine($"Large Dataset({LARGE_DS_SIZE})");
            Console.WriteLine($"{"Sample Size"}\t\t{"Preview"}");
            foreach (var samples in lds_samples)
                Console.WriteLine($"\t{samples.Count()}\t\t{$"[{string.Join(", ", samples.Take(15))}{((samples.Count() > 15) ? ", ..." : "")}]"}");
            Console.WriteLine();


            //Writing the samples to disk
            Console.WriteLine("Writing the samples to disk...");
            Directory.CreateDirectory("Small Ds samples");
            Directory.CreateDirectory("Medium Ds samples");
            Directory.CreateDirectory("Large Ds samples");
            foreach (var samples in sds_samples)
                File.WriteAllText($"Small Ds samples\\{samples.Count()}.txt", string.Join(", ", samples));
            foreach (var samples in mds_samples)
                File.WriteAllText($"Medium Ds samples\\{samples.Count()}.txt", string.Join(", ", samples));
            foreach (var samples in lds_samples)
                File.WriteAllText($"Large Ds samples\\{samples.Count()}.txt", string.Join(", ", samples));
            Console.WriteLine("Samples written to:");
            Console.WriteLine($"\t{Path.Combine(Environment.CurrentDirectory, "Small Ds samples")}");
            Console.WriteLine($"\t{Path.Combine(Environment.CurrentDirectory, "Medium Ds samples")}");
            Console.WriteLine($"\t{Path.Combine(Environment.CurrentDirectory, "Large Ds samples")}");
            Console.WriteLine();


            //calculating the population std with and without bias and calculating the actual std for the entire set
            var smallDsVariance = Arithmetic.Variance(smallDs);
            var mediumDsVariance = Arithmetic.Variance(mediumDs);
            var largeDsVariance = Arithmetic.Variance(largeDs);
            var smallDsStd = Arithmetic.StandardDeviation(smallDs);
            var mediumDsStd = Arithmetic.StandardDeviation(mediumDs);
            var largeDsStd = Arithmetic.StandardDeviation(largeDs);

            //calculating datasets statistics
            var smallDsStatistics = Arithmetic.CalculateStatistics(smallDs);
            var mediumDsStatistics = Arithmetic.CalculateStatistics(mediumDs);
            var largeDsStatistics = Arithmetic.CalculateStatistics(largeDs);

            Console.WriteLine("Actual standard deviation for the datasets is");
            Console.WriteLine($"{"Small"}\ts= {smallDsStd:f2}\ts^2= {Math.Pow(smallDsStd, 2):f2}\tMean= {smallDsStatistics.mean:f2}\tMedian= {smallDsStatistics.median:f2}\tQ1= {smallDsStatistics.Q1:f2}\tQ2= {smallDsStatistics.Q2:f2}\tQ3= {smallDsStatistics.Q3:f2}\tIQR= {smallDsStatistics.IQR:f2}\tRange= {smallDsStatistics.range:f2}");
            Console.WriteLine($"{"Medium"}\ts= {mediumDsStd:f2}\ts^2= {Math.Pow(mediumDsStd, 2):f2}\tMean= {mediumDsStatistics.mean:f2}\tMedian= {mediumDsStatistics.median:f2}\tQ1= {mediumDsStatistics.Q1:f2}\tQ2= {mediumDsStatistics.Q2:f2}\tQ3= {mediumDsStatistics.Q3:f2}\tIQR= {mediumDsStatistics.IQR:f2}\tRange= {mediumDsStatistics.range:f2}");
            Console.WriteLine($"{"Large"}\ts= {largeDsStd:f2}\ts^2= {Math.Pow(largeDsStd, 2):f2}\tMean= {largeDsStatistics.mean:f2}\tMedian= {largeDsStatistics.median:f2}\tQ1= {largeDsStatistics.Q1:f2}\tQ2= {largeDsStatistics.Q2:f2}\tQ3= {largeDsStatistics.Q3:f2}\tIQR= {largeDsStatistics.IQR:f2}\tRange= {largeDsStatistics.range:f2}");
            Console.WriteLine();

            Directory.CreateDirectory("Results");
            var sb = new StringBuilder();
            sb.AppendLine($"Sample Size, Sample Percentage, Actual Std, Biased Std, Non-Biased Std, Biased AD, Non-Biased AD");
            for (int i = 0; i < sds_samples.Count(); i++)
            {
                var samples = sds_samples.ElementAt(i);
                (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(samples.ToArray());
                sb.AppendLine($"{samples.Count()},{samplingArray[i] * 100}%,{smallDsStd:f2},{biased:f2},{nonbiased:f2},{Math.Abs(smallDsStd - biased):f2},{Math.Abs(smallDsStd - nonbiased):f2}");
            }
            File.WriteAllText("Results\\small-ds.csv", sb.ToString());

            sb = new StringBuilder();
            sb.AppendLine($"Sample Size, Sample Percentage, Actual Std, Biased Std, Non-Biased Std, Biased AD, Non-Biased AD");
            for (int i = 0; i < mds_samples.Count(); i++)
            {
                var samples = mds_samples.ElementAt(i);
                (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(samples.ToArray());
                sb.AppendLine($"{samples.Count()},{samplingArray[i] * 100}%,{mediumDsStd:f2},{biased:f2},{nonbiased:f2},{Math.Abs(smallDsStd - biased):f2},{Math.Abs(smallDsStd - nonbiased):f2}");
            }
            File.WriteAllText("Results\\medium-ds.csv", sb.ToString());

            sb = new StringBuilder();
            sb.AppendLine($"Sample Size, Sample Percentage, Actual Std, Biased Std, Non-Biased Std, Biased AD, Non-Biased AD");
            for (int i = 0; i < lds_samples.Count(); i++)
            {
                var samples = lds_samples.ElementAt(i);
                (double biased, double nonbiased) = Arithmetic.StandardDeviationBiases(samples.ToArray());
                sb.AppendLine($"{samples.Count()},{samplingArray[i] * 100}%,{largeDsStd:f2},{biased:f2},{nonbiased:f2},{Math.Abs(smallDsStd - biased):f2},{Math.Abs(smallDsStd - nonbiased):f2}");
            }
            File.WriteAllText("Results\\large-ds.csv", sb.ToString());

            Console.ReadLine();
        }

        
        static void Experiment2(int times)
        {
            var samplingArrayCount = (int)((SAMPLING_END - SAMPLING_START) / SAMPLING_INC) + 1;
            var samplingArray = Enumerable.Range((int)SAMPLING_START + 1, (int)samplingArrayCount)
                .Select(i => i * SAMPLING_INC).ToArray();



            var sb = new StringBuilder();
            sb.AppendLine("Dataset Size, MAD, MSE, MAPE");
            for(int i = 1; i <= times; i++)
            {
                //initializing a random generator using constant seed
                int s1 = Guid.NewGuid().GetHashCode(), s2 = Guid.NewGuid().GetHashCode();
                Console.WriteLine($"Instantiating Random generators, seeds used [{s1}, {s2}]");
                var r1 = new Random(s1);
                var r2 = new Random(s2);

                //generating the datasets
                var smallDs = Enumerable.Range(0, i * SMALL_DS_SIZE)
                    .Select(i => r1.Next(DS_MIN_VALUE, DS_MAX_VALUE)).ToArray();

                //sampling the datasets
                Console.WriteLine("Generating samples...");
                var sds_samples_counts = Enumerable.Range(0, samplingArrayCount)
                    .Select(i => (int)(samplingArray[i] * SMALL_DS_SIZE));
                var sds_samples = sds_samples_counts
                    .Select(j => Enumerable.Range(0, (int)j).Select(k => smallDs[r2.Next(0, SMALL_DS_SIZE)]));


                //calculating the population std with and without bias and calculating the actual std for the entire set
                var smallDsStd = Arithmetic.StandardDeviation(smallDs);

                //calculate mean average deviation between biased std and non-biased std
                var smallMAD = sds_samples.Sum(s =>
                {
                    (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(s.ToArray());
                    return Math.Abs(biased - nonbiased);
                }) / sds_samples.Count();
                var smallMSE = sds_samples.Sum(s =>
                {
                    (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(s.ToArray());
                    return Math.Pow(biased - nonbiased, 2);
                }) / sds_samples.Count();
                var smallMAPE = sds_samples.Sum(s =>
                {
                    (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(s.ToArray());
                    return (Math.Abs(biased - nonbiased) / smallDsStd) * 100;
                }) / sds_samples.Count();
                sb.AppendLine($"{smallDs.Length}, {smallMAD:f2}, {smallMSE:f2}, {smallMAPE:f2}%");
            }
            File.WriteAllText("Results\\exp2-ds.csv", sb.ToString());
        }

        static void Experiment3(int times)
        {
            var samplingArrayCount = (int)((SAMPLING_END - SAMPLING_START) / SAMPLING_INC) + 1;
            var samplingArray = Enumerable.Range((int)SAMPLING_START + 1, (int)samplingArrayCount)
                .Select(i => i * SAMPLING_INC).ToArray();



            var sb = new StringBuilder();
            sb.AppendLine("Dataset Size, MAD, MSE, MAPE");
            for (int i = 1; i <= times; i++)
            {
                //initializing a random generator using constant seed
                int s1 = Guid.NewGuid().GetHashCode(), s2 = Guid.NewGuid().GetHashCode();
                Console.WriteLine($"Instantiating Random generators, seeds used [{s1}, {s2}]");
                var r1 = new Random(s1);
                var r2 = new Random(s2);

                //generating the datasets
                var smallDs = Enumerable.Range(0, i * SMALL_DS_SIZE)
                    .Select(i => r1.Next(DS_MIN_VALUE, DS_MAX_VALUE)).ToArray();

                //sampling the datasets
                Console.WriteLine("Generating samples...");
                var sds_samples_counts = Enumerable.Range(0, samplingArrayCount)
                    .Select(k => (int)(samplingArray[k] * SMALL_DS_SIZE * i));
                var sds_samples = sds_samples_counts
                    .Select(j => Enumerable.Range(0, (int)j).Select(k => smallDs[r2.Next(0, SMALL_DS_SIZE)]));


                //calculating the population std with and without bias and calculating the actual std for the entire set
                var smallDsStd = Arithmetic.StandardDeviation(smallDs);

                //calculate mean average deviation between biased std and non-biased std
                var smallMAD = sds_samples.Sum(s =>
                {
                    (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(s.ToArray());
                    return Math.Abs(nonbiased - biased);
                }) / sds_samples.Count();
                var smallMSE = sds_samples.Sum(s =>
                {
                    (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(s.ToArray());
                    return Math.Pow(nonbiased - biased, 2);
                }) / sds_samples.Count();
                var smallMAPE = sds_samples.Sum(s =>
                {
                    (double nonbiased, double biased) = Arithmetic.StandardDeviationBiases(s.ToArray());
                    return (Math.Abs(nonbiased - biased) / smallDsStd) * 100;
                }) / sds_samples.Count();
                sb.AppendLine($"{smallDs.Length}, {smallMAD:f2}, {smallMSE:f2}, {smallMAPE:f2}%");
            }
            File.WriteAllText("Results\\exp3-ds.csv", sb.ToString());


        }
    }
}