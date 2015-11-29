namespace Highway1.Universal.UI.Controls
{

    using System;
    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>Control extensions class/module.</summary>
    public static partial class ControlExtensions
    {

        #region Properties

        /// <summary>Gets the system cursor property.</summary>
        /// <value>The system cursor property.</value>
        public static DependencyProperty SystemCursorProperty { get; }
            = DependencyProperty.RegisterAttached("SystemCursor", typeof(CoreCursorType), typeof(ControlExtensions), new PropertyMetadata(CoreCursorType.Arrow, OnSystemCursorChanged));

        #endregion

        #region Methods

        /// <summary>Gets the system cursor.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static CoreCursorType GetSystemCursor(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
                throw new ArgumentNullException(nameof(dependencyObject));
            return (CoreCursorType)dependencyObject.GetValue(SystemCursorProperty);
        }

        private static void OnSystemCursorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newCursorType = (CoreCursorType)e.NewValue;
        }

        /// <summary>Sets the system cursor.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetSystemCursor(this DependencyObject dependencyObject, CoreCursorType value)
        {
            if (dependencyObject == null)
                throw new ArgumentNullException(nameof(dependencyObject));
            dependencyObject.SetValue(SystemCursorProperty, value);
        }

        #endregion

    }

}