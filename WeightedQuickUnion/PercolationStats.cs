using System;
using System.Linq;

namespace Percolation
{
    public class PercolationStats
    {
        private double[] _means;
        private int _tries;

        public PercolationStats(int n, int tries)
        {
            if (n < 0 && tries < 0)
                throw new ArgumentException("Number of tries and size of the grid must be more than 0");

            _means = new double[tries];
            _tries = tries;

            Random rnd = new Random();
            for (int i = 0; i < tries; i++)
            {
                Percolation percolation = new Percolation(n);
                while (!percolation.Percolates())
                {
                    int row = rnd.Next(1, n + 1);
                    int col = rnd.Next(1, n + 1);
                    percolation.Open(row, col);
                }

                int openSites = percolation.NumberOfOpenSites();
                _means[i] = (double)openSites / (n * n);
            }
        }

        // sample mean of percolation threshold
        public double Mean()
        {
            return _means.ToList().Average();
        }

        // sample standard deviation of percolation threshold
        public double Stddev()
        {
            var meansList = _means.ToList();
            var mean = Mean();
            var maxDeviation = Math.Abs(meansList.Max() - mean);
            var minDeviation = Math.Abs(meansList.Min() - mean);

            return Math.Max(maxDeviation, minDeviation);
        }

        // low endpoint of 95% confidence interval
        public double ConfidenceLo()
        {
            return Mean() - (1.96 * Stddev() / Math.Sqrt(_tries));
        }

        // high endpoint of 95% confidence interval
        public double ConfidenceHi()
        {
            return Mean() + (1.96 * Stddev() / Math.Sqrt(_tries));
        }
    }
}
