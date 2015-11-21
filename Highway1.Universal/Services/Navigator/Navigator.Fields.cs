namespace Highway1.Universal.Services
{

    using System;
    using System.Collections.Concurrent;
    using Windows.UI.Xaml.Controls;

    partial class Navigator
    {

        private readonly Frame _frame;

        private readonly ConcurrentDictionary<Type, NavigatorViewTypeCache> _registrations = new ConcurrentDictionary<Type, NavigatorViewTypeCache>();

    }

}