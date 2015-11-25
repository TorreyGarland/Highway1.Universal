namespace Highway1.Universal.ViewModels
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics.Contracts;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>Key value view model class.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class KeyValueViewModel<TKey, TValue> : ViewModel<TValue>
    {

        #region Fields

        private TKey _key;

        #endregion

        #region Properties

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public TKey Key
        {
            get { return _key; }
            set { Set(ref _key, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueViewModel{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public KeyValueViewModel(TKey key = default(TKey), TValue value = default(TValue))
            : base(value)
        {
            _key = key;
        }

        /// <summary>Clones this instance.</summary>
        /// <returns></returns>
        public new KeyValueViewModel<TKey, TValue> Clone()
        {
            Contract.Ensures(Contract.Result<KeyValueViewModel<TKey, TValue>>() != null, nameof(Clone));
            return new KeyValueViewModel<TKey, TValue>(_key, Value);
        }

        /// <summary>
        /// Called when [property changing].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            var args = new CancelEventArgs();
            if (string.Equals(propertyName, nameof(Key)))
            {
                KeyChanging?.Invoke(this, args);
                if (args.Cancel)
                    return;
            }
            base.OnPropertyChanging(propertyName);
        }

        /// <summary>Resets the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Reset(TKey key = default(TKey), TValue value = default(TValue))
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="KeyValueViewModel{TKey, TValue}" /> to <see cref="KeyValuePair{TKey, TValue}" />.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator KeyValuePair<TKey, TValue> (KeyValueViewModel<TKey, TValue> viewModel)
        {
            if (viewModel == null)
                return new KeyValuePair<TKey, TValue>();
            return new KeyValuePair<TKey, TValue>(viewModel._key, viewModel.Value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="KeyValuePair{TKey, TValue}" /> to <see cref="KeyValueViewModel{TKey, TValue}" />.
        /// </summary>
        /// <param name="pair">The pair.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator KeyValueViewModel<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
        {
            Contract.Ensures(Contract.Result<KeyValueViewModel<TKey, TValue>>() != null);
            return new KeyValueViewModel<TKey, TValue>(pair.Key, pair.Value);
        }

        #endregion

        #region Events

        /// <summary>Occurs when [key changing].</summary>
        public event EventHandler<CancelEventArgs> KeyChanging;

        #endregion

    }

}