namespace Highway1.Universal.ViewModels
{

    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using System;

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

        /// <summary>
        /// Called when [property changing].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            var args = new CancelEventArgs();
            OnValueChanging(args);
            if (args.Cancel)
                return;
            base.OnPropertyChanging(propertyName);
        }

        /// <summary>Raises the <see cref="E:ValueChanging" /> event.</summary>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        protected virtual void OnValueChanging(CancelEventArgs e) 
            => ValueChanging?.Invoke(this, e);

        /// <summary>Resets the specified value.</summary>
        /// <param name="value">The value.</param>
        public void Reset(T value = default(T)) => Value = value;

        /// <summary>Implements the operator implicit T.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The result of the operator.</returns>
        public static implicit operator T(ViewModel<T> viewModel) => viewModel == null ? default(T) : viewModel.Value;

        /// <summary>Implements the operator implicit ViewModel&lt;T&gt;.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static implicit operator ViewModel<T>(T value)
        {
            Contract.Ensures(Contract.Result<ViewModel<T>>() != null);
            return new ViewModel<T>(value);
        }

        #endregion

        #region Events

        /// <summary>Occurs when [value changing].</summary>
        public event EventHandler<CancelEventArgs> ValueChanging;

        #endregion

    }

}