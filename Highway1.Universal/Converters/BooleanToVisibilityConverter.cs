namespace Highway1.Universal.Converters
{

    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>Boolean to visiblity converter class.</summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
            => ((value is bool) && (bool)value) ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value is Visibility && (Visibility)value == Visibility.Visible;

    }

}