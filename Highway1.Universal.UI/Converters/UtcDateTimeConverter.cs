namespace Highway1.Universal.UI.Converters
{

    using System;
    using System.Diagnostics;
    using Windows.UI.Xaml.Data;

    /// <summary>UTC date time value converter class.</summary>
    public sealed class UtcDateTimeValueConverter : IValueConverter
    {

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object Convert(object value, Type targetType, object parameter, string language) 
            => value is DateTime ? ((DateTime)value).ToLocalTime() : DateTime.Now;

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object ConvertBack(object value, Type targetType, object parameter, string language) 
            => value is DateTime ? ((DateTime)value).ToUniversalTime() : DateTime.UtcNow;

    }

}