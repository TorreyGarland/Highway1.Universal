namespace Highway1.Universal.UI.Interactivity
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>Multi-select behavior class.</summary>
    /// <seealso cref="Highway1.Universal.UI.Interactivity.BehaviorBase{Windows.UI.Xaml.Controls.ListViewBase}" />
    public sealed class MultiSelectBehavior : BehaviorBase<ListViewBase>
    {

        #region Fields

        private bool _selectionChanging;

        #endregion

        #region Properties

        /// <summary>Gets or sets the selected items.</summary>
        /// <value>The selected items.</value>
        public ICollection<object> SelectedItems
        {
            get { return (ICollection<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>The selected items property</summary>
        public static DependencyProperty SelectedItemsProperty { get; }
            = DependencyProperty.Register(nameof(SelectedItems), typeof(ICollection<object>), typeof(MultiSelectBehavior), new PropertyMetadata(null, OnSelectedItemsChanged));

        #endregion

        #region Methods

        /// <summary>Called when [attached].</summary>
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
                AssociatedObject.SelectionChanged += ListViewSelectionChanged;
        }

        /// <summary>Called when [detached].</summary>
        protected override void OnDetached()
        {
            base.OnDetached();
            if (AssociatedObject != null)
                AssociatedObject.SelectionChanged -= ListViewSelectionChanged;
        }

        private void ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectionChanging || SelectedItems == null)
                return;
            _selectionChanging = true;
            foreach (var item in e.RemovedItems)
            {
                if (SelectedItems.Contains(item))
                    SelectedItems.Remove(item);
            }
            foreach (var item in e.AddedItems)
            {
                if (!SelectedItems.Contains(item))
                    SelectedItems.Add(item);
            }
            _selectionChanging = false;

        }

        private static void SelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var behavior = sender as MultiSelectBehavior;
            if (behavior == null)
                return;
            var listView = behavior.AssociatedObject;
            if (listView == null)
                return;
            var selectedItems = listView.SelectedItems;
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (selectedItems.Contains(item))
                        selectedItems.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (!selectedItems.Contains(item))
                        selectedItems.Add(item);
                }
            }
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var handler = new NotifyCollectionChangedEventHandler((source, args) => SelectedItemsCollectionChanged(d, args));
            var old = e.OldValue as INotifyCollectionChanged;
            if (old != null)
                old.CollectionChanged -= handler;
            var @new = e.NewValue as INotifyCollectionChanged;
            if (@new != null)
                @new.CollectionChanged += handler;
        }
        
        #endregion

    }

}