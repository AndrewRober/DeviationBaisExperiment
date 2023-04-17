using System;
using System.Runtime.Intrinsics.X86;

namespace DeviationBaisExperiment
{
    internal class Program
    {
        const int SMALL_DS_SIZE = 1000, MEDIUM_DS_SIZE = 10_000, LARGE_DS_SIZE = 1_000_000,
            DS_MIN_VALUE = 0, DS_MAX_VALUE = 1000;
        const double SAMPLING_START = 0.005, SAMPLING_END = 0.1, SAMPLING_INC = 0.005;
        static readonly int[] RANDOM_SEEDS = new[] { 0, 1 };

        static void Main(string[] args)
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
            Console.WriteLine($"Saving datasets(small-ds.txt, medium-ds.txt, large-ds.txt) to disk...");
            Console.WriteLine($"at {Environment.CurrentDirectory}");
            File.WriteAllText("small-ds.txt", string.Join(",", smallDs));
            File.WriteAllText("medium-ds.txt", string.Join(",", mediumDs));
            File.WriteAllText("large-ds.txt", string.Join(",", largeDs));

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
        }
    }
}