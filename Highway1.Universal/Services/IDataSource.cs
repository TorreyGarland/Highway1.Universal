namespace Highway1.Universal.Services
{

    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>Data source interface.</summary>
    public interface IDataSource
    {

        /// <summary>Gets the cultures.</summary>
        /// <value>The cultures.</value>
        IReadOnlyCollection<CultureInfo> Cultures { get; }

        /// <summary>Gets the cultures.</summary>
        /// <returns></returns>
        IReadOnlyCollection<CultureInfo> GetCultures();

    }
    
}