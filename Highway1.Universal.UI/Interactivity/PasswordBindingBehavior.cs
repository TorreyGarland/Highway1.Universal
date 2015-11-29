namespace Highway1.Universal.UI.Interactivity
{

    using System.Diagnostics;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Password binding behavior class.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class PasswordBindingBehavior : BehaviorBase<PasswordBox>
    {
        
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [bind password].
        /// </summary>
        /// <value><c>true</c> if [bind password]; otherwise, <c>false</c>.</value>
        public bool BindPassword
        {
            get { return (bool)GetValue(BindPasswordProperty); }
            set { SetValue(BindPasswordProperty, value); }
        }

        /// <summary>The bind password property</summary>
        public static DependencyProperty BindPasswordProperty { get; } 
            = DependencyProperty.Register(nameof(BindPassword), typeof(bool), typeof(PasswordBindingBehavior), new PropertyMetadata(true, BindPasswordChanged));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is updating.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is updating; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpdating
        {
            get { return (bool)GetValue(IsUpdatingProperty); }
            set { SetValue(IsUpdatingProperty, value); }
        }

        /// <summary>The is updating property</summary>
        public static DependencyProperty IsUpdatingProperty { get; } 
            = DependencyProperty.Register(nameof(IsUpdating), typeof(bool), typeof(PasswordBindingBehavior), new PropertyMetadata(false));

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>The password property</summary>
        public static DependencyProperty PasswordProperty { get; } 
            = DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBindingBehavior), new PropertyMetadata(string.Empty, PasswordChanged));

        #endregion

        #region Methods

        [DebuggerStepThrough]
        private static void BindPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
            (sender as PasswordBindingBehavior)?.BindPasswordChanged((bool)e.OldValue, (bool)e.NewValue);

        /// <summary>Binds the password changed.</summary>
        /// <param name="oldValue">if set to <c>true</c> [old value].</param>
        /// <param name="newValue">if set to <c>true</c> [new value].</param>
        private void BindPasswordChanged(bool oldValue, bool newValue)
        {
            if (AssociatedObject == null)
                return;
            if (oldValue)
                AssociatedObject.PasswordChanged -= PasswordBoxPasswordChanged;
            if (newValue)
                AssociatedObject.PasswordChanged += PasswordBoxPasswordChanged;
        }

        /// <summary>Called when [detached].</summary>
        protected override void OnDetached()
        {
            base.OnDetached();
            if (AssociatedObject != null)
                AssociatedObject.PasswordChanged -= PasswordBoxPasswordChanged;
        }

        [DebuggerStepThrough]
        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject == null)
                return;
            IsUpdating = true;
            Password = AssociatedObject.Password;
            IsUpdating = false;
        }

        [DebuggerStepThrough]
        private static void PasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
            (sender as PasswordBindingBehavior)?.PasswordChanged();

        /// <summary>Passwords the changed.</summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        [DebuggerStepThrough]
        private void PasswordChanged()
        {
            if (AssociatedObject == null)
                return;
            if (!BindPassword)
                return;
            AssociatedObject.PasswordChanged -= PasswordBoxPasswordChanged;
            if (!IsUpdating)
                AssociatedObject.Password = Password ?? string.Empty;
            AssociatedObject.PasswordChanged += PasswordBoxPasswordChanged;
        }

        #endregion

    }

}