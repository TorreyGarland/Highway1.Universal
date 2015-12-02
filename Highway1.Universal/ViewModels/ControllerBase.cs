namespace Highway1.Universal.ViewModels
{

    using Input;
    using Services;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Windows.Input;

    /// <summary>Controller base class.</summary>
    /// <seealso cref="Highway1.Universal.ViewModels.ViewModelBase" />
    [DebuggerStepThrough]
    public abstract class ControllerBase : ViewModelBase
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DelegateCommand _goBackCommand;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DelegateCommand _goForwardCommand;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DelegateCommand _settingsCommand;

        #endregion

        #region Properties

        /// <summary>Gets or sets the go back command.</summary>
        /// <value>The go back command.</value>
        public ICommand GoBackCommand => _goBackCommand;

        /// <summary>Gets or sets the go forward command.</summary>
        /// <value>The go forward command.</value>
        public ICommand GoForwardCommand => _goForwardCommand;

        /// <summary>Gets the navigator.</summary>
        /// <value>The navigator.</value>
        protected INavigator Navigator { get; }

        /// <summary>Gets the settings command.</summary>
        /// <value>The settings command.</value>
        public ICommand SettingsCommand => _settingsCommand;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase" /> class.
        /// </summary>
        /// <param name="navigator">The navigator.</param>
        [DebuggerStepThrough]
        protected ControllerBase(INavigator navigator)
        {
            Contract.Requires<ArgumentNullException>(navigator != null);
            _goBackCommand = new DelegateCommand(GoBackExecute).WithCanExecute(GoBackCanExecute);
            _goForwardCommand = new DelegateCommand(GoForwardExecute).WithCanExecute(GoForwardCanExecute);
            _settingsCommand = new DelegateCommand(SettingExecute).WithCanExecute(SettingsCanExecute);
            Navigator = navigator;
        }

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(Navigator != null);
        }

        /// <summary>Goes the back can execute.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        protected virtual bool GoBackCanExecute(object parameter) 
            => Navigator.CanGoBack;

        /// <summary>Goes the forward can execute.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        protected virtual bool GoForwardCanExecute(object parameter) 
            => Navigator.CanGoForward;

        /// <summary>Goes the back execute.</summary>
        /// <param name="parameter">The parameter.</param>
        [DebuggerStepThrough]
        protected virtual void GoBackExecute(object parameter) 
            => Navigator.GoBack();

        /// <summary>Goes the forward execute.</summary>
        /// <param name="parameter">The parameter.</param>
        [DebuggerStepThrough]
        protected virtual void GoForwardExecute(object parameter) 
            => Navigator.GoForward();

        /// <summary>Initializes this instance.</summary>
        [DebuggerStepThrough]
        public virtual void Initialize() { }

        /// <summary>Settingses the can execute.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        protected virtual bool SettingsCanExecute(object parameter)
            => true;

        /// <summary>Settings the execute.</summary>
        /// <param name="parameter">The parameter.</param>
        [DebuggerStepThrough]
        protected virtual void SettingExecute(object parameter) { }

        #endregion

    }

}