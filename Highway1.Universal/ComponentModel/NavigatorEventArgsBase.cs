namespace Highway1.Universal.ComponentModel
{

    using System;
    using Windows.UI.Xaml.Navigation;

    /// <summary>Navigator event arguments base class.</summary>
    public abstract class NavigatorEventArgsBase : EventArgs
    {

        #region Properties

        /// <summary>Gets the mode.</summary>
        /// <value>The mode.</value>
        public NavigationMode Mode { get; }

        /// <summary>Gets the parameter.</summary>
        /// <value>The parameter.</value>
        public object Parameter { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatorEventArgsBase" /> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="parameter">The parameter.</param>
        protected NavigatorEventArgsBase(NavigationMode mode, object parameter)
        {
            Mode = mode;
            Parameter = parameter;
        }

        #endregion

    }

}