namespace Highway1.Universal.ViewModels
{

    using Input;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>Observable collection view model class.</summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Highway1.Universal.Collections.ObservableVector{T}" />
    public class ObservableCollectionViewModel<T> : Collections.ObservableVector<T>
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DelegateCommand _deleteCommand;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ObservableCollection<object> _selectedItems = new ObservableCollection<object>();

        #endregion

        #region Properties

        /// <summary>Gets the add command.</summary>
        /// <value>The add command.</value>
        public ICommand AddCommand { get; }

        /// <summary>Gets or sets the delete command.</summary>
        /// <value>The delete command.</value>
        public ICommand DeleteCommand
        {
            get
            {
                Contract.Ensures(Contract.Result<ICommand>() != null, nameof(DeleteCommand));
                return _deleteCommand;
            }
        }

        /// <summary>Gets the select command.</summary>
        /// <value>The select command.</value>
        public ICommand SelectCommand { get; }

        /// <summary>Gets or sets the selected items.</summary>
        /// <value>The selected items.</value>
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                Contract.Ensures(Contract.Result<ObservableCollection<object>>() != null, nameof(SelectedItems));
                return _selectedItems;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionViewModel{T}"/> class.
        /// </summary>
        public ObservableCollectionViewModel()
        {
            AddCommand = new DelegateCommand(AddExecute);
            _deleteCommand = new DelegateCommand(DeleteExecute).WithCanExecute(DeleteCanExecute);
            _selectedItems.CollectionChanged += SelectedItemsCollectionChanged;
            SelectCommand = new DelegateCommand(SelectExecute);
        }

        private void SelectExecute(object parameter) 
            => OnSelecting(EventArgs.Empty);

        private void AddExecute(object parameter) 
            => OnAdding(EventArgs.Empty);

        private bool DeleteCanExecute(object parameter)
            => _selectedItems.Any();

        private void DeleteExecute(object parameter)
        {
            var args = new CancelEventArgs();
            OnDeleting(args);
            if (!args.Cancel)
            {
                _selectedItems.OfType<T>().ToArray().Each(x => Remove(x));
                _selectedItems.Clear();
                _deleteCommand.RaiseCanExecuteChanged();
                OnDeleted(EventArgs.Empty);
            }
        }

        /// <summary>Raises the <see cref="E:Adding" /> event.</summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnAdding(EventArgs e)
            => Adding?.Invoke(this, e);

        /// <summary>Raises the <see cref="E:Deleted" /> event.</summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnDeleted(EventArgs e) 
            => Deleted?.Invoke(this, e);

        /// <summary>Raises the <see cref="E:Deleting" /> event.</summary>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        protected virtual void OnDeleting(CancelEventArgs e) 
            => Deleting?.Invoke(this, e);

        /// <summary>Raises the <see cref="E:Selecting" /> event.</summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelecting(EventArgs e) 
            => Selecting?.Invoke(this, e);

        private void SelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) 
            => _deleteCommand.RaiseCanExecuteChanged();

        #endregion

        #region Events

        public event EventHandler Adding;

        /// <summary>Occurs when [deleted].</summary>
        public event EventHandler Deleted;

        /// <summary>Occurs when [deleting].</summary>
        public event EventHandler<CancelEventArgs> Deleting;

        /// <summary>Occurs when [selecting].</summary>
        public event EventHandler Selecting;

        #endregion

    }

}