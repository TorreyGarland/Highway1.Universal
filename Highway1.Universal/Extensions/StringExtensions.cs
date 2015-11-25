namespace Highway1.Universal
{

    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Numerics;
    using System.Reflection;

    /// <summary>String extensions class/module.</summary>
    public static class StringExtensions
    {

        #region Methods

        [Pure]
        private static T? DateTryParse<T>(string value, IFormatProvider provider, DateTimeStyles style, DateTryParseHandler<T> handler)
            where T : struct
        {
            Contract.Requires(handler != null, nameof(handler));
            T result;
            if (handler(value, provider, style, out result))
                return result;
            return null;
        }

        [Pure]
        private static T? DateTryParseExact<T>(string value, string[] formats, IFormatProvider provider, DateTimeStyles style, DateTryParseExactHandler<T> handler)
            where T : struct
        {
            Contract.Requires(handler != null, nameof(handler));
            T result;
            if (handler(value, formats, provider, style, out result))
                return result;
            return null;
        }

        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrEmpty(this string value)
        {
            Contract.Ensures(Contract.Result<bool>() == string.IsNullOrEmpty(value));
            return string.IsNullOrEmpty(value);
        }



        /// <summary>
        /// Determines whether [is null or white space].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrWhiteSpace(this string value)
        {
            Contract.Ensures(Contract.Result<bool>() == string.IsNullOrWhiteSpace(value));
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>Determines whether the specified URI kind is URI.</summary>
        /// <param name="value">The value.</param>
        /// <param name="uriKind">Kind of the URI.</param>
        /// <returns></returns>
        [Pure]
        public static bool IsUri(this string value, UriKind uriKind = UriKind.Absolute)
            => value.ToUri(uriKind) != null;

        [Pure]
        private static T? NumericTryParse<T>(string value, NumberStyles style, IFormatProvider provider, NumericTryParseHandler<T> handler)
            where T : struct
        {
            Contract.Requires(handler != null, nameof(handler));
            T result;
            if (handler(value, style, provider, out result))
                return result;
            return null;
        }

        /// <summary>To the big integer.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static BigInteger? ToBigInteger(this string value)
            => value == null ? null : TryParse<BigInteger>(value, BigInteger.TryParse);

        /// <summary>To the big integer.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static BigInteger? ToBigInteger(this string value, NumberStyles style, IFormatProvider provider)
        {
            // TODO: Code contracts is reporting a warning with the following two lines because of the use of the Pure attribute.
            Contract.Requires<ArgumentException>(!style.HasFlag(NumberStyles.AllowHexSpecifier), nameof(style));
            Contract.Requires<ArgumentException>(!style.HasFlag(NumberStyles.HexNumber), nameof(style));
            return NumericTryParse<BigInteger>(value, style, provider, BigInteger.TryParse);
        }

        /// <summary>To the bool.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static bool? ToBool(this string value)
            => TryParse<bool>(value, bool.TryParse);

        /// <summary>To the byte.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static byte? ToByte(this string value)
            => TryParse<byte>(value, byte.TryParse);

        /// <summary>To the byte.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static byte? ToByte(this string value, NumberStyles style, IFormatProvider provider)
        {
            Contract.Requires<ArgumentException>(!style.HasFlag(NumberStyles.AllowHexSpecifier), nameof(style));
            Contract.Requires<ArgumentException>(!style.HasFlag(NumberStyles.HexNumber), nameof(style));
            return NumericTryParse<byte>(value, style, provider, byte.TryParse);
        }

        /// <summary>To the character.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static char? ToChar(this string value)
            => TryParse<char>(value, char.TryParse);

        /// <summary>To the date time.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static DateTime? ToDateTime(this string value)
            => TryParse<DateTime>(value, DateTime.TryParse);

        /// <summary>To the date time.</summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        [Pure]
        public static DateTime? ToDateTime(this string value, IFormatProvider provider, DateTimeStyles style)
            => DateTryParse<DateTime>(value, provider, style, DateTime.TryParse);

        /// <summary>To the date time exact.</summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <param name="formats">The formats.</param>
        /// <returns></returns>
        [Pure]
        public static DateTime? ToDateTimeExact(this string value, IFormatProvider provider, DateTimeStyles style, params string[] formats)
            => DateTryParseExact<DateTime>(value, formats, provider, style, DateTime.TryParseExact);

        /// <summary>To the date time offset.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static DateTimeOffset? ToDateTimeOffset(this string value)
            => TryParse<DateTimeOffset>(value, DateTimeOffset.TryParse);

        /// <summary>To the date time offset.</summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        [Pure]
        public static DateTimeOffset? ToDateTimeOffset(this string value, IFormatProvider provider, DateTimeStyles style)
            => DateTryParse<DateTimeOffset>(value, provider, style, DateTimeOffset.TryParse);

        /// <summary>To the date time offset.</summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        [Pure]
        public static DateTimeOffset? ToDateTimeOffset(this string value, string format, IFormatProvider provider, DateTimeStyles style)
            => DateTryParseExact<DateTimeOffset>(value, new[] { format }, provider, style, DateTimeOffset.TryParseExact);

        /// <summary>To the date time offset.</summary>
        /// <param name="value">The value.</param>
        /// <param name="formats">The formats.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        [Pure]
        public static DateTimeOffset? ToDateTimeOffset(this string value, string[] formats, IFormatProvider provider, DateTimeStyles style)
            => DateTryParseExact<DateTimeOffset>(value, formats, provider, style, DateTimeOffset.TryParseExact);

        /// <summary>To the decimal.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static decimal? ToDecimal(this string value)
            => TryParse<decimal>(value, decimal.TryParse);

        /// <summary>To the decimal.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static decimal? ToDecimal(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<decimal>(value, style, provider, decimal.TryParse);

        /// <summary>To the double.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static double? ToDouble(this string value)
            => TryParse<double>(value, double.TryParse);

        /// <summary>To the double.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static double? ToDouble(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<double>(value, style, provider, double.TryParse);

        /// <summary>To the enum.</summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        [Pure]
        public static TEnum? ToEnum<TEnum>(this string value, bool ignoreCase = false) where TEnum : struct
        {
        //    Contract.Requires<ArgumentException>(typeof(TEnum).GetTypeInfo().IsEnum, nameof(TEnum));
            TEnum result;
            if (Enum.TryParse(value, ignoreCase, out result))
                return result;
            return null;
        }

        /// <summary>To the float.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static float? ToFloat(this string value)
            => TryParse<float>(value, float.TryParse);

        /// <summary>To the float.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static float? ToFloat(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<float>(value, style, provider, float.TryParse);

        /// <summary>To the unique identifier.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static Guid? ToGuid(this string value)
            => TryParse<Guid>(value, Guid.TryParse);

        /// <summary>To the unique identifier exact.</summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        [Pure]
        public static Guid? ToGuidExact(this string value, string format)
        {
            Guid result;
            if (Guid.TryParseExact(value, format, out result))
                return result;
            return null;
        }

        /// <summary>To the int.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static int? ToInt(this string value)
            => TryParse<int>(value, int.TryParse);

        /// <summary>To the int.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static int? ToInt(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<int>(value, style, provider, int.TryParse);

        /// <summary>To the long.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static long? ToLong(this string value)
            => TryParse<long>(value, long.TryParse);

        /// <summary>To the long.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static long? ToLong(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<long>(value, style, provider, long.TryParse);

        /// <summary>To the sbyte.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static sbyte? ToSbyte(this string value)
            => TryParse<sbyte>(value, sbyte.TryParse);

        /// <summary>To the sbyte.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static sbyte? ToSbyte(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<sbyte>(value, style, provider, sbyte.TryParse);

        /// <summary>To the short.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static short? ToShort(this string value)
            => TryParse<short>(value, short.TryParse);

        /// <summary>To the short.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static short? ToShort(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<short>(value, style, provider, short.TryParse);

        /// <summary>To the time span.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static TimeSpan? ToTimeSpan(this string value)
            => TryParse<TimeSpan>(value, TimeSpan.TryParse);

        /// <summary>To the time span.</summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static TimeSpan? ToTimeSpan(this string value, IFormatProvider provider)
        {
            TimeSpan result;
            if (TimeSpan.TryParse(value, provider, out result))
                return result;
            return null;
        }

        /// <summary>To the time span exact.</summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static TimeSpan? ToTimeSpanExact(this string value, string format, IFormatProvider provider)
            => value.ToTimeSpanExact(new[] { format }, provider);

        /// <summary>To the time span exact.</summary>
        /// <param name="value">The value.</param>
        /// <param name="formats">The formats.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static TimeSpan? ToTimeSpanExact(this string value, string[] formats, IFormatProvider provider)
        {
            TimeSpan result;
            if (TimeSpan.TryParseExact(value, formats, provider, out result))
                return result;
            return null;
        }

        /// <summary>To the time span exact.</summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        [Pure]
        public static TimeSpan? ToTimeSpanExact(this string value, string format, IFormatProvider provider, TimeSpanStyles style)
            => value.ToTimeSpanExact(new[] { format }, provider, style);

        /// <summary>To the time span exact.</summary>
        /// <param name="value">The value.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <param name="formats">The formats.</param>
        /// <returns></returns>
        [Pure]
        public static TimeSpan? ToTimeSpanExact(this string value, string[] formats, IFormatProvider provider, TimeSpanStyles style)
        {
            TimeSpan result;
            if (TimeSpan.TryParseExact(value, formats, provider, style, out result))
                return result;
            return null;
        }

        /// <summary>To the type.</summary>
        /// <param name="value">The value.</param>
        /// <param name="throwOnError">if set to <c>true</c> [throw on error].</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        [Pure]
        public static Type ToType(this string value, bool throwOnError = false, bool ignoreCase = false)
        {
            if (value.IsNullOrWhiteSpace())
                return null;
            return Type.GetType(value, throwOnError, ignoreCase);
        }

        /// <summary>To the uint.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static uint? ToUint(this string value)
            => TryParse<uint>(value, uint.TryParse);

        /// <summary>To the uint.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static uint? ToUint(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<uint>(value, style, provider, uint.TryParse);

        /// <summary>To the ulong.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static ulong? ToUlong(this string value)
            => TryParse<ulong>(value, ulong.TryParse);

        /// <summary>To the ulong.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static ulong? ToUlong(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<ulong>(value, style, provider, ulong.TryParse);

        /// <summary>To the ushort.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static ushort? ToUshort(this string value)
            => TryParse<ushort>(value, ushort.TryParse);

        /// <summary>To the ushort.</summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [Pure]
        public static ushort? ToUshort(this string value, NumberStyles style, IFormatProvider provider)
            => NumericTryParse<ushort>(value, style, provider, ushort.TryParse);

        /// <summary>To the URI.</summary>
        /// <param name="value">The value.</param>
        /// <param name="uriKind">Kind of the URI.</param>
        /// <returns></returns>
        [Pure]
        public static Uri ToUri(this string value, UriKind uriKind = UriKind.Absolute)
        {
            Uri uri;
            Uri.TryCreate(value, uriKind, out uri);
            return uri;
        }

        /// <summary>To the version.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static Version ToVersion(this string value)
        {
            Version version;
            Version.TryParse(value, out version);
            return version;
        }

        [Pure]
        private static T? TryParse<T>(string value, TryParseHandler<T> handler)
            where T : struct
        {
            Contract.Requires(handler != null, nameof(handler));
            T result;
            if (handler(value, out result))
                return result;
            return null;
        }

        #endregion

        #region Nested Types

        private delegate bool DateTryParseHandler<T>(string value, IFormatProvider provider, DateTimeStyles style, out T result);

        private delegate bool DateTryParseExactHandler<T>(string value, string[] formats, IFormatProvider provider, DateTimeStyles style, out T result);

        private delegate bool NumericTryParseHandler<T>(string value, NumberStyles style, IFormatProvider provider, out T result);

        private delegate bool TryParseHandler<T>(string value, out T result);

        #endregion

    }

}