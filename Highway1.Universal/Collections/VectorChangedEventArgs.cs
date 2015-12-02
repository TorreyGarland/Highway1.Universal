namespace Highway1.Universal.Collections
{

    using System;
    using Windows.Foundation.Collections;
    using System.Diagnostics.Contracts;

    /// <summary>Vector changed event arguments class.</summary>
    public sealed class VectorChangedEventArgs : EventArgs, IVectorChangedEventArgs
    {

        #region Fields

        private static readonly VectorChangedEventArgs _empty = Create();

        #endregion

        #region Properties

        /// <summary>Gets the collection change.</summary>
        /// <value>The collection change.</value>
        public CollectionChange CollectionChange { get; }

        /// <summary>Gets the empty.</summary>
        /// <value>The empty.</value>
        public static new VectorChangedEventArgs Empty
        {
            get
            {
                Contract.Ensures(Contract.Result<VectorChangedEventArgs>() != null, nameof(Empty));
                return _empty;
            }
        }

        /// <summary>Gets the index.</summary>
        /// <value>The index.</value>
        public uint Index { get; }

        #endregion

        #region Methods

        private VectorChangedEventArgs(CollectionChange collectionChange, uint index)
        {
            CollectionChange = collectionChange;
            Index = index;
        }

        /// <summary>Creates the specified collection change.</summary>
        /// <param name="collectionChange">The collection change.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        [Pure]
        public static VectorChangedEventArgs Create(CollectionChange collectionChange = CollectionChange.Reset, uint index = 0)
        {
            Contract.Ensures(Contract.Result<VectorChangedEventArgs>() != null, nameof(Create));
            return new VectorChangedEventArgs(collectionChange, index);
        }

        /// <summary>Withes the collection change.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public VectorChangedEventArgs WithCollectionChange(CollectionChange value)
        {
            Contract.Ensures(Contract.Result<VectorChangedEventArgs>() != null, nameof(WithCollectionChange));
            return value == CollectionChange ? this : new VectorChangedEventArgs(value, Index);
        }

        /// <summary>Withes the index.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public VectorChangedEventArgs WithIndex(int value)
        {
            Contract.Ensures(Contract.Result<VectorChangedEventArgs>() != null, nameof(WithIndex));
            return WithIndex((uint)value);
        }

        /// <summary>Withes the index.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public VectorChangedEventArgs WithIndex(uint value)
        {
            Contract.Ensures(Contract.Result<VectorChangedEventArgs>() != null, nameof(WithIndex));
            return value == Index ? this : new VectorChangedEventArgs(CollectionChange, value);
        }

        #endregion

    }

}