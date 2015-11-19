namespace Highway1.Universal
{

    using System.Diagnostics.Contracts;

    /// <summary>View model class.</summary>
    /// <typeparam name="T"></typeparam>
    public class ViewModel<T> : ViewModelBase
    {

        #region Fields

        private T _value;

        #endregion

        #region Properties

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel{T}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ViewModel(T value = default(T))
        {
            _value = value;
        }

        /// <summary>Clones this instance.</summary>
        /// <returns></returns>
        [Pure]
        public ViewModel<T> Clone()
        {
            Contract.Ensures(Contract.Result<ViewModel<T>>() != null, nameof(Clone));
            return new ViewModel<T>(_value);
        }

        /// <summary>Resets the specified value.</summary>
        /// <param name="value">The value.</param>
        public void Reset(T value = default(T)) => Value = value;

        /// <summary>Implements the operator implicit T.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The result of the operator.</returns>
        public static implicit operator T(ViewModel<T> viewModel)
        {
            if (viewModel == null)
                return default(T);
            return viewModel.Value;
        }

        /// <summary>Implements the operator implicit ViewModel&lt;T&gt;.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static implicit operator ViewModel<T>(T value)
        {
            Contract.Ensures(Contract.Result<ViewModel<T>>() != null);
            return new ViewModel<T>(value);
        }

        #endregion

    }

}