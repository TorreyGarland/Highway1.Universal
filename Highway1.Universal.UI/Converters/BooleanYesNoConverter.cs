namespace Highway1.Universal.UI.Converters
{

    using System;
    using System.Diagnostics;

    /// <summary>Boolean yes/no converter class.</summary>
    [DebuggerStepThrough]
    public sealed class BooleanYesNoConverter 
    {

        #region Fields

        /// <summary>The default yes value</summary>
        public const string DefaultYesValue = "Yes";

        /// <summary>The default no value</summary>
        public const string DefaultNoValue = "No";

        #endregion

        #region Properties

        /// <summary>Gets or sets the yes value.</summary>
        /// <value>The yes value.</value>
        public string YesValue { get; set; } = DefaultYesValue;

        /// <summary>Gets or sets the no value.</summary>
        /// <value>The no value.</value>
        public string NoValue { get; set; } = DefaultNoValue;

        #endregion

        #region Methods

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                var result = (bool)value;
                return result ? (YesValue ?? DefaultYesValue) : (NoValue ?? DefaultNoValue);
            }
            return NoValue ?? DefaultNoValue;
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object ConvertBack(object value, Type targetType, object parameter, string language) 
            => string.Equals(YesValue ?? DefaultYesValue, value as string, StringComparison.CurrentCultureIgnoreCase);

        #endregion

    }
}
