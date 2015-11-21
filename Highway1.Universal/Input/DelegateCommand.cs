namespace Highway1.Universal.Input
{

    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Delegate command class.</summary>
    public sealed class DelegateCommand : DelegateCommandBase
    {

        #region Fields

        private readonly Action<object> _action;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DelegateCommand(Action<object> action)
            : this(action, null)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
        }

        private DelegateCommand(Action<object> action, Func<object, bool> predicate)
            : base(predicate)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            _action = action;
        }

        /// <summary>Creates the specified action.</summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        [Pure]
        public static DelegateCommand Create(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(Create));
            return new DelegateCommand(_ => action());
        }

        /// <summary>Executes the specified parameter.</summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter) => _action(parameter);

        /// <summary>Withes the can execute.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public DelegateCommand WithCanExecute(Func<bool> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, nameof(value));
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(WithCanExecute));
            return WithCanExecute(_ => value());
        }

        /// <summary>Withes the can execute.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public DelegateCommand WithCanExecute(Func<object, bool> value)
        {
            Contract.Ensures(Contract.Result<DelegateCommand>() != null, nameof(WithCanExecute));
            return value == Predicate ? this : new DelegateCommand(_action, value);
        }

        #endregion

    }

}