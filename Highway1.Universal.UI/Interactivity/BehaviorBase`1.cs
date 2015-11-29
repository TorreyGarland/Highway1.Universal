namespace Highway1.Universal.UI.Interactivity
{

    using Microsoft.Xaml.Interactivity;
    using Windows.UI.Xaml;

    /// <summary>Behavior base class.</summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="DependencyObject" />
    /// <seealso cref="IBehavior" />
    public abstract class BehaviorBase<T> : DependencyObject, IBehavior where T : DependencyObject
    {

        #region Properties

        /// <summary>Gets the associated object.</summary>
        /// <value>The associated object.</value>
        protected T AssociatedObject { get; private set; }

        DependencyObject IBehavior.AssociatedObject => AssociatedObject;

        #endregion

        #region Methods

        void IBehavior.Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject as T;
            OnAttached();
        }

        void IBehavior.Detach() => OnDetached();

        /// <summary>Called when [attached].</summary>
        protected virtual void OnAttached() { }

        /// <summary>Called when [detached].</summary>
        protected virtual void OnDetached() { }

        #endregion

    }

}