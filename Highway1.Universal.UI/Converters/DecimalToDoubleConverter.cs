namespace Highway1.Universal.UI.Converters
{

    using System;
    using System.Diagnostics;
    using Windows.UI.Xaml.Data;

    /// <summary>Decimal to double value converter class.</summary>
    /// <seealso cref="IValueConverter" />
    public sealed class DecimalToDoubleValueConverter : IValueConverter
    {

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object Convert(object value, Type targetType, object parameter, string language) 
            => value is decimal ? decimal.ToDouble((decimal)value) : default(double);

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object ConvertBack(object value, Type targetType, object parameter, string language) 
            => value is double ? (decimal)((double)value) : default(decimal);

    }

}