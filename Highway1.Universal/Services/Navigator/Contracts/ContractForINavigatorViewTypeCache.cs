namespace Highway1.Universal.Services.Contracts
{

    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(INavigatorViewTypeCache))]
    internal abstract class ContractForINavigatorViewTypeCache : INavigatorViewTypeCache
    {

        #region Properties

        public abstract Type ViewType { get; }

        #endregion

        #region Methods

        public INavigator For(Type viewType)
        {
            Contract.Requires<ArgumentNullException>(viewType != null, nameof(viewType));
            Contract.Ensures(Contract.Result<INavigator>() != null, nameof(For));
            return default(INavigator);
        }

        public INavigator For<TView>()
        {
            Contract.Ensures(Contract.Result<INavigator>() != null, nameof(For));
            return default(INavigator);
        }

        #endregion

    }

}