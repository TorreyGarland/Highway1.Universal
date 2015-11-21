namespace Highway1.Universal.Services
{

    using System;
    using System.Diagnostics.Contracts;

    partial class Navigator
    {

        /// <summary>Registers this instance.</summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <returns></returns>
        public INavigatorViewTypeCache Register<TViewModel>()
        {
            Contract.Ensures(Contract.Result<INavigatorViewTypeCache>() != null, nameof(Register));
            return Register(typeof(TViewModel));
        }

        /// <summary>Registers the specified view model type.</summary>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns></returns>
        public INavigatorViewTypeCache Register(Type viewModelType)
        {
            Contract.Requires<ArgumentNullException>(viewModelType != null, nameof(viewModelType));
            Contract.Ensures(Contract.Result<INavigatorViewTypeCache>() != null, nameof(Register));
            return _registrations.GetOrAdd(viewModelType, (key) => new NavigatorViewTypeCache(this));
        }

    }

}