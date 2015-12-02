namespace Highway1.Universal.Services
{

    using Autofac;
    using Autofac.Core;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Autofac service provider base class.</summary>
    /// <seealso cref="System.IServiceProvider" />
    [DebuggerStepThrough]
    public abstract class AutofacServiceProviderBase : IServiceProvider
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Lazy<IContainer> _container;

        #endregion

        #region Properties

        /// <summary>Gets the container.</summary>
        /// <value>The container.</value>
        protected IContainer Container
        {
            get
            {
                Contract.Ensures(Contract.Result<IContainer>() != null, nameof(Container));
                return _container.Value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacServiceProviderBase" /> class.
        /// </summary>
        [DebuggerStepThrough]
        protected AutofacServiceProviderBase()
        {
            _container = new Lazy<IContainer>(BuildContainer);
        }

        [DebuggerStepThrough]
        private IContainer BuildContainer()
        {
            Contract.Ensures(Contract.Result<IContainer>() != null, nameof(BuildContainer));
            var builder = new ContainerBuilder();
            Register(builder);
            return builder.Build();
        }

        /// <summary>Gets the service.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [DebuggerStepThrough]
        [Pure]
        public T GetService<T>()
        {
            T instance;
            _container.Value.TryResolve(out instance);
            return instance;
        }

        /// <summary>Gets the service.</summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object GetService(Type serviceType)
        {
            object instance;
            _container.Value.TryResolve(serviceType, out instance);
            return instance;
        }

        /// <summary>Registers the specified builder.</summary>
        /// <param name="builder">The builder.</param>
        [DebuggerStepThrough]
        protected virtual void Register(ContainerBuilder builder)
        {
            Contract.Requires<ArgumentNullException>(builder != null, nameof(builder));
            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance().OnActivated(RegisterNavigation);
           // builder.RegisterType<ViewStateManager>().As<IViewStateManager>().SingleInstance();
            builder.RegisterType<DataSource>().As<IDataSource>().SingleInstance();
        }

        /// <summary>Registers the navigation.</summary>
        /// <param name="args">The <see cref="IActivatedEventArgs{Navigator}"/> instance containing the event data.</param>
        [DebuggerStepThrough]
        protected virtual void RegisterNavigation(IActivatedEventArgs<Navigator> args)
        {
            Contract.Requires<ArgumentNullException>(args != null, nameof(args));
        }

        #endregion

    }

}