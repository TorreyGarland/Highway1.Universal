namespace Highway1.Universal.UI.Converters
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Data;

    public sealed class DateTimeToDateTimeOffsetConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
                return new DateTimeOffset((DateTime)value);
            return DateTimeOffset.MinValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset)
                return ((DateTimeOffset)value).DateTime;
            return DateTime.MinValue;
        }

    }

    public sealed class DateTimeOffsetToDateTimeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset)
                return ((DateTimeOffset)value).DateTime;
            return DateTime.MinValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
                return new DateTimeOffset((DateTime)value);
            return DateTimeOffset.MinValue;
        }

    }

}