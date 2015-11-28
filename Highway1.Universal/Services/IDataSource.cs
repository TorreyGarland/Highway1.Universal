using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highway1.Universal.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSource
    {

        /// <summary>Gets the cultures.</summary>
        /// <value>The cultures.</value>
        IReadOnlyCollection<System.Globalization.CultureInfo> Cultures { get; }

        /// <summary>Gets the cultures.</summary>
        /// <returns></returns>
        IReadOnlyCollection<System.Globalization.CultureInfo> GetCultures();

    }
}
