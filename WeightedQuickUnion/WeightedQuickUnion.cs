namespace Percolation
{
    class WeightedQuickUnion
    {
        private int[] sortedArr;
        private int[] sizeArr;

        /// <summary>
        /// Initializes an empty union-find data structure with n elements 0 through n-1. Initially, each elements is in its own set.
        /// </summary>
        /// <param name="n"></param>
        public WeightedQuickUnion(int n)
        {
            sortedArr = new int[n];
            sizeArr = new int[n];
            for (int i = 0; i < n; i++)
            {
                sortedArr[i] = i;
                sizeArr[i] = 1;
            }

        }

        /// <summary>
        /// Finds the root of element at index position
        /// </summary>
        /// <param name="index">position in the set</param>
        /// <returns>root</returns>
        public int Find(int index)
        {
            int result = index;
            while (sortedArr[result] != result)
            {
                sortedArr[result] = sortedArr[sortedArr[result]];
                result = sortedArr[result];
            }
            return result;
        }

        /// <summary>
        /// Connects two points of the set
        /// </summary>
        /// <param name="firstElem">first point position</param>
        /// <param name="secondElem">second point position</param>
        public void Union(int firstElem, int secondElem)
        {
            int i = Find(firstElem);
            int j = Find(secondElem);

            int sizeI = sizeArr[i];
            int sizeJ = sizeArr[j];

            if(sizeI < sizeJ)
            {
                sortedArr[i] = j;
                sizeArr[j] += sizeI;
            }
            else
            {
                sortedArr[j] = i;
                sizeArr[i] += sizeJ;
            }
        }

    }
}
