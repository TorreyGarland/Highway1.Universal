namespace Highway1.Universal.ComponentModel
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>Property changed event source class.</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PropertyChangeEventSource<T> : FrameworkElement
    {

        #region Fields

        /// <summary>The value property</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static DependencyProperty ValueProperty { get; } 
            = DependencyProperty.Register(nameof(Value), typeof(T), typeof(PropertyChangeEventSource<T>), new PropertyMetadata(default(T), OnValueChanged));

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

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangeEventSource{T}" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingMode">The binding mode.</param>
        [DebuggerNonUserCode]
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

        [DebuggerNonUserCode]
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => (d as PropertyChangeEventSource<T>)?.OnValueChanged((T)e.OldValue, (T)e.NewValue);

        [DebuggerNonUserCode]
        private void OnValueChanged(T oldValue, T newValue) 
            => ValueChanged?.Invoke(_source, newValue);

        #endregion

        #region Events

        /// <summary>Occurs when [value changed].</summary>
        public event EventHandler<T> ValueChanged;

        #endregion

    }

}