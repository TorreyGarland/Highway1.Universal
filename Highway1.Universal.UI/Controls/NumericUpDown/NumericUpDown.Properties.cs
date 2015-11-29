namespace Highway1.Universal.UI.Controls
{

    using Windows.UI.Xaml;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    partial class NumericUpDown
    {

        /// <summary>Gets or sets the drag speed.</summary>
        /// <value>The drag speed.</value>
        public double DragSpeed
        {
            get { return (double)GetValue(DragSpeedProperty); }
            set { SetValue(DragSpeedProperty, value); }
        }

        /// <summary>Gets or sets the header.</summary>
        /// <value>The header.</value>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>Gets or sets the header template.</summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>Gets or sets the text alignment.</summary>
        /// <value>The text alignment.</value>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        /// <summary>Gets or sets the text box style.</summary>
        /// <value>The text box style.</value>
        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        /// <summary>Gets or sets the value bar visibility.</summary>
        /// <value>The value bar visibility.</value>
        public Visibility ValueBarVisibility
        {
            get { return (Visibility)GetValue(ValueBarVisibilityProperty); }
            set { SetValue(ValueBarVisibilityProperty, value); }
        }

        /// <summary>Gets or sets the value format.</summary>
        /// <value>The value format.</value>
        public string ValueFormat
        {
            get { return (string)GetValue(ValueFormatProperty); }
            set { SetValue(ValueFormatProperty, value); }
        }

    }
}
