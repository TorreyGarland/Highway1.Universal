namespace Highway1.Universal.Services
{

    using ComponentModel;
    using System;

    partial class Navigator
    {

        /// <summary>Occurs when [navigated].</summary>
        public event EventHandler<NavigatorEventArgs> Navigated;

        /// <summary>Occurs when [navigating].</summary>
        public event EventHandler<NavigatorCancelEventArgs> Navigating;

        /// <summary>Occurs when [navigation failed].</summary>
        public event EventHandler<NavigatorFailedEventArgs> NavigationFailed;

        /// <summary>Occurs when [navigation stopped].</summary>
        public event EventHandler<NavigatorEventArgs> NavigationStopped;

    }

}