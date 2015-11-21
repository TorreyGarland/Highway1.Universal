namespace Highway1.Universal.Services.Contracts
{

    using ComponentModel;
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(INavigator))]
    internal abstract class ContractForINavigator : INavigator
    {

        #region Properties

        public abstract int BackStackDepth { get; }

        public abstract bool CanGoBack { get; }

        public abstract bool CanGoForward { get; }

        #endregion

        #region Methods

        public abstract void GoBack();

        public abstract void GoForward();

        public bool NavigateTo(Type viewModelType, object parameter = null)
        {
            Contract.Requires<ArgumentNullException>(viewModelType != null, nameof(viewModelType));
            return default(bool);
        }

        public abstract bool NavigateTo<TViewModel>(object parameter = null);

        #endregion

        #region Events

        public abstract event EventHandler<NavigatorEventArgs> Navigated;

        public abstract event EventHandler<NavigatorCancelEventArgs> Navigating;

        public abstract event EventHandler<NavigatorFailedEventArgs> NavigationFailed;

        public abstract event EventHandler<NavigatorEventArgs> NavigationStopped;

        #endregion

    }

}