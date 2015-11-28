namespace Highway1.Universal.Controls
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Expander class.
    /// </summary>
    public sealed class Expander : ContentControl
    {

        #region Fields

        /// <summary>The header property</summary>
        public static DependencyProperty HeaderProperty { get; }
            = DependencyProperty.Register(nameof(Header), typeof(object), typeof(Expander), new PropertyMetadata(null));

        /// <summary>Gets the header template property.</summary>
        /// <value>The header template property.</value>
        public static DependencyProperty HeaderTemplateProperty { get; }
            = DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(Expander), new PropertyMetadata(null));

        /// <summary>Gets the header template selector property.</summary>
        /// <value>The header template selector property.</value>
        public static DependencyProperty HeaderTemplateSelectorProperty { get; }
            = DependencyProperty.Register(nameof(HeaderTemplateSelector), typeof(DataTemplateSelector), typeof(Expander), new PropertyMetadata(null));

        /// <summary>The is expanded property</summary>
        /// <value>The is expanded property.</value>
        public static DependencyProperty IsExpandedProperty { get; }
            = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(Expander), new PropertyMetadata(false, OnIsExpandedChanged));

        /// <summary>Gets the off content property.</summary>
        /// <value>The off content property.</value>
        public static DependencyProperty OffContentProperty { get; }
            = DependencyProperty.Register(nameof(OffContent), typeof(object), typeof(Expander), new PropertyMetadata("Expand"));

        /// <summary>Gets the on content property.</summary>
        /// <value>The on content property.</value>
        public static DependencyProperty OnContentProperty { get; }
            = DependencyProperty.Register(nameof(OnContent), typeof(object), typeof(Expander), new PropertyMetadata("Collapse"));

        private ToggleSwitch _toggleSwitch;

        #endregion

        #region Properties

        /// <summary>Gets or sets the header.</summary>
        /// <value>The header.</value>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>Gets or sets the header template.</summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>Gets or sets the header template selector.</summary>
        /// <value>The header template selector.</value>
        public DataTemplateSelector HeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty); }
            set { SetValue(HeaderTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>Gets or sets the content of the off.</summary>
        /// <value>The content of the off.</value>
        public object OffContent
        {
            get { return GetValue(OffContentProperty); }
            set { SetValue(OffContentProperty, value); }
        }

        /// <summary>Gets or sets the content of the on.</summary>
        /// <value>The content of the on.</value>
        public object OnContent
        {
            get { return GetValue(OnContentProperty); }
            set { SetValue(OnContentProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Expander"/> class.
        /// </summary>
        public Expander()
        {
            DefaultStyleKey = typeof(Expander);
        }

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (_toggleSwitch != null)
                _toggleSwitch.Toggled -= Toggle_Toggled;
            _toggleSwitch = GetTemplateChild("PART_Toggle") as ToggleSwitch;
            if (_toggleSwitch != null)
                _toggleSwitch.Toggled += Toggle_Toggled;
            UpdateVisualState(false);
        }

        private void Toggle_Toggled(object sender, RoutedEventArgs e)
        {
            IsExpanded = !IsExpanded;
        }

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as Expander)?.UpdateVisualState(true);

        private void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsExpanded ? "STATE_Expanded" : "STATE_COLLAPSED", useTransitions);
        }

        #endregion

    }

}