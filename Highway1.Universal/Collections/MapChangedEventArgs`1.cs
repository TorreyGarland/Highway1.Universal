namespace Highway1.Universal.Collections
{

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using Windows.Foundation.Collections;

    /// <summary>Map changed event arguments structure.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    [DebuggerStepThrough]
    public struct MapChangedEventArgs<TKey> : IMapChangedEventArgs<TKey>
    {

        #region Properties

        /// <summary>Gets the collection change.</summary>
        /// <value>The collection change.</value>
        public CollectionChange CollectionChange { get; }

        /// <summary>Gets the empty.</summary>
        /// <value>The empty.</value>
        public static MapChangedEventArgs<TKey> Empty { get; } = new MapChangedEventArgs<TKey>();

        /// <summary>Gets the key.</summary>
        /// <value>The key.</value>
        public TKey Key { get; }

        #endregion

        #region Methods

        [DebuggerStepThrough]
        private MapChangedEventArgs(TKey key, CollectionChange collectionChange)
            : this()
        {
            CollectionChange = collectionChange;
            Key = key;
        }

        /// <summary>Creates the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="collectionChange">The collection change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        public static MapChangedEventArgs<TKey> Create(TKey key = default(TKey), CollectionChange collectionChange = CollectionChange.Reset)
            => new MapChangedEventArgs<TKey>(key, collectionChange);

        /// <summary>Withes the collection change.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        public MapChangedEventArgs<TKey> WithCollectionChange(CollectionChange value)
            => value == CollectionChange ? this : new MapChangedEventArgs<TKey>(Key, value);

        /// <summary>Withes the key.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        public MapChangedEventArgs<TKey> WithKey(TKey value)
            => EqualityComparer<TKey>.Default.Equals(value, Key) ? this : new MapChangedEventArgs<TKey>(value, CollectionChange);

        #endregion

    }

}