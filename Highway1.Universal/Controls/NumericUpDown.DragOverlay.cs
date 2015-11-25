namespace Highway1.Universal.Controls
{

    using Windows.Devices.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;

    partial class NumericUpDown
    {

        private void DragOverlayManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // TODO: Remove dynamics when the Code Contracts team gets their shit together.
            dynamic delta = e.Delta;
            dynamic translation = delta.Translation;
            double dx = translation.X;
            double dy = translation.Y;
            if (UpdateByDragging(dx, dy))
                return;
            e.Handled = true;
        }

        private async void DragOverlayPointerCaptureLost(object sender, PointerRoutedEventArgs e)
            => await EndDraggingAsync();

        private async void DragOverlayPointerReleased(object sender, PointerRoutedEventArgs e)
            => await EndDraggingAsync(e);

        private void DragOverlayPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _dragOverlay?.CapturePointer(e.Pointer);
            _totalDeltaX = 0;
            _totalDeltaY = 0;
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                _isDraggingWithMouse = true;
                _mouseDevice = MouseDevice.GetForCurrentView();
                if (_mouseDevice != null)
                    _mouseDevice.MouseMoved += MouseDeviceMouseMoved;
#if WINDOWS_APP
                Window.Current.CoreWindow.PointerCursor = null;
#endif
            }
            else if (_dragOverlay != null)
                _dragOverlay.ManipulationDelta += DragOverlayManipulationDelta;
        }

        private void DragOverlayTapped(object sender, TappedRoutedEventArgs e)
        {
            if (IsEnabled && (_valueTextBox != null && _valueTextBox.IsTabStop))
            {
                _valueTextBox.Focus(FocusState.Programmatic);
#if WINDOWS_APP
                Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.IBeam, 0);
#endif
            }
        }

    }

}