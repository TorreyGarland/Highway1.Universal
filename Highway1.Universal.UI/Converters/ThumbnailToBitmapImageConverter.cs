namespace Highway1.Universal.UI.Converters
{

    using System;
    using Windows.Storage.FileProperties;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>Thumbnail to bitmap image converter class.</summary>
    public sealed class ThumbnailToBitmapImageConverter : IValueConverter
    {

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage image = null;
            var thumbnail = value as StorageItemThumbnail;
            if (thumbnail != null)
            {
                image = new BitmapImage();
                image.SetSource(thumbnail);
            }
            return image;
        }

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }

}