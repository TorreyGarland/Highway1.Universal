namespace Highway1.Universal.Services
{

    using System;

    partial class Navigator
    {

        /// <summary>Navigates to.</summary>
        /// <typeparam name="TViewModel">The type of the view.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public bool NavigateTo<TViewModel>(object parameter = null)
            => NavigateTo(typeof(TViewModel), parameter);

        /// <summary>Navigates to.</summary>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public bool NavigateTo(Type viewModelType, object parameter = null)
        {
            if (_frame != null)
            {
                NavigatorViewTypeCache typeCache;
                if (_registrations.TryGetValue(viewModelType, out typeCache) && typeCache != null && typeCache.ViewType != null)
                {
                    return _frame.Navigate(typeCache.ViewType, parameter);
                }
            }
            return false;
        }

    }

}