namespace Highway1.Universal.UI.Converters
{

    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>Boolean to visiblity converter class.</summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BooleanToVisibilityConverter"/> is invert.
        /// </summary>
        /// <value><c>true</c> if invert; otherwise, <c>false</c>.</value>
        public bool Invert { get; set; }

        #endregion

        #region Methods

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
            => ((value is bool) && (bool)value) ? (Invert ? Visibility.Collapsed : Visibility.Visible) : (Invert ? Visibility.Visible : Visibility.Collapsed);

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value is Visibility && (Visibility)value == (Invert ? Visibility.Collapsed : Visibility.Visible);

        #endregion

    }

}