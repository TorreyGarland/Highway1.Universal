namespace Highway1.Universal.ComponentModel
{

    using System;
    using System.Diagnostics;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>Property changed event source class.</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PropertyChangeEventSource<T> : FrameworkElement
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DependencyObject _source;

        #endregion

        #region Properties

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>The value property</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static DependencyProperty ValueProperty { get; }
            = DependencyProperty.Register(nameof(Value), typeof(T), typeof(PropertyChangeEventSource<T>), new PropertyMetadata(default(T), OnValueChanged));

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangeEventSource{T}" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingMode">The binding mode.</param>
        [DebuggerStepThrough]
        public PropertyChangeEventSource(DependencyObject source, string propertyName, BindingMode bindingMode = BindingMode.TwoWay)
        {
            _source = source;
            SetBinding(ValueProperty, new Binding
            {
                Source = source,
                Path = new PropertyPath(propertyName),
                Mode = bindingMode
            });
        }

        [DebuggerStepThrough]
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => (d as PropertyChangeEventSource<T>)?.OnValueChanged((T)e.OldValue, (T)e.NewValue);

        [DebuggerStepThrough]
        private void OnValueChanged(T oldValue, T newValue) 
            => ValueChanged?.Invoke(_source, newValue);

        #endregion

        #region Events

        /// <summary>Occurs when [value changed].</summary>
        public event EventHandler<T> ValueChanged;

        #endregion

    }

}