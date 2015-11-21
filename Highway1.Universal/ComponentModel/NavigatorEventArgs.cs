namespace Highway1.Universal.ComponentModel
{

    using System;
    using System.Diagnostics.Contracts;
    using Windows.UI.Xaml.Navigation;

    /// <summary>Navigator event arguments class.</summary>
    public sealed class NavigatorEventArgs : NavigatorEventArgsBase
    {

        #region Fields

        private static readonly NavigatorEventArgs _empty = new NavigatorEventArgs(NavigationMode.New, null);

        #endregion

        #region Properties

        /// <summary>Gets the empty.</summary>
        /// <value>The empty.</value>
        public static new NavigatorEventArgs Empty
        {
            get
            {
                Contract.Ensures(Contract.Result<NavigatorEventArgs>() != null, nameof(Empty));
                return _empty;
            }
        }

        /// <summary>Gets or sets the URI.</summary>
        /// <value>The URI.</value>
        public Uri Uri { get; set; }

        #endregion

        #region Methods

        private NavigatorEventArgs(NavigationMode mode, object parameter)
            : base(mode, parameter)
        {
        }

        /// <summary>Froms the navigation event arguments.</summary>
        /// <param name="args">The <see cref="NavigationEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        [Pure]
        public static NavigatorEventArgs FromNavigationEventArgs(NavigationEventArgs args)
            => args != null ? new NavigatorEventArgs(args.NavigationMode, args.Parameter) { Uri = args.Uri } : null;

        /// <summary>
        /// Performs an explicit conversion from <see cref="NavigationEventArgs" /> to <see cref="NavigatorEventArgs" />.
        /// </summary>
        /// <param name="args">The <see cref="NavigationEventArgs" /> instance containing the event data.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator NavigatorEventArgs(NavigationEventArgs args)
            => FromNavigationEventArgs(args);

        #endregion

    }

}