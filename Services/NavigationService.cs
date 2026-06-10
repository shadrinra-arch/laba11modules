using System;

namespace laba9modules.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private object _currentView;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                CurrentViewChanged?.Invoke(); // Уведомляем Оболочку
            }
        }

        public event Action CurrentViewChanged;

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            // Запрашиваем экземпляр ViewModel из DI-контейнера
            var viewModel = _serviceProvider.GetService(typeof(TViewModel));
            if (viewModel != null)
            {
                CurrentView = viewModel;
            }
        }
    }
}
