namespace Highway1.Universal.ComponentModel
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>Delegate comparer class.</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DelegateComparer<T> : IComparer<T>
    {

        #region Fields

        private readonly Func<T, T, int> _compare;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateComparer{T}" /> class.
        /// </summary>
        /// <param name="compare">The compare.</param>
        public DelegateComparer(Func<T, T, int> compare)
        {
            Contract.Requires<ArgumentNullException>(compare != null);
            _compare = compare;
        }

        /// <summary>Compares the specified x.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Compare(T x, T y) => _compare(x, y);

        #endregion

    }

}