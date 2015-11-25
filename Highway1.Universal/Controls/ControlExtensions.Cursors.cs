namespace Highway1.Universal.Controls
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using System.Diagnostics.Contracts;

    partial class ControlExtensions
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
        [Pure]
        public static CoreCursorType GetSystemCursor(this DependencyObject dependencyObject)
        {
            Contract.Requires<ArgumentNullException>(dependencyObject != null, nameof(dependencyObject));
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
            Contract.Requires<ArgumentNullException>(dependencyObject != null, nameof(dependencyObject));
            dependencyObject.SetValue(SystemCursorProperty, value);
        }

        #endregion

    }

}