namespace Highway1.Universal.UI.Controls
{

    using Windows.Devices.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Primitives;

    partial class NumericUpDown
    {

        private RepeatButton _decrementButton;

        private UIElement _dragOverlay;

        private bool _hasFocus;

        private RepeatButton _incrementButton;

        private bool _isChangingTextWithCode;

        private bool _isChangingValueWithCode;

        private bool _isDraggingWithMouse;

        private bool _isDragUpdated;

        private MouseDevice _mouseDevice;

        private double _unusedManipulationDelta;

        private double _totalDeltaX;

        private double _totalDeltaY;

        private FrameworkElement _valueBar;

        private UpDownTextBox _valueTextBox;

    }

}