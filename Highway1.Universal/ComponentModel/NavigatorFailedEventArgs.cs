namespace Highway1.Universal.ComponentModel
{

    using System;
    using System.Diagnostics.Contracts;
    using Windows.UI.Xaml.Navigation;

    /// <summary>Navigator failed event arguments class.</summary>
    public sealed class NavigatorFailedEventArgs : EventArgs
    {

        #region Fields

        private static readonly NavigatorFailedEventArgs _empty = new NavigatorFailedEventArgs(null);

        #endregion

        #region Properties

        /// <summary>Gets the empty.</summary>
        /// <value>The empty.</value>
        public static new NavigatorFailedEventArgs Empty
        {
            get
            {
                Contract.Ensures(Contract.Result<NavigatorFailedEventArgs>() != null, nameof(Empty));
                return _empty;
            }
        }

        /// <summary>Gets the exception.</summary>
        /// <value>The exception.</value>
        public Exception Exception { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NavigatorFailedEventArgs" /> is handled.
        /// </summary>
        /// <value><c>true</c> if handled; otherwise, <c>false</c>.</value>
        public bool Handled { get; set; }

        #endregion

        #region Methods

        private NavigatorFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>Froms the navigation failed event arguments.</summary>
        /// <param name="args">The <see cref="NavigationFailedEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        [Pure]
        public static NavigatorFailedEventArgs FromNavigationFailedEventArgs(NavigationFailedEventArgs args)
            => args != null ? new NavigatorFailedEventArgs(args.Exception) { Handled = args.Handled } : null;

        /// <summary>Implements the operator explicit NavigatorFailedEventArgs.</summary>
        /// <param name="args">The <see cref="NavigationFailedEventArgs" /> instance containing the event data.</param>
        /// <returns>The result of the operator.</returns>
        public static explicit operator NavigatorFailedEventArgs(NavigationFailedEventArgs args)
            => FromNavigationFailedEventArgs(args);

        #endregion

    }

}