namespace Highway1.Universal.UI.Controls
{

    using System;
    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    partial class NumericUpDown
    {

        private void ValueBarSizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateValueBar();

        private void ValueTextBoxDownPressed(object sender, EventArgs e)
            => Decrement();

        private void ValueTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (_dragOverlay != null)
                _dragOverlay.IsHitTestVisible = false;
            _valueTextBox?.SelectAll();
        }

        private void ValueTextBoxKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && UpdateValueFromText())
            {
                UpdateValueText();
                _valueTextBox?.SelectAll();
                e.Handled = true;
            }
        }

        private void ValueTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (_valueTextBox == null)
                return;
            _dragOverlay.IsHitTestVisible = true;
            UpdateValueText();
        }

        private void ValueTextBoxPointerExited(object sender, PointerRoutedEventArgs e)
        {
#if WINDOWS_APP
            if (Window.Current.CoreWindow.PointerCursor.Type == CoreCursorType.IBeam)
                Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
#endif
        }

        private void ValueTextBoxUpPressed(object sender, EventArgs e)
            => Increment();

        private void ValueTextBoxTextChanged(object sender, TextChangedEventArgs e)
            => UpdateValueFromText();

    }

}