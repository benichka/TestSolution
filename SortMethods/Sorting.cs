using System.Collections.Generic;
using System.Linq;

namespace SortMethods
{
    /// <summary>
    /// Class used to test different sorting methods
    /// </summary>
    public static class Sorting
    {
        #region Merge sorting

        /// <summary>
        /// Merge sorting: cut the list in smaller lists, sort it and merge it
        /// </summary>
        /// <param name="list">The list to sort.</param>
        /// <returns>A sorted list</returns>
        /// <example> 
        /// Use: 
        /// <code> 
        ///   List&lt;int&gt; list = new List&lt;int&gt;() { 3, 1, 5, 2, 7 };
        ///   List&lt;int&gt; sortedList = Sorting.MergeSorting(list);
        /// </code>
        /// Steps:<br />
        /// <code>
        /// MergeSorting([3, 1, 5, 2, 7]);
        /// //[3, 1, 5][2, 7]
        ///     MergeSorting([3, 1, 5]);
        ///     //[3, 1][5]
        ///         MergeSorting([3, 1]);
        ///         //[3][1]
        ///             MergeSorting([3]);
        ///             //[3]
        ///             MergeSorting([1]);
        ///             //[1]
        ///             Merge([3], [1]);
        ///             //[1, 3]
        ///         MergeSorting([5]);
        ///         //[5]
        ///         Merge([1, 3], [5]);
        ///         //[1, 3, 5]
        ///     MergeSorting([2, 7]);
        ///     //[2][7]
        ///         MergeSorting([2]);
        ///         //[2]
        ///         MergeSorting([7]);
        ///         //[7]
        ///         Merge([2], [7]);
        ///         //[2, 7]
        ///     Merge([1, 3, 5], [2, 7]);
        ///     //[1, 2, 3, 5, 7]
        /// </code>
        /// </example>
        public static List<int> MergeSorting(List<int> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }

            // int / int, the return is automatically an rounded int
            var mid = list.Count / 2;

            var left = new List<int>();
            var right = new List<int>();

            // Populate the 2 sub-lists
            for (int i = 0; i < mid; i++)
            {
                left.Add(list.ElementAt(i));
            }

            for (int i = mid; i < list.Count; i++)
            {
                right.Add(list.ElementAt(i));
            }

            // Recursively cut and sort the sub-lists
            left = MergeSorting(left);
            right = MergeSorting(right);

            // Merge left and right sub-lists
            return Merge(left, right);
        }

        /// <summary>
        /// Merge two list in one ordering them
        /// </summary>
        /// <param name="left">left list to merge</param>
        /// <param name="right">right list to merge</param>
        /// <returns>The two list merged and sorted</returns>
        private static List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();

            // Compare the 2 first elements in both list and move the smaller
            // one into the result list
            while (left.Count > 0 && right.Count > 0)
            {
                if (left.First() > right.First())
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
                else
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
            }

            // By construction, the algorithm merge only sorted lists if there is
            // more than 1 element in the list.
            // If one list is empty, it moves the content of the only populated list
            // into the result list.
            while (left.Count > 0)
            {
                result.Add(left.First());
                left.Remove(left.First());
            }

            while (right.Count > 0)
            {
                result.Add(right.First());
                right.Remove(right.First());
            }

            return result;
        }
        #endregion Merge sorting
    }
}
