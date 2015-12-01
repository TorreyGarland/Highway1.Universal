namespace Highway1.Universal.UI.Collections
{

    using System;
    using System.Collections.Generic;
    using Universal.Collections;
    using Windows.UI.Xaml;

    /// <summary>Framework element collection class.</summary>
    public sealed class FrameworkElementCollection : ObservableVector<FrameworkElement>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkElementCollection" /> class.
        /// </summary>
        public FrameworkElementCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkElementCollection" /> class.
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FrameworkElementCollection(IEnumerable<FrameworkElement> collection)
            : base(collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
        }

    }

}