namespace Highway1.Universal.Controls
{

    using Windows.UI.Xaml;

    partial class NumericUpDown
    {

        /// <summary>The drag speed property</summary>
        /// <value>The drag speed property.</value>
        public static DependencyProperty DragSpeedProperty { get; } 
            = DependencyProperty.Register(nameof(DragSpeed), typeof(double), typeof(NumericUpDown), new PropertyMetadata(double.NaN));

        /// <summary>The header property</summary>
        /// <value>The header property.</value>
        public static DependencyProperty HeaderProperty { get; } 
            = DependencyProperty.Register(nameof(Header), typeof(object), typeof(NumericUpDown), new PropertyMetadata(null));

        /// <summary>The header template property</summary>
        /// <value>The header template property.</value>
        public static DependencyProperty HeaderTemplateProperty { get; } 
            = DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(NumericUpDown), new PropertyMetadata(null));

        /// <summary>The is read only property</summary>
        /// <value>The is read only property.</value>
        public static DependencyProperty IsReadOnlyProperty { get; } 
            = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

        /// <summary>The text alignment property</summary>
        /// <value>The text alignment property.</value>
        public static DependencyProperty TextAlignmentProperty { get; } 
            = DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(NumericUpDown), new PropertyMetadata(TextAlignment.Left));

        /// <summary>Gets the text box style property.</summary>
        /// <value>The text box style property.</value>
        public static DependencyProperty TextBoxStyleProperty { get; } 
            = DependencyProperty.Register(nameof(TextBoxStyle), typeof(Style), typeof(NumericUpDown), new PropertyMetadata(null));

        /// <summary>The value bar visibility property</summary>
        /// <value>The value bar visibility property.</value>
        public static DependencyProperty ValueBarVisibilityProperty { get; } 
            = DependencyProperty.Register(nameof(ValueBarVisibility), typeof(Visibility), typeof(NumericUpDown), new PropertyMetadata(Visibility.Visible, OnValueBarVisibilityChanged));

        /// <summary>The value format property</summary>
        /// <value>The value format property.</value>
        public static DependencyProperty ValueFormatProperty { get; } 
            = DependencyProperty.Register(nameof(ValueFormat), typeof(string), typeof(NumericUpDown), new PropertyMetadata(null, OnValueFormatChanged));

    }

}