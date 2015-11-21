namespace Highway1.Universal.Services
{

    using Contracts;
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Navigator view type cache interface.</summary>
    [ContractClass(typeof(ContractForINavigatorViewTypeCache))]
    public interface INavigatorViewTypeCache
    {

        #region Properties

        /// <summary>Gets the type of the view.</summary>
        /// <value>The type of the view.</value>
        Type ViewType { get; }

        #endregion

        #region Methods

        /// <summary>Fors this instance.</summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <returns></returns>
        INavigator For<TView>();

        /// <summary>Fors the specified view type.</summary>
        /// <param name="viewType">Type of the view.</param>
        /// <returns></returns>
        INavigator For(Type viewType);

        #endregion

    }

}