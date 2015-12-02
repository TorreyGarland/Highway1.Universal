﻿namespace Highway1.Universal.Collections
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.Contracts;
    using Windows.Foundation.Collections;

    /// <summary>Observable vector class.</summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    /// <seealso cref="Windows.Foundation.Collections.IObservableVector{T}" />
    public class ObservableVector<T> : ObservableCollection<T>, IObservableVector<T>
    {

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableVector{T}" /> class.
        /// </summary>
        public ObservableVector()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableVector{T}" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ObservableVector(IEnumerable<T> collection)
            : base(collection)
        {
            Contract.Requires<ArgumentNullException>(collection != null, nameof(collection));
        }

        /// <summary>Raises the <see cref="E:CollectionChanged" /> event.</summary>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            VectorChangedEventArgs args;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    args = new VectorChangedEventArgs(CollectionChange.ItemInserted, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    args = new VectorChangedEventArgs(CollectionChange.ItemRemoved, e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    args = new VectorChangedEventArgs(CollectionChange.ItemChanged, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Move:
                    args = new VectorChangedEventArgs(CollectionChange.Reset, 0);
                    break;
                default:
                    args = new VectorChangedEventArgs(CollectionChange.ItemChanged, 0);
                    break;
            }
            OnVectorChanged(args);
        }

        /// <summary>Raises the <see cref="E:VectorChanged" /> event.</summary>
        /// <param name="e">The <see cref="IVectorChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnVectorChanged(IVectorChangedEventArgs e)
            => VectorChanged?.Invoke(this, e);

        #endregion

        #region Events

        /// <summary>Occurs when [vector changed].</summary>
        public event VectorChangedEventHandler<T> VectorChanged;

        #endregion

    }

}