namespace Highway1.Universal.Controls
{

    using System;
    using System.Threading.Tasks;
    using Windows.Devices.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using System.Diagnostics;
    using System.Reflection;
    using Windows.Foundation;
    using Windows.System;
    using Windows.UI.Core;
    using Windows.UI.Input;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using System.Globalization;
    using Windows.UI.Xaml.Media;

    /// <summary>Numeric up/down class.</summary>
    [TemplatePart(Name = DecrementButtonName, Type = typeof(RepeatButton))]
    [TemplatePart(Name = DragOverlayName, Type = typeof(UIElement))]
    [TemplatePart(Name = IncrementButtonName, Type = typeof(RepeatButton))]
    [TemplatePart(Name = ValueBarName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ValueTextBoxName, Type = typeof(TextBox))]
    [TemplatePart(Name = IncrementButtonName, Type = typeof(RepeatButton))]
    [TemplateVisualState(GroupName = IncrementalButtonsStatesGroupName, Name = IncrementDisabledStateName)]
    [TemplateVisualState(GroupName = IncrementalButtonsStatesGroupName, Name = IncrementEnabledStateName)]
    [TemplateVisualState(GroupName = DecrementalButtonsStatesGroupName, Name = DecrementDisabledStateName)]
    [TemplateVisualState(GroupName = DecrementalButtonsStatesGroupName, Name = DecrementEnabledStateName)]
    public sealed partial class NumericUpDown : RangeBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericUpDown" /> class.
        /// </summary>
        public NumericUpDown()
        {
            DefaultStyleKey = typeof(NumericUpDown);
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void ApplyManipulationDelta(double delta)
        {

        }

        private void CoreWindowPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
        }

        private void CoreWindowVisibilityChanged(CoreWindow sender, VisibilityChangedEventArgs args)
        {

        }

        /// <summary>Raises the <see cref="E:GotFocus" /> event.</summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
            => _hasFocus = true;

        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerPressed += CoreWindowPointerPressed;
            Window.Current.CoreWindow.VisibilityChanged += CoreWindowVisibilityChanged;
        }

        /// <summary>Raises the <see cref="E:LostFocus" /> event.</summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
            => _hasFocus = false;

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerPressed -= CoreWindowPointerPressed;
            Window.Current.CoreWindow.VisibilityChanged -= CoreWindowVisibilityChanged;
        }

        private static void OnValueBarVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NumericUpDown)d).UpdateValueBar();

        /// <summary>
        /// Called when [value changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            UpdateValueBar();
            if (!_isChangingValueWithCode)
                UpdateValueText();
        }

        private static void OnValueFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NumericUpDown)d).UpdateValueText();

        private void SetValidIncrementDirection()
        {
            if (Value < Maximum)
                VisualStateManager.GoToState(this, IncrementEnabledStateName, true);
            if (Value > Minimum)
                VisualStateManager.GoToState(this, DecrementEnabledStateName, true);
            if (Value.Equals(Maximum))
                VisualStateManager.GoToState(this, IncrementDisabledStateName, true);
            if (Value.Equals(Minimum))
                VisualStateManager.GoToState(this, DecrementDisabledStateName, true);
        }

        private bool SetValueAndUpdateValidDirections(double value)
        {
            var oldValue = value;
            Value = value;
            SetValidIncrementDirection();
            return Value != oldValue;
        }

        private bool UpdateByDragging(double dx, double dy)
        {
            if (!IsEnabled || IsReadOnly || (dx.Equals(0) && dy.Equals(0)))
                return false;
            ApplyManipulationDelta(Math.Abs(dx) > Math.Abs(dy) ? dx : -dy);
            if (_valueTextBox != null)
                _valueTextBox.IsTabStop = false;
            _isDragUpdated = true;
            return true;
        }

        private void UpdateIsReadOnly()
        {
            //if(_decrementButton != null)
            //    _decrementButton.Visibility = IsReadOnly ? Visibility.Collapsed : 
        }

        private void UpdateValueBar()
        {
            if (_valueBar == null)
                return;
            if(ValueBarVisibility == Visibility.Collapsed)
            {
                _valueBar.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private bool UpdateValueFromText()
        {
            if (_isChangingTextWithCode)
                return false;
            double value;
            if (double.TryParse(_valueTextBox.Text, NumberStyles.Any, CultureInfo.CurrentUICulture, out value))
            {
                _isChangingValueWithCode = true;
                SetValueAndUpdateValidDirections(value);
                _isChangingValueWithCode = false;
                return true;
            }
            return false;
        }

        

        private void UpdateValueText()
        {

        }

    }

}