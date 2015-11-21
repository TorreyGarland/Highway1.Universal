namespace Highway1.Universal.ComponentModel
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>Delegate equality comparer class.</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {

        #region Fields

        private readonly Func<T, T, bool> _equals;

        private readonly Func<T, int> _hashCode;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateEqualityComparer{T}" /> class.
        /// </summary>
        /// <param name="equals">The equals.</param>
        /// <param name="hashCode">The hash code.</param>
        public DelegateEqualityComparer(Func<T, T, bool> equals, Func<T, int> hashCode = null)
        {
            Contract.Requires<ArgumentNullException>(equals != null, nameof(equals));
            _equals = equals;
            _hashCode = hashCode;
        }

        /// <summary>Equalses the specified x.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(T x, T y) => _equals(x, y);

        /// <summary>Returns a hash code for this instance.</summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(T obj) => (_hashCode?.Invoke(obj)).GetValueOrDefault(EqualityComparer<T>.Default.GetHashCode(obj));

        #endregion

    }

}