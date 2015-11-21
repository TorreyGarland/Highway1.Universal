namespace Highway1.Universal.Services
{

    using ComponentModel;
    using Contracts;
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Navigator interface.</summary>
    [ContractClass(typeof(ContractForINavigator))]
    public interface INavigator
    {

        #region Properties

        /// <summary>Gets the back stack depth.</summary>
        /// <value>The back stack depth.</value>
        int BackStackDepth { get; }

        /// <summary>Gets a value indicating whether this instance can go back.</summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        bool CanGoBack { get; }

        /// <summary>Gets a value indicating whether this instance can go forward.</summary>
        /// <value>
        /// <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        bool CanGoForward { get; }

        #endregion

        #region Methods

        /// <summary>Goes the back.</summary>
        void GoBack();

        /// <summary>Goes the forward.</summary>
        void GoForward();

        /// <summary>Navigates to.</summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        bool NavigateTo<TViewModel>(object parameter = null);

        /// <summary>Navigates to.</summary>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        bool NavigateTo(Type viewModelType, object parameter = null);

        #endregion

        #region Events

        /// <summary>Occurs when [navigated].</summary>
        event EventHandler<NavigatorEventArgs> Navigated;

        /// <summary>Occurs when [navigating].</summary>
        event EventHandler<NavigatorCancelEventArgs> Navigating;

        /// <summary>Occurs when [navigation failed].</summary>
        event EventHandler<NavigatorFailedEventArgs> NavigationFailed;

        /// <summary>Occurs when [navigation stopped].</summary>
        event EventHandler<NavigatorEventArgs> NavigationStopped;

        #endregion

    }

}