namespace Highway1.Universal.UI.Converters
{

    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>Zero index converter class.</summary>
    /// <seealso cref="IValueConverter" />
    public class ZeroIndexConverter : IValueConverter
    {

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int)
            {
                var count = (int)value;
                return count - 1;
            }
            else if (value is long)
            {
                var count = (int)value;
                return count - 1;

            }
            else if (value is double)
            {
                var count = (double)value;
                if (!double.IsNaN(count))
                    return count - 1;
            }
            return 0;
        }

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language) => 0;

    }

}