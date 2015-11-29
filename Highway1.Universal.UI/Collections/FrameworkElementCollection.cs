namespace Highway1.Universal.UI.Collections
{

    using System.Collections.ObjectModel;
    using Universal.Collections;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;

    /// <summary>Framework element collection class.</summary>
    public sealed class FrameworkElementCollection : ObservableCollection<FrameworkElement>, IObservableVector<FrameworkElement>
    {

        #region Methods

        /// <summary>Inserts the item.</summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void InsertItem(int index, FrameworkElement item)
        {
            base.InsertItem(index, item);
            VectorChanged?.Invoke(this, new VectorChangedEventArgs(CollectionChange.ItemInserted, index));
        }

        /// <summary>Removes the item.</summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            VectorChanged?.Invoke(this, new VectorChangedEventArgs(CollectionChange.ItemRemoved, index));
        }

        /// <summary>Sets the item.</summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void SetItem(int index, FrameworkElement item)
        {
            base.SetItem(index, item);
            VectorChanged?.Invoke(this, new VectorChangedEventArgs(CollectionChange.ItemChanged, index));
        }

        #endregion

        #region Events

        /// <summary>Occurs when [vector changed].</summary>
        public event VectorChangedEventHandler<FrameworkElement> VectorChanged;

        #endregion

    }

}