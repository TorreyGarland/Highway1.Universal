namespace Highway1.Universal.Services
{
    using ComponentModel;
    using ViewModels;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>Navigator class.</summary>
    public sealed partial class Navigator : INavigator
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Navigator" /> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        public Navigator(Frame frame)
        {
            _frame = frame;
            if (_frame == null)
                return;
            _frame.Navigated += FrameNavigated;
            _frame.Navigating += FrameNavigating;
            _frame.NavigationFailed += FrameNavigationFailed;
            _frame.NavigationStopped += FrameNavigationStopped;
        }

        private async void FrameNavigated(object sender, NavigationEventArgs e)
        {
            var frameworkElement = e.Content as FrameworkElement;
            if (frameworkElement != null)
            {
                var activatable = frameworkElement.DataContext as IActivatable;
                if (activatable != null)
                    activatable.Activate((NavigatorEventArgs)e);
                var asyncActivatable = frameworkElement.DataContext as IAsyncActivatable;
                if (asyncActivatable != null)
                    await asyncActivatable.ActivateAsync((NavigatorEventArgs)e);
            }
            Navigated?.Invoke(this, (NavigatorEventArgs)e);
        }

        private void FrameNavigating(object sender, NavigatingCancelEventArgs e)
            => Navigating?.Invoke(this, (NavigatorCancelEventArgs)e);

        private void FrameNavigationFailed(object sender, NavigationFailedEventArgs e)
            => NavigationFailed?.Invoke(this, (NavigatorFailedEventArgs)e);

        private void FrameNavigationStopped(object sender, NavigationEventArgs e)
            => NavigationStopped?.Invoke(this, (NavigatorEventArgs)e);

        /// <summary>Goes the back.</summary>
        public void GoBack() => _frame?.GoBack();

        /// <summary>Goes the forward.</summary>
        public void GoForward() => _frame?.GoForward();

    }

}