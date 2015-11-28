namespace Highway1.Universal.UI.Controls
{

    using Collections;
    using System.Diagnostics;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media.Animation;

    /// <summary>Animated expander class.</summary>
    /// <remarks>Based off of the ExpanderUWP control developed by Dean Chalk (Thanks!!!) @ <a href="https://github.com/deanchalk/ExpanderUWP">https://github.com/deanchalk/ExpanderUWP</a>.</remarks>
    [ContentProperty(Name = nameof(Items))]
    [TemplatePart(Name = ToggleButtonPartName, Type = typeof(ToggleButton))]
    [TemplateVisualState(Name = CollapsedStateName, GroupName = ExpansionStatesGroupName)]
    [TemplateVisualState(Name = ExpandedStateName, GroupName = ExpansionStatesGroupName)]
    public sealed class AnimatedExpander : Control
    {

        #region Fields

        private const string CollapsedAnimationPartName = "PART_CollapsedAnimation";

        private const string CollapsedStateName = "STATE_Collapsed";

        private const string ExpandedAnimationPartName = "PART_ExpandedAnimation";

        private const string ExpandedStateName = "STATE_Expanded";

        private const string ExpansionStatesGroupName = "GROUP_ExpansionStates";

        private const string ToggleButtonPartName = "PART_ToggleButton";

        private DoubleAnimation _collapsedAnimation;

        private DoubleAnimation _expandedAnimation;

        private ToggleButton _toggleButton;

        #endregion

        #region Properties

        /// <summary>Gets or sets the header.</summary>
        /// <value>The header.</value>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>Gets the header property.</summary>
        /// <value>The header property.</value>
        public static DependencyProperty HeaderProperty { get; }
            = DependencyProperty.Register(nameof(Header), typeof(object), typeof(AnimatedExpander), new PropertyMetadata(null));

        /// <summary>Gets or sets the header template.</summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>Gets the header template property.</summary>
        /// <value>The header template property.</value>
        public static DependencyProperty HeaderTemplateProperty { get; }
            = DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(AnimatedExpander), new PropertyMetadata(null));

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

        /// <summary>Gets the is expanded property.</summary>
        /// <value>The is expanded property.</value>
        public static DependencyProperty IsExpandedProperty { get; }
            = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(AnimatedExpander), new PropertyMetadata(false, OnIsExpandedChanged));

        /// <summary>Gets or sets the items.</summary>
        /// <value>The items.</value>
        public FrameworkElementCollection Items
        {
            get { return (FrameworkElementCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        /// <summary>Gets the items property.</summary>
        /// <value>The items property.</value>
        public static DependencyProperty ItemsProperty { get; }
            = DependencyProperty.Register(nameof(Items), typeof(FrameworkElementCollection), typeof(AnimatedExpander), new PropertyMetadata(null));

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedExpander" /> class.
        /// </summary>
        [DebuggerStepThrough]
        public AnimatedExpander()
        {
            DefaultStyleKey = typeof(AnimatedExpander);
            Items = new FrameworkElementCollection();
        }

        /// <summary>Called when [apply template].</summary>
        protected override void OnApplyTemplate()
        {
            if (_toggleButton != null)
            {
                _toggleButton.Checked -= ToggleButtonChecked;
                _toggleButton.Unchecked -= ToggleButtonUnchecked;
            }
            base.OnApplyTemplate();
            _collapsedAnimation = GetTemplateChild(CollapsedAnimationPartName) as DoubleAnimation;
            _expandedAnimation = GetTemplateChild(ExpandedAnimationPartName) as DoubleAnimation;
            _toggleButton = GetTemplateChild(ToggleButtonPartName) as ToggleButton;
            if (_toggleButton != null)
            {
                _toggleButton.Checked += ToggleButtonChecked;
                _toggleButton.Unchecked += ToggleButtonUnchecked;
                _toggleButton.IsChecked = IsExpanded;
            }
        }

        [DebuggerStepThrough]
        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as AnimatedExpander)?.OnIsExpandedChanged();

        [DebuggerStepThrough]
        private void OnIsExpandedChanged()
        {
            if (_toggleButton == null)
                return;
            _toggleButton.IsChecked = IsExpanded;
            UpdateVisualStates();
        }

        [DebuggerStepThrough]
        private void ToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            IsExpanded = true;
            UpdateVisualStates();
        }

        [DebuggerStepThrough]
        private void ToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            IsExpanded = false;
            UpdateVisualStates();
        }

        [DebuggerStepThrough]
        private void UpdateVisualStates()
        {
            var items = Items;
            double totalHeight = 0;
            if (items != null)
            {
                var totalHeights = items.Where(x => x != null && !x.DesiredSize.IsEmpty);
                if (totalHeights.Any())
                    totalHeight = totalHeights.Sum(x => x.DesiredSize.Height);
            }
            if (_collapsedAnimation != null)
                _collapsedAnimation.From = totalHeight;
            if (_expandedAnimation != null)
                _expandedAnimation.To = totalHeight;
            var r = VisualStateManager.GoToState(this, IsExpanded ? ExpandedStateName : CollapsedStateName, true);
            Debug.WriteLine(r);
        }

        #endregion

    }

}