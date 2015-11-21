namespace Highway1.Universal.Input
{

    using System;
    using System.Windows.Input;

    /// <summary>Delegate command base class.</summary>
    public abstract class DelegateCommandBase : ICommand
    {

        #region Properties

        /// <summary>Gets the predicate.</summary>
        /// <value>The predicate.</value>
        protected Func<object, bool> Predicate { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommandBase" /> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        protected DelegateCommandBase(Func<object, bool> predicate)
        {
            Predicate = predicate;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter) => (Predicate?.Invoke(parameter)).GetValueOrDefault();

        /// <summary>Executes the specified parameter.</summary>
        /// <param name="parameter">The parameter.</param>
        public abstract void Execute(object parameter);

        /// <summary>Raises the <see cref="E:CanExecuteChanged" /> event.</summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnCanExecuteChanged(EventArgs e) => CanExecuteChanged?.Invoke(this, e);

        /// <summary>Raises the can execute changed.</summary>
        public void RaiseCanExecuteChanged() => OnCanExecuteChanged(EventArgs.Empty);

        #endregion

        #region Events

        /// <summary>Occurs when [can execute changed].</summary>
        public event EventHandler CanExecuteChanged;

        #endregion

    }

}