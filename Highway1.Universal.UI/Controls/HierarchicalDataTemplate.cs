using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Highway1.Universal.UI.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class HierarchicalDataTemplate : DataTemplate
    {

        #region Fields

        /// <summary>The item container style property</summary>
        public static  DependencyProperty ItemContainerStyleProperty { get; } 
            = DependencyProperty.Register(nameof(ItemTemplateSelector), typeof(Style), typeof(HierarchicalDataTemplate), new PropertyMetadata(null, OnItemContainerStyleChanged));

        /// <summary>The data template property</summary>
        public static DependencyProperty ItemTemplateProperty { get; }
            = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(HierarchicalDataTemplate), new PropertyMetadata(null, OnItemTemplateChanged));

        /// <summary>The data template selector property</summary>
        public static DependencyProperty ItemTemplateSelectorProperty { get; }
            = DependencyProperty.Register(nameof(DataTemplateSelector), typeof(DataTemplateSelector), typeof(HierarchicalDataTemplate), new PropertyMetadata(null));

        #endregion

        #region Properties

        internal bool IsItemContainerStyleSet { get; private set; }

        internal bool IsItemTemplateSet { get; private set; }

        /// <summary>Gets or sets the item container style.</summary>
        /// <value>The item container style.</value>
        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        /// <summary>Gets or sets the items source.</summary>
        /// <value>The items source.</value>
        public Binding ItemsSource { get; set; }

        /// <summary>Gets or sets the item template.</summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>Gets or sets the item template selector.</summary>
        /// <value>The item template selector.</value>
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        #endregion

        #region Methods

        [DebuggerStepThrough]
        private static void OnItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as HierarchicalDataTemplate)?.OnItemContainerStyleChanged();

        [DebuggerStepThrough]
        private void OnItemContainerStyleChanged()
            => IsItemContainerStyleSet = true;

        [DebuggerStepThrough]
        private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as HierarchicalDataTemplate)?.OnItemTemplateChanged();

        [DebuggerStepThrough]
        private void OnItemTemplateChanged()
            => IsItemTemplateSet = true;

        #endregion

    }
}
