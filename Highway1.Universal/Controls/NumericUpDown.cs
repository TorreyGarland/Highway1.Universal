namespace Highway1.Universal.Controls
{

    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Windows.Devices.Input;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Input;
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
            if (Math.Sign(delta) == Math.Sign(_unusedManipulationDelta))
                _unusedManipulationDelta += delta;
            else
                _unusedManipulationDelta = delta;
            if (_unusedManipulationDelta <= 0 && Value.Equals(Minimum))
            {
                _unusedManipulationDelta = 0;
                return;
            }
            if (_unusedManipulationDelta >= 0 && Value.Equals(Maximum))
            {
                _unusedManipulationDelta = 0;
                return;
            }
            double smallerScreenDimension;
            if (Window.Current != null)
            {
                // TODO: Remove when code contracts are fixed.
                dynamic current = Window.Current;
                dynamic bounds = current.Bounds;
                double width = bounds.Width;
                double height = bounds.Height;
                smallerScreenDimension = Math.Min(width, height);
            }
            else
                smallerScreenDimension = 768;
            var speed = DragSpeed;
            if (double.IsNaN(speed) || double.IsInfinity(speed))
                speed = Maximum - Minimum;
            if (double.IsNaN(speed) || double.IsInfinity(speed))
                speed = double.MaxValue;
            var screenAdjustedDelta = speed * _unusedManipulationDelta / smallerScreenDimension;
            SetValueAndUpdateValidDirections(Value + screenAdjustedDelta);
            _unusedManipulationDelta = 0;
        }

        private async void CoreWindowPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (!_isDragUpdated)
                return;
            args.Handled = true;
            await Task.Delay(DefaultDragDelay);
            if (_valueTextBox == null)
                return;
            _valueTextBox.IsTabStop = true;
        }

        private async void CoreWindowVisibilityChanged(CoreWindow sender, VisibilityChangedEventArgs args)
        {
            if (args.Visible)
                return;
            await EndDraggingAsync();
        }

        private bool Decrement()
            => SetValueAndUpdateValidDirections(Value - SmallChange);

        private void DecrementButtonClick(object sender, RoutedEventArgs e)
           => Decrement();

        private async Task EndDraggingAsync(PointerRoutedEventArgs e = null)
        {
            if (_isDraggingWithMouse)
            {
                _isDraggingWithMouse = false;
                if (_mouseDevice != null)
                    _mouseDevice.MouseMoved -= MouseDeviceMouseMoved;
#if WINDOWS_APP
                Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeAll, 1);
#endif
                _mouseDevice = null;
            }
            else if (_dragOverlay != null)
                _dragOverlay.ManipulationDelta -= DragOverlayManipulationDelta;
            if (!_isDragUpdated)
                return;
            if (e != null)
                e.Handled = true;
            await Task.Delay(DefaultDragDelay);
            if (_valueTextBox == null)
                return;
            _valueTextBox.IsTabStop = true;
        }

        private bool Increment()
            => SetValueAndUpdateValidDirections(Value + SmallChange);

        private void IncrementButtonClick(object sender, RoutedEventArgs e)
           => Increment();

        private void MouseDeviceMouseMoved(MouseDevice sender, MouseEventArgs args)
        {
            var dx = args.MouseDelta.X;
            var dy = args.MouseDelta.Y;
            var value = MinimumMouseDragDelta * 100;
            if (dx > value || dx < -value || dy > value || dy < -value)
                return;
            _totalDeltaX += dx;
            _totalDeltaY += dy;
            if (_totalDeltaX > MinimumMouseDragDelta || _totalDeltaX < -MinimumMouseDragDelta || _totalDeltaY > MinimumMouseDragDelta || _totalDeltaY < -MinimumMouseDragDelta)
            {
                UpdateByDragging(_totalDeltaX, _totalDeltaY);
                _totalDeltaX = 0;
                _totalDeltaY = 0;
            }
        }

        /// <summary>Called when [apply template].</summary>
        protected override void OnApplyTemplate()
        {
            #region Remove Handlers

            if (_valueTextBox != null)
            {
                _valueTextBox.LostFocus -= ValueTextBoxLostFocus;
                _valueTextBox.GotFocus -= ValueTextBoxGotFocus;
                _valueTextBox.TextChanged -= ValueTextBoxTextChanged;
                _valueTextBox.KeyDown -= ValueTextBoxKeyDown;
                _valueTextBox.UpPressed -= ValueTextBoxUpPressed;
                _valueTextBox.DownPressed -= ValueTextBoxDownPressed;
                _valueTextBox.PointerExited -= ValueTextBoxPointerExited;
            }

            if (_dragOverlay != null)
            {
                _dragOverlay.Tapped -= DragOverlayTapped;
                _dragOverlay.PointerPressed -= DragOverlayPointerPressed;
                _dragOverlay.PointerReleased -= DragOverlayPointerReleased;
                _dragOverlay.PointerCaptureLost -= DragOverlayPointerCaptureLost;
            }

            if (_decrementButton != null)
                _decrementButton.Click -= DecrementButtonClick;

            if (_incrementButton != null)
                _incrementButton.Click -= IncrementButtonClick;

            if (_valueBar != null)
                _valueBar.SizeChanged -= ValueBarSizeChanged;

            #endregion

            base.OnApplyTemplate();

            _dragOverlay = GetTemplateChild(DragOverlayName) as UIElement;
            _valueBar = GetTemplateChild(ValueBarName) as FrameworkElement;
            _valueTextBox = GetTemplateChild(ValueTextBoxName) as UpDownTextBox;
            _decrementButton = GetTemplateChild(DecrementButtonName) as RepeatButton;
            _incrementButton = GetTemplateChild(IncrementButtonName) as RepeatButton;

            #region Add Handlers

            if (_valueTextBox != null)
            {
                _valueTextBox.LostFocus += ValueTextBoxLostFocus;
                _valueTextBox.GotFocus += ValueTextBoxGotFocus;
                _valueTextBox.Text = ValueFormat != null ? Value.ToString(ValueFormat) : Value.ToString();
                _valueTextBox.TextChanged += ValueTextBoxTextChanged;
                _valueTextBox.KeyDown += ValueTextBoxKeyDown;
                _valueTextBox.UpPressed += ValueTextBoxUpPressed;
                _valueTextBox.DownPressed += ValueTextBoxDownPressed;
                _valueTextBox.PointerExited += ValueTextBoxPointerExited;
            }

            if (_dragOverlay != null)
            {
                _dragOverlay.Tapped += DragOverlayTapped;
                _dragOverlay.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
                _dragOverlay.PointerPressed += DragOverlayPointerPressed;
                _dragOverlay.PointerReleased += DragOverlayPointerReleased;
                _dragOverlay.PointerCaptureLost += DragOverlayPointerCaptureLost;
            }

            if (_incrementButton != null)
                _incrementButton.Click += IncrementButtonClick;

            if (_decrementButton != null)
                _decrementButton.Click += DecrementButtonClick;

            if (_valueBar != null)
            {
                _valueBar.SizeChanged += ValueBarSizeChanged;
                UpdateValueBar();
            }

            #endregion

            SetValidIncrementDirection();
        }

        /// <summary>Raises the <see cref="E:GotFocus" /> event.</summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
            => _hasFocus = true;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerPressed += CoreWindowPointerPressed;
            Window.Current.CoreWindow.VisibilityChanged += CoreWindowVisibilityChanged;
        }

        /// <summary>Raises the <see cref="E:LostFocus" /> event.</summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
            => _hasFocus = false;

        /// <summary>Raises the <see cref="E:PointerWheelChanged" /> event.</summary>
        /// <param name="e">The <see cref="PointerRoutedEventArgs" /> instance containing the event data.</param>
        protected override void OnPointerWheelChanged(PointerRoutedEventArgs e)
        {
            base.OnPointerWheelChanged(e);
            if (!_hasFocus)
                return;
            var delta = (e.GetCurrentPoint(this)?.Properties?.MouseWheelDelta).GetValueOrDefault();
            if (delta < 0)
                Decrement();
            else
                Increment();
            e.Handled = true;
        }

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

        private void UpdateValueBar()
        {
            if (_valueBar == null)
                return;
            if (ValueBarVisibility == Visibility.Collapsed)
            {
                _valueBar.Visibility = Visibility.Collapsed;
                return;
            }
            // TODO:  Fix this when code contracts get fixed.
            dynamic rect = Activator.CreateInstance(Type.GetType(ControlExtensions.RectAssemblyFullQualifiedName));
            rect.X = 0;
            rect.Y = 0;
            rect.Height = _valueBar.ActualHeight;
            rect.Width = _valueBar.ActualWidth * (Value - Minimum) / (Maximum - Minimum);
            dynamic rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = rect;
            _valueBar.Clip = rectangleGeometry;
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
            if (_valueTextBox == null)
                return;
            _isChangingTextWithCode = true;
            _valueTextBox.Text = ValueFormat != null ? Value.ToString(ValueFormat) : Value.ToString();
            _isChangingTextWithCode = false;
        }

    }

}