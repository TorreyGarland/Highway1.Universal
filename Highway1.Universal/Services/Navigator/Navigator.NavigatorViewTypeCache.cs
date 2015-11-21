namespace Highway1.Universal.Services
{

    using System;
    using System.Diagnostics.Contracts;

    partial class Navigator
    {

        private sealed class NavigatorViewTypeCache : INavigatorViewTypeCache
        {

            #region Fields

            private readonly Navigator _navigator;

            #endregion

            #region Properties

            public Type ViewType { get; private set; }

            #endregion

            #region Methods

            public NavigatorViewTypeCache(Navigator navigator)
            {
                Contract.Requires(navigator != null, nameof(navigator));
                _navigator = navigator;
            }

            public INavigator For<TView>() => For(typeof(TView));

            public INavigator For(Type viewType)
            {
                ViewType = viewType;
                return _navigator;
            }

            #endregion

        }

    }

}