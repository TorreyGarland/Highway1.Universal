namespace Highway1.Universal.Collections
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;

    /// <summary>Route value observable dictionary class.</summary>
    public class RouteValueObservableDictionary : ObservableDictionary<string, object>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueObservableDictionary" /> class.
        /// </summary>
        public RouteValueObservableDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueObservableDictionary" /> class.
        /// </summary>
        /// <param name="capacity"></param>
        public RouteValueObservableDictionary(int capacity)
            : base(capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, nameof(capacity));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueObservableDictionary" /> class.
        /// </summary>
        /// <param name="comparer"></param>
        public RouteValueObservableDictionary(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueObservableDictionary" /> class.
        /// </summary>
        /// <param name="dictionary"></param>
        public RouteValueObservableDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, nameof(dictionary));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueObservableDictionary"/> class.
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public RouteValueObservableDictionary(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, nameof(capacity));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteValueObservableDictionary"/> class.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="comparer"></param>
        public RouteValueObservableDictionary(IDictionary<string, object> dictionary, IEqualityComparer<string> comparer)
            : base(dictionary, comparer)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, nameof(dictionary));
        }

        /// <summary>Creates the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        [Pure]
        public static RouteValueObservableDictionary Create(object item)
        {
            Contract.Ensures(Contract.Result<RouteValueObservableDictionary>() != null, nameof(Create));
            if (item != null)
            {
                var properties = item.GetType().GetRuntimeProperties().ToDictionary(x => x.Name, x => x.GetValue(item, null));
                return new RouteValueObservableDictionary(properties, StringComparer.CurrentCultureIgnoreCase);
            }
            return new RouteValueObservableDictionary();
        }

    }

}