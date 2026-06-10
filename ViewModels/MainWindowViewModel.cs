using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using laba9modules.Services;

namespace laba9modules.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        // Свойство возвращает текущую ViewModel из сервиса навигации
        public object CurrentDisplayView => _navigationService.CurrentView;

        // Команды для верхнего меню
        public ICommand NavigateToContactsCommand { get; }
        public ICommand NavigateToAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            // Подписываемся на обновление представления в сервисе, чтобы обновить UI Оболочки
            _navigationService.CurrentViewChanged += () => OnPropertyChanged(nameof(CurrentDisplayView));

            // Инициализация команд перехода
            NavigateToContactsCommand = new RelayCommand(obj => _navigationService.NavigateTo<MainWindowViewModel>());
            NavigateToAboutCommand = new RelayCommand(obj => _navigationService.NavigateTo<AboutViewModel>());

            // Стартовый экран по умолчанию
            _navigationService.NavigateTo<MainWindowViewModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
