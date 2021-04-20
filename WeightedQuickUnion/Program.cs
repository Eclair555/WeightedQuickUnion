using System;
namespace Percolation
{
    public class Program
    {
        static void Main(string[] args)
        {
            PercolationStats stats = new PercolationStats(4, 30);

            Console.WriteLine($"mean = {stats.Mean()}");
            Console.WriteLine($"stddev = {stats.Stddev()}");
            Console.WriteLine($"95% confidence interval = {stats.ConfidenceLo()}, {stats.ConfidenceHi()}");
            Console.ReadKey();
        }
    }
}
