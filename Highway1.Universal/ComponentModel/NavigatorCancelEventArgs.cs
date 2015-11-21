namespace Highway1.Universal.ComponentModel
{
    using System.Diagnostics.Contracts;
    using Windows.UI.Xaml.Navigation;

    /// <summary>Navigator cancel event arguments class.</summary>
    public sealed class NavigatorCancelEventArgs : NavigatorEventArgsBase
    {

        #region Fields

        private static readonly NavigatorCancelEventArgs _empty = new NavigatorCancelEventArgs(NavigationMode.New, null);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NavigatorCancelEventArgs" /> is cancel.
        /// </summary>
        /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel { get; set; }

        /// <summary>Gets the empty.</summary>
        /// <value>The empty.</value>
        public static new NavigatorCancelEventArgs Empty
        {
            get
            {
                Contract.Ensures(Contract.Result<NavigatorCancelEventArgs>() != null, nameof(Empty));
                return _empty;
            }
        }

        #endregion

        #region Methods

        private NavigatorCancelEventArgs(NavigationMode mode, object parameter)
            : base(mode, parameter)
        {
        }

        /// <summary>Froms the navigating cancel event arguments.</summary>
        /// <param name="args">The <see cref="NavigatingCancelEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        [Pure]
        public static NavigatorCancelEventArgs FromNavigatingCancelEventArgs(NavigatingCancelEventArgs args)
            => args != null ? new NavigatorCancelEventArgs(args.NavigationMode, args.Parameter) { Cancel = args.Cancel } : null;

        /// <summary>Implements the operator explicit NavigatorCancelEventArgs.</summary>
        /// <param name="args">The <see cref="NavigatingCancelEventArgs" /> instance containing the event data.</param>
        /// <returns>The result of the operator.</returns>
        public static explicit operator NavigatorCancelEventArgs(NavigatingCancelEventArgs args)
            => FromNavigatingCancelEventArgs(args);

        #endregion

    }

}