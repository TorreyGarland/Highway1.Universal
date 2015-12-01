namespace Highway1.Universal.UI.Controls
{

    using System;
    using System.Diagnostics;
    using Windows.System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    /// <summary>Up/down text box class.</summary>
    /// <seealso cref="TextBox" />
    [DebuggerStepThrough]
    public sealed partial class UpDownTextBox : TextBox
    {

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="UpDownTextBox" /> class.
        /// </summary>
        public UpDownTextBox()
        {
            DefaultStyleKey = typeof(TextBox);
        }

        /// <summary>Raises the <see cref="E:KeyDown" /> event.</summary>
        /// <param name="e">The <see cref="KeyRoutedEventArgs" /> instance containing the event data.</param>
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Up)
            {
                UpPressed?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            if (e.Key == VirtualKey.Down)
            {
                DownPressed?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            base.OnKeyDown(e);
        }

        #endregion

        #region Events

        /// <summary>Occurs when [down pressed].</summary>
        public event EventHandler DownPressed;

        /// <summary>Occurs when [up pressed].</summary>
        public event EventHandler UpPressed;

        #endregion

    }

}