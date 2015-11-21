namespace Highway1.Universal.Input
{

    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    /// <summary>Asynchronous delegate command class.</summary>
    public sealed class AsyncDelegateCommand : DelegateCommandBase
    {

        #region Fields

        private readonly Func<object, Task> _action;

        private bool _isExecuting;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDelegateCommand" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public AsyncDelegateCommand(Func<object, Task> action)
            : this(action, null)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
        }

        private AsyncDelegateCommand(Func<object, Task> action, Func<object, bool> predicate)
            : base(predicate)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            _action = action;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
            => !_isExecuting && base.CanExecute(parameter);

        /// <summary>Creates the specified action.</summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static AsyncDelegateCommand Create(Func<Task> action)
        {
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(Create));
            return new AsyncDelegateCommand(_ => action());
        }

        /// <summary>Executes the specified parameter.</summary>
        /// <param name="parameter">The parameter.</param>
        public override async void Execute(object parameter)
            => await ExecuteAsync(parameter);

        /// <summary>Executes the asynchronous.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public async Task ExecuteAsync(object parameter)
        {
            Contract.Ensures(Contract.Result<Task>() != null, nameof(ExecuteAsync));
            _isExecuting = true;
            try
            {
                RaiseCanExecuteChanged();
                await _action(parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>Withes the can execute.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public AsyncDelegateCommand WithCanExecute(Func<bool> value)
        {
            Contract.Requires<ArgumentNullException>(value != null, nameof(value));
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(WithCanExecute));
            return WithCanExecute(_ => value());
        }

        /// <summary>Withes the can execute.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public AsyncDelegateCommand WithCanExecute(Func<object, bool> value)
        {
            Contract.Ensures(Contract.Result<AsyncDelegateCommand>() != null, nameof(WithCanExecute));
            return value == Predicate ? this : new AsyncDelegateCommand(_action, value);
        }

        #endregion

    }

}