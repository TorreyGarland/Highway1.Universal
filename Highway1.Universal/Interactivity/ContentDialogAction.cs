namespace Highway1.Universal
{

    using Microsoft.Xaml.Interactivity;
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Data;

    /// <summary>Content dialog action class.</summary>
    public sealed class ContentDialogAction : DependencyObject, IAction
    {

        #region Properties

        /// <summary>Gets or sets the content template.</summary>
        /// <value>The content template.</value>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>Gets the content template property.</summary>
        /// <value>The content template property.</value>
        public static DependencyProperty ContentTemplateProperty { get; }
            = DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the primary command.</summary>
        /// <value>The primary command.</value>
        public ICommand PrimaryCommand
        {
            get { return (ICommand)GetValue(PrimaryCommandProperty); }
            set { SetValue(PrimaryCommandProperty, value); }
        }

        /// <summary>Gets the primary command property.</summary>
        /// <value>The primary command property.</value>
        public static DependencyProperty PrimaryCommandProperty { get; }
            = DependencyProperty.Register(nameof(PrimaryCommand), typeof(ICommand), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the primary command parameter.</summary>
        /// <value>The primary command parameter.</value>
        public object PrimaryCommandParameter
        {
            get { return GetValue(PrimaryCommandParameterProperty); }
            set { SetValue(PrimaryCommandParameterProperty, value); }
        }

        /// <summary>Gets the primary command parameter property.</summary>
        /// <value>The primary command parameter property.</value>
        public static DependencyProperty PrimaryCommandParameterProperty { get; }
            = DependencyProperty.Register(nameof(PrimaryCommandParameter), typeof(object), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the primary text.</summary>
        /// <value>The primary text.</value>
        public string PrimaryText
        {
            get { return (string)GetValue(PrimaryTextProperty); }
            set { SetValue(PrimaryTextProperty, value); }
        }

        /// <summary>Gets the primary text property.</summary>
        /// <value>The primary text property.</value>
        public static DependencyProperty PrimaryTextProperty { get; }
            = DependencyProperty.Register(nameof(PrimaryText), typeof(string), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the secondary command.</summary>
        /// <value>The secondary command.</value>
        public ICommand SecondaryCommand
        {
            get { return (ICommand)GetValue(SecondaryCommandProperty); }
            set { SetValue(SecondaryCommandProperty, value); }
        }

        /// <summary>Gets the secondary command property.</summary>
        /// <value>The secondary command property.</value>
        public static DependencyProperty SecondaryCommandProperty { get; }
            = DependencyProperty.Register(nameof(SecondaryCommand), typeof(ICommand), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the secondary command parameter.</summary>
        /// <value>The secondary command parameter.</value>
        public object SecondaryCommandParameter
        {
            get { return GetValue(SecondaryCommandParameterProperty); }
            set { SetValue(SecondaryCommandParameterProperty, value); }
        }

        /// <summary>Gets the secondary command parameter property.</summary>
        /// <value>The secondary command parameter property.</value>
        public static DependencyProperty SecondaryCommandParameterProperty { get; }
            = DependencyProperty.Register(nameof(SecondaryCommandParameter), typeof(object), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the secondary text.</summary>
        /// <value>The secondary text.</value>
        public string SecondaryText
        {
            get { return (string)GetValue(SecondaryTextProperty); }
            set { SetValue(SecondaryTextProperty, value); }
        }

        /// <summary>Gets the secondary text property.</summary>
        /// <value>The secondary text property.</value>
        public static DependencyProperty SecondaryTextProperty { get; }
            = DependencyProperty.Register(nameof(SecondaryText), typeof(string), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>Gets the title property.</summary>
        /// <value>The title property.</value>
        public static DependencyProperty TitleProperty { get; }
            = DependencyProperty.Register(nameof(Title), typeof(object), typeof(ContentDialogAction), new PropertyMetadata(null));

        /// <summary>Gets or sets the title template.</summary>
        /// <value>The title template.</value>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        /// <summary>Gets the title template property.</summary>
        /// <value>The title template property.</value>
        public static DependencyProperty TitleTemplateProperty { get; }
            = DependencyProperty.Register(nameof(TitleTemplate), typeof(DataTemplate), typeof(ContentDialogAction), new PropertyMetadata(null));

        #endregion

        #region Methods

        private void CopyBindings(ContentDialog dialog, DependencyProperty property, string path)
        {
            Contract.Requires(dialog != null, nameof(dialog));
            Contract.Requires(property != null, nameof(property));
            dialog.SetBinding(property, new Binding
            {
                Source = this,
                Path = new PropertyPath(path)
            });
        }

        /// <summary>Executes the asynchronous.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public IAsyncOperation<ContentDialogResult> Execute(object sender, object parameter)
        {
            var contentDialog = new ContentDialog();
            CopyBindings(contentDialog, ContentControl.ContentTemplateProperty, nameof(ContentTemplate));
            CopyBindings(contentDialog, ContentDialog.PrimaryButtonCommandParameterProperty, nameof(PrimaryCommandParameter));
            CopyBindings(contentDialog, ContentDialog.PrimaryButtonCommandProperty, nameof(PrimaryCommand));
            CopyBindings(contentDialog, ContentDialog.PrimaryButtonTextProperty, nameof(PrimaryText));
            CopyBindings(contentDialog, ContentDialog.SecondaryButtonCommandParameterProperty, nameof(SecondaryCommandParameter));
            CopyBindings(contentDialog, ContentDialog.SecondaryButtonCommandProperty, nameof(SecondaryCommand));
            CopyBindings(contentDialog, ContentDialog.SecondaryButtonTextProperty, nameof(SecondaryText));
            CopyBindings(contentDialog, ContentDialog.TitleProperty, nameof(Title));
            CopyBindings(contentDialog, ContentDialog.TitleTemplateProperty, nameof(TitleTemplate));
            return contentDialog.ShowAsync();
        }

        object IAction.Execute(object sender, object parameter)
        {
            Task.Run(() => Execute(sender, parameter));
            return sender;
        }

        #endregion

    }

}