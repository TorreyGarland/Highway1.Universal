namespace Highway1.Universal.Services
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Windows.ApplicationModel;

    /// <summary>Data source class.</summary>
    /// <seealso cref="Highway1.Universal.Services.IDataSource" />
    public partial class DataSource : IDataSource
    {

        #region Fields

        private IReadOnlyCollection<CultureInfo> _cultures;

        #endregion

        #region Properties

        /// <summary>Gets or sets the cultures.</summary>
        /// <value>The cultures.</value>
        public IReadOnlyCollection<CultureInfo> Cultures => _cultures ?? (_cultures = GetCultures());

        #endregion

        #region Methods

        /// <summary>Gets the cultures.</summary>
        /// <returns></returns>
        public virtual IReadOnlyCollection<CultureInfo> GetCultures()
        {
            if (DesignMode.DesignModeEnabled)
                return new List<CultureInfo>();
            try
            {
                var results = GetCultures(CultureTypes.AllCultures)
                    .Where(x => x != null && !string.IsNullOrWhiteSpace(x.Name))
                    .ToList();
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>Gets the cultures.</summary>
        /// <param name="cultureTypes">The culture types.</param>
        /// <returns></returns>
        public static IReadOnlyCollection<CultureInfo> GetCultures(CultureTypes cultureTypes)
        {
            var cultures = new List<CultureInfo>();
            EnumLocalesProcExDelegate enumCallback = (locale, flags, lParam) =>
            {
                try
                {
                    cultures.Add(new CultureInfo(locale));
                }
                catch (CultureNotFoundException)
                {
                    // This culture is not supported by .NET (not happened so far)
                    // Must be ignored.
                }
                return true;
            };

            if (EnumSystemLocalesEx(enumCallback, (LocaleType)cultureTypes, 0, (IntPtr)0) == false)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new LocalesRetrievalException("Win32 error " + errorCode +
                   " while trying to get the Windows locales");
            }

            // Add the two neutral cultures that Windows misses 
            // (CultureInfo.GetCultures adds them also):
            if (cultureTypes == CultureTypes.NeutralCultures || cultureTypes == CultureTypes.AllCultures)
            {
                //cultures.Add(new CultureInfo("zh-CHS"));
                //cultures.Add(new CultureInfo("zh-CHT"));
            }

            return new ReadOnlyCollection<CultureInfo>(cultures);
        }


        #endregion

        #region Nested Types

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool EnumSystemLocalesEx(EnumLocalesProcExDelegate pEnumProcEx, LocaleType dwFlags, int lParam, IntPtr lpReserved);

        private delegate bool EnumLocalesProcExDelegate(
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpLocaleString, LocaleType dwFlags, int lParam);

        /// <summary>
        /// 
        /// </summary>
        private enum LocaleType : uint
        {
            LocaleAll = 0x00000000,             // Enumerate all named based locales
            LocaleWindows = 0x00000001,         // Shipped locales and/or replacements for them
            LocaleSupplemental = 0x00000002,    // Supplemental locales only
            LocaleAlternateSorts = 0x00000004,  // Alternate sort locales
            LocaleNeutralData = 0x00000010,     // Locales that are "neutral" (language only, region data is default)
            LocaleSpecificData = 0x00000020,    // Locales that contain language and region data
        }

        /// <summary>
        /// 
        /// </summary>
        public enum CultureTypes : uint
        {
            /// <summary>The specific cultures</summary>
            SpecificCultures = LocaleType.LocaleSpecificData,
            /// <summary>The neutral cultures</summary>
            NeutralCultures = LocaleType.LocaleNeutralData,
            /// <summary>All cultures</summary>
            AllCultures = LocaleType.LocaleWindows
        }

        /// <summary>
        /// 
        /// </summary>
        public class LocalesRetrievalException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LocalesRetrievalException" /> class.
            /// </summary>
            /// <param name="message">The message.</param>
            public LocalesRetrievalException(string message)
                            : base(message)
            {
            }
        }

        #endregion

    }

}
