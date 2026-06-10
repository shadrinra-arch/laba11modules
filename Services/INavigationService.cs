using System;

namespace laba9modules.Services
{
    public interface INavigationService
    {
        // Свойство, хранящее текущую активную ViewModel для отображения в ContentControl
        object CurrentView { get; }

        // Событие для уведомления Оболочки (MainWindow) о смене экрана
        event Action CurrentViewChanged;

        // Метод для перехода на указанную ViewModel через DI-контейнер
        void NavigateTo<TViewModel>() where TViewModel : class;
    }
}
