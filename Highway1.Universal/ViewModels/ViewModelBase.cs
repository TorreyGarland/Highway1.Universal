namespace Highway1.Universal.ViewModels
{

    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Windows.Foundation.Metadata;

    /// <summary>View model base class.</summary>
    [WebHostHidden]
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConcurrentDictionary<string, PropertyChangedEventArgs> _changes = new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConcurrentDictionary<string, PropertyChangingEventArgs> _changings = new ConcurrentDictionary<string, PropertyChangingEventArgs>();

        #endregion

        #region Methods

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, _changes.GetOrAdd(propertyName ?? string.Empty, key => new PropertyChangedEventArgs(propertyName)));

        /// <summary>
        /// Called when [property changing].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
            => PropertyChanging?.Invoke(this, _changings.GetOrAdd(propertyName ?? string.Empty, key => new PropertyChangingEventArgs(propertyName)));

        /// <summary>Raises the property changed.</summary>
        /// <param name="propertyNames">The property names.</param>
        public void RaisePropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null || !propertyNames.Any())
                OnPropertyChanged(null);
            else
                propertyNames.Each(OnPropertyChanged);
        }

        /// <summary>Sets the specified field.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            OnPropertyChanging(propertyName);
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region Events

        /// <summary>Occurs when [property changed].</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Occurs when [property changing].</summary>
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

    }

}