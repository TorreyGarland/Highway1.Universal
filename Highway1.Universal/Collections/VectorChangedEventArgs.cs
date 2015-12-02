namespace Highway1.Universal.Collections
{

    using Windows.Foundation.Collections;

    /// <summary>Vector changed event arguments class.</summary>
    public struct VectorChangedEventArgs : IVectorChangedEventArgs
    {

        #region Properties

        /// <summary>Gets the collection change.</summary>
        /// <value>The collection change.</value>
        public CollectionChange CollectionChange { get; } 

        /// <summary>Gets the index.</summary>
        /// <value>The index.</value>
        public uint Index { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorChangedEventArgs" /> class.
        /// </summary>
        /// <param name="collectionChange">The collection change.</param>
        /// <param name="index">The index.</param>
        public VectorChangedEventArgs(CollectionChange collectionChange, int index)
            : this()
        {
            CollectionChange = collectionChange;
            Index = (uint)index;
        }

        #endregion

    }

}