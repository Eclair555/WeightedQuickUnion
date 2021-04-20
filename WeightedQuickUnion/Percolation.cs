using System;

namespace Percolation
{
    public class Percolation
    {
        private bool[,] _percolationGrid;
        private int _size;
        private int _top;
        private int _bottom;
        private int _openSites = 0;

        private WeightedQuickUnion quickUnionAlg;

        /// <summary>
        /// Creates n-by-n grid, with all sites initially blocked
        /// </summary>
        /// <param name="n">size of the grid</param>
        public Percolation(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Grid size must be more than 0");

            _size = n;
            _top = 0;
            _bottom = _size * _size + 1;

            //add two points at top and bottom
            quickUnionAlg = new WeightedQuickUnion(_size * _size + 2);

            //connect first point to a first row
            // and last point to a last row
            for(int i = 1; i < _size; i++)
            {
                quickUnionAlg.Union(GetSitePosition(1, i), _top);
                quickUnionAlg.Union(GetSitePosition(_size, i), _bottom);
            }

            _percolationGrid = new bool[_size,_size];

        }

        /// <summary>
        /// Opens the blocked site
        /// </summary>
        /// <param name="row">row index of the site</param>
        /// <param name="col">column index of the site</param>
        public void Open(int row, int col)
        {
            Validate(row, col);

            if (!IsOpen(row, col))
            {
                _percolationGrid[row - 1,col - 1] = true;
                _openSites++;

                int position = GetSitePosition(row, col);

                //connect point to top point
                if (row - 1 > 0 && IsOpen(row - 1, col))
                    quickUnionAlg.Union(position, GetSitePosition(row - 1, col));
                //connect point to bottom point
                if (row + 1 <= _size && IsOpen(row + 1, col))
                    quickUnionAlg.Union(position, GetSitePosition(row + 1, col));
                //connect point to left point
                if (col - 1 > 0 && IsOpen(row, col - 1))
                    quickUnionAlg.Union(position, GetSitePosition(row, col - 1));
                //connect point to right point
                if (col + 1 <= _size && IsOpen(row, col + 1))
                    quickUnionAlg.Union(position, GetSitePosition(row, col + 1));
            }
        }

        /// <summary>
        /// Checks if the site is open
        /// </summary>
        /// <param name="row">row index of the site</param>
        /// <param name="col">column index of the site</param>
        /// <returns></returns>
        public bool IsOpen(int row, int col)
        {
            Validate(row, col);

            return _percolationGrid[row - 1,col - 1];
        }

        /// <summary>
        /// Checks if the site is full
        /// </summary>
        /// <param name="row">row index of the site</param>
        /// <param name="col">column index of the site</param>
        /// <returns></returns>
        public bool isFull(int row, int col)
        {
            Validate(row, col);

            return IsOpen(row, col) && quickUnionAlg.Find(GetSitePosition(row, col)) == quickUnionAlg.Find(_top);
        }

        /// <summary>
        /// Returns number of open sites
        /// </summary>
        /// <returns></returns>
        public int NumberOfOpenSites()
        {
            return _openSites;
        }

        /// <summary>
        /// Check if system percolates
        /// </summary>
        /// <returns></returns>
        public bool Percolates()
        {
            return quickUnionAlg.Find(_top) == quickUnionAlg.Find(_bottom);
        }

        private void Validate(int row, int col)
        {
            if (row < 1 || row > _size)
                throw new ArgumentException($"Row index must be more than 1 and less than {_size}");

            if (col < 1 || col > _size)
                throw new ArgumentException($"Column index must be more than 1 and less than {_size}");
        }

        private int GetSitePosition(int row, int col)
        {
            return col + (row - 1) * _size;
        }
    }
}
